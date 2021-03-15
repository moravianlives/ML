using System;
using System.Collections.Generic;
using System.Linq;
using edu.bucknell.project.moravianLives.model;
using edu.bucknell.project.moravianLives.model.Common;
using edu.bucknell.project.moravianLives.model.Common.Reference;
using edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.model;
using Newtonsoft.Json.Linq;
using Zen.Base.Extension;
using Zen.Pebble.CrossModelMap;
using Zen.Pebble.CrossModelMap.Change;
using Zen.Pebble.FlexibleData.Historical;
using Zen.Pebble.FlexibleData.String.Localization;
using Zen.Storage.Provider.File;

namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.tracker
{
    public class Personography : ChangeTracker<ContentEntry, Person>
    {
        private readonly PersonReference _personReference = new PersonReference();
        private readonly LocationReference _locationReference = new LocationReference();
        public class BirthData
        {
            public JObject Token { get; set; }
            public string Text { get; set; }
            public HistoricDateTime Timestamp { get; set; }
            public Location Location { get; set; }
        }

        public Personography()
        {
            ClearChangeTrack();


            SourceRepository = new MoravianLivesGitHubFileStorage().ResolveStorage();

            var _pl = new MapDefinition
            {
                KeyMaps = new Dictionary<string, KeyMapConfiguration>
                {
                    {
                        "person.persName.forename",
                        new KeyMapConfiguration
                        {
                            Target = "Name",
                            Handler = "HistoricString"
                        }
                    }
                }
            };

            // SourceRepository.StoreText("Projects/TEI_Memoirs", "ML_personography-transformMap.json", _pl.ToJson());

            Identifier(entry => entry.Id)
                .Configure(config =>
                {
                    config.StaleRecordTimespan = TimeSpan.FromSeconds(1);
                    config.Collection = "Bucknell.MoravianLives.GitHub";
                    config.SourceIdentifierPath = "person.@xml:id";
                    config.Set = "Personography";

                    //config.MemberMapping = SourceRepository
                    //    .GetText("Projects/TEI_Memoirs", "ML_personography-transformMap.json")
                    //    .Result
                    //    .FromJson<MapDefinition>();

                    config.MemberMapping = _pl;
                })
                .SourceModel(raw =>
                {
                    // First - Persons.
                    var source = SourceRepository
                        .GetDynamic("Projects/TEI_Memoirs/personography/XML", "ML_personography.xml")
                        .Result;

                    raw.Source = source;

                    // Select and Typecast the entries.
                    var items = ((JArray)source.SelectToken("TEI.text.body.listPerson"))
                        .Select(i => i.ToContentEntry(Configuration.SourceIdentifierPath))
                        .ToList();

                    raw.Items = items;
                })
                .SourceValue((item, path) => item.Contents.SelectTokens(path).FirstOrDefault()?.ToString())
                .ConvertToModelType(item =>
                {
                    if (item.Result == null) return;

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
                    var domain = $"{Configuration.Collection}.{Configuration.Set}";

                    return _personReference.ResolveReference(domain, sourceId, null);
                })
                .ComplexTransform(entry =>
                {
                    if (entry.targetModel == null) return;
                    if (entry.sourceData?.Contents == null) return;

                    var a = entry.sourceData.Contents.ToJson();
                    var b = entry.targetModel.ToJson();

                    var source = entry.sourceData?.Contents;

                    // First - name.

                    var firstName = source?.StringValue("person.persName.forename");
                    var lastName = source?.StringValue("person.persName.surname.#text");

                    var fullName = $"{firstName} {lastName}";

                    // Birth

                    var bd = new BirthData
                    {
                        Token = source?.JValue(new[]
                        {
                            "person.birth.date[?(@.@type == 'birth' && @.@calendar == 'Gregorian' && @.@resp == 'memoir')]",
                            "person.birth.date[?(@.@type == 'birth' && @.@calendar == 'Gregorian')]",
                            "person.birth.date[?(@.@type == 'birth' && @.@calendar != 'Julian')]"
                        })
                    };


                    bd.Text =  bd.Token.StringValue("#text");
                    bd.Timestamp = bd.Text;

                    entry.targetModel.Name = $"{firstName} {lastName}";

                    if (bd.Timestamp != null)
                    {
                        var targetNameVariant = entry.targetModel.Name.Variants.FirstOrDefault().Value.Variants.FirstOrDefault();
                        if (targetNameVariant != null)
                        {
                            targetNameVariant.Period = bd.Timestamp;
                            targetNameVariant.Period.End = null;

                            var nameResp = bd.Token.StringValue("@resp");
                            if (nameResp != null)
                                targetNameVariant.Comments ??= $"Source: {nameResp}";
                        }
                    }

                    var bpIdentifier = source.StringValue("person.birth.placeName.@ref");
                    var bpName = source.StringValue("person.birth.placeName.#text");

                    if (bpIdentifier != null)
                        bd.Location = _locationReference.ResolveReference(Placeography.DefaultDomain, bpIdentifier, bpName);
             
                })
                .OnCommit(() =>
                {
                    _personReference.Save();
                    _locationReference.Save();
                });
        }

        public IFileStorage SourceRepository { get; set; }
    }
}