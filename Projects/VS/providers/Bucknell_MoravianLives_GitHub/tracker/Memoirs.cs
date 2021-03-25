using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using edu.bucknell.project.moravianLives.model;
using edu.bucknell.project.moravianLives.model.Common;
using edu.bucknell.project.moravianLives.model.Common.Reference;
using edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.model;
using Newtonsoft.Json.Linq;
using Zen.Base;
using Zen.Base.Common;
using Zen.Base.Extension;
using Zen.Base.Module.Log;
using Zen.Pebble.CrossModelMap.Change;
using Zen.Pebble.FlexibleData.Historical;
using Zen.Pebble.FlexibleData.String.Localization;
using Zen.Storage.Cache;
using Zen.Storage.Provider.File;

namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.tracker
{
    [Priority(Level = -2)]

    public class Memoirs : ChangeTracker<ContentEntry, Source>, IMoravianLivesDataOnboarding
    {
        private readonly LocationReference _locationReference = new LocationReference();
        private readonly PersonReference _personReference = new PersonReference();
        private readonly SourceReference _sourceReference = new SourceReference();

        private string _contentDumpPath;
        public Dictionary<string, string> EventRoleIdentifiers = new Dictionary<string, string>();

        public IFileStorage SourceRepository;

        public Memoirs()
        {
            ClearChangeTrack();

            SourceRepository = new MoravianLivesGitHubFileStorage().ResolveStorage();

            _ = Identifier(entry => entry.Id)
                .Configure(config =>
                {
                    config.StaleRecordTimespan = TimeSpan.FromSeconds(1);
                    config.Collection = "Bucknell.MoravianLives.GitHub";
                    config.SourceIdentifierPath = "person.@xml:id";
                    config.Set = "Memoirs";

                    config.CollectionFullIdentifier = $"{config.Collection}.{config.Set}";
                })
                .Prepare(() =>
                {

                })
                .SourceModel(raw =>
                {
                    raw.Items = new List<ContentEntry>();

                    var memoirs = SourceRepository.Collection("Fulneck/StandardXML").Result;

                    foreach (var memoir in memoirs)
                    {
                        try
                        {
                            var source = (JObject)SourceRepository
                             .GetDynamic("Fulneck/StandardXML", memoir.Value.StorageName)
                             .Result;

                             raw.Items.Add(new ContentEntry() { Id = memoir.Value.StorageName.Trim().ToLower(), Contents = source });
                        }
                        catch (Exception e)
                        {
                            ScopedLog.Log($"{memoir.Value.StorageName}: [Load] File Error ({e.FancyString()})", Message.EContentType.Critical);
                            continue;
                        }



                    }
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

                    return _sourceReference.GetReference(Configuration.CollectionFullIdentifier, sourceId, null);
                })
                .ComplexTransform(entry =>
                {
                    if (entry.targetModel == null) return;
                    if (entry.sourceData?.Contents == null) return;
                    if (entry.timeLog == null) return;

                    entry.timeLog.Log("Entity serialization");

                    var serializedSource = entry.sourceData.Contents.ToJson();
                    var serializedModel = entry.targetModel.ToJson();
                    var source = entry.sourceData?.Contents;


                    //If development - dump info.

                    if (Host.IsDevelopment)
                    {
                        var devDumpPath = Path.Combine(Configuration.Set, $"{entry.sourceData.Id}.json");

                        var finalDumpPath = Local.WriteString(devDumpPath, serializedSource);

                        if (_contentDumpPath == null)
                        {
                            _contentDumpPath = Path.GetDirectoryName(finalDumpPath);

                            Log.Info("Debug dump location:");
                            Log.Info(_contentDumpPath);
                        }
                    }

                    entry.timeLog.Log("Gender detection");

                    // Gender



                })
                .OnCommit(() =>
                {
                    _personReference.Save();
                    _locationReference.Save();
                });
        }


        public class PrimaryEventData
        {
            public JObject Token { get; set; }
            public string Text { get; set; }
            public HistoricDateTime Timestamp { get; set; }
            public Location Location { get; set; }
            public string PlaceIdentifier { get; set; }
            public string PlaceName { get; set; }
        }
    }
}