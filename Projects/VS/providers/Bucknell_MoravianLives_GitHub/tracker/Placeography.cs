using System;
using System.Collections.Generic;
using System.Linq;
using edu.bucknell.project.moravianLives.model;
using edu.bucknell.project.moravianLives.model.Common;
using edu.bucknell.project.moravianLives.model.Common.Reference;
using edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.model;
using Newtonsoft.Json.Linq;
using Zen.Base.Common;
using Zen.Base.Extension;
using Zen.Pebble.CrossModelMap;
using Zen.Pebble.CrossModelMap.Change;
using Zen.Pebble.FlexibleData.Historical;
using Zen.Pebble.FlexibleData.String.Localization;
using Zen.Storage.Provider.File;

namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.tracker
{
    [Priority(Level = 0)]
    public class Placeography : ChangeTracker<ContentEntry, Location>, IMoravianLivesDataOnboarding
    {
        private readonly LocationReference _locationReference = new LocationReference();

        internal static readonly string DefaultDomain = "Bucknell.MoravianLives.GitHub.Placeography";

        public Placeography()
        {
            SourceRepository = new MoravianLivesGitHubFileStorage().ResolveStorage();

            var _pl = new MapDefinition
            {
                KeyMaps = new Dictionary<string, KeyMapConfiguration>
                {
                    {
                        "place.location.geo",
                        new KeyMapConfiguration {Target = "Geography", Handler = "Historical.Geography"}
                    },
                    {
                        "place.placeName[?(@.@type == 'preferred')].name.#text",
                        new KeyMapConfiguration
                        {
                            Target = "Name",
                            Handler = "HistoricString",
                            AternateSources = new List<string>
                            {
                                "place.placeName[?(@.@type == 'standard')].name.#text",
                                "place.placeName[?(@.@type == 'preferred')].name",
                                "place.placeName[?(@.@type == 'standard')].name",
                                "place.placeName[0].name"
                            }
                        }
                    }
                }
            };

            SourceRepository.StoreText("Projects/TEI_Memoirs", "ML_placeography-transformMap.json", _pl.ToJson());

            Identifier(entry => entry.Id)
                .Configure(config =>
                {
                    config.StaleRecordTimespan = TimeSpan.FromSeconds(1);
                    config.Collection = "Bucknell.MoravianLives.GitHub";
                    config.SourceIdentifierPath = "place.@xml:id";
                    config.Set = "Placeography";

                    config.MemberMapping = SourceRepository
                        .GetText("Projects/TEI_Memoirs", "ML_placeography-transformMap.json")
                        .Result
                        .FromJson<MapDefinition>();

                    config.MemberMapping = _pl;
                })
                .SourceModel(raw =>
                {
                    // First - Locations.
                    var placeographySource = SourceRepository.GetDynamic("Projects/TEI_Memoirs", "ML_placeography.xml")
                        .Result;

                    raw.Source = placeographySource;

                    // Select and Typecast the entries.
                    raw.Items = ((JArray)placeographySource.SelectToken("TEI.text.body.listPlace"))
                        .Select(i => i.ToContentEntry(Configuration.SourceIdentifierPath))
                        .ToList();
                })
                .SourceValue((item, path) => item.Contents.SelectTokens(path).FirstOrDefault()?.ToString())
                .ConvertToModelType(item =>
                {
                    item.Result.Success = false;

                    switch (item.HandlerType)
                    {
                        case "HistoricString":
                            item.Result.Value = (HistoricString)item.Source;
                            item.Result.Success = true;
                            break;

                        case "Historical.Geography":
                            Geography tempValue = item.Source;
                            item.Result.Value = tempValue;
                            item.Result.Success = true;
                            break;
                    }
                })
                .ResolveReference(source =>
                {
                    //Let's first try to identify if the item is already present on our database as a reference.
                    // We'll receive a source item and try to resolve it to its 1:1 Data model.

                    var sourceId = GetIdentifier(source);
                    var domain = (Configuration.Collection != null && Configuration.Set != null ? $"{Configuration.Collection}.{Configuration.Set}" : DefaultDomain);

                    return _locationReference.GetReference(domain, sourceId);
                })
                .ComplexTransform(entry =>
                {
                    var a = entry.sourceData.Contents.ToJson();
                    var b = entry.targetModel.ToJson();
                    //#Mlpla
                    entry.targetModel.MLid = entry.sourceData.Id;

                    var relationshipMarker = entry.sourceData.Contents.SelectToken("relation");
                    if (relationshipMarker != null)
                        if (relationshipMarker.SelectToken("@name").ToString() == "containedBy")
                        {
                            var hardReferenceId = relationshipMarker.SelectToken("@normalizedId")?.ToString();

                            if (hardReferenceId == null)
                            {
                                var domain = $"{Configuration.Collection}.{Configuration.Set}";
                                var referenceId = relationshipMarker.SelectToken("@passive").ToString();

                                var parentReference = _locationReference.GetReference(domain, referenceId).Id;
                                hardReferenceId = parentReference;
                            }

                            entry.targetModel.Parent = hardReferenceId;
                        }

                

                    //Specifically for Placeography we have alternate name formats. So we'll extract and add it to the Variants list.

                    var variantNames = entry.sourceData.Contents
                        .SelectTokens("place.placeName[?(@.@type == 'variant')]").ToList();

                    foreach (var i in variantNames)
                    {
                        var c = i.ToJson();

                        var variantName = i.SVal("name.#text");

                        entry.targetModel.Name.SetVariant(
                            i.SVal("name.#text"),
                            JsonHelperExtensions.GetDefaultCulture(i.SVal("@xml:lang")),
                            i.SVal("note.label"),
                            null,
                            i.SVal("@when-iso")
                        );
                    }
                })
                .OnCommit(() =>
                {
                    _locationReference.Save();
                });


            ClearChangeTrack();
        }

        public IFileStorage SourceRepository { get; set; }
    }
}