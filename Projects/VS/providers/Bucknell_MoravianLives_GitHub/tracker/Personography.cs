using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using edu.bucknell.project.moravianLives.model;
using edu.bucknell.project.moravianLives.model.Common;
using edu.bucknell.project.moravianLives.model.Common.Reference;
using edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.model;
using Newtonsoft.Json;
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
    [Priority(Level = -1)]
    public class Personography : ChangeTracker<ContentEntry, Person>, IMoravianLivesDataOnboarding
    {
        private readonly LocationReference _locationReference = new LocationReference();
        private readonly PersonReference _personReference = new PersonReference();

        private readonly Dictionary<string, List<string>> DatetimeHealMap = new Dictionary<string, List<string>>
        {
            {"february", new List<string> {"febuary", "Febuary"}},
            {"november", new List<string> {"Novemeber"}}
        };

        private readonly Dictionary<string, string> EventCategoryIdentifiers = new Dictionary<string, string>();

        private readonly Dictionary<string, string> EventRolePerEventType = new Dictionary<string, string>
        {
            {"religious.baptism", "religious.receiver"},
            {"religious.marriage", "religious.spouse"},
            {"religious.widowed", "religious.spouse"},
            {"religious.sacrament", "religious.receiver"},
            {"religious.ordination", "religious.receiver"},
            {"religious.consecration", "religious.receiver"},
            {"religious.confirmation", "religious.receiver"},
            {"religious.exclusion", "religious.receiver"},
            {"religious.acceptance", "religious.receiver"},
            {"religious.synod", "religious.synod"},
            {"religious.readmission", "religious.readmitted"},
            {"religious.interment", "religious.receiver"},
            {"social.transfer", "social.transfer"},
            {"social.reception", "social.subject"},
            {"social.resignation", "social.resignation"},
            {"social.entrance", "social.subject"},
            {"life.accident", "life.person"},
            {"geo.travel.departure", "geo.traveler"},
            {"geo.travel.arrival", "geo.traveler"}
        };

        private readonly Dictionary<string, List<string>> EventTypeHealMap = new Dictionary<string, List<string>>
        {
            {"religious.baptism", new List<string> {"baptism"}},
            {"religious.marriage", new List<string> {"marriage"}},
            {"religious.widowed", new List<string> {"widowed"}},
            {"religious.sacrament", new List<string> {"sacrament"}},
            {"religious.ordination", new List<string> {"ordination"}},
            {"religious.consecration", new List<string> {"consecration"}},
            {"religious.confirmation", new List<string> {"confirmation"}},
            {"religious.exclusion", new List<string> {"exclusion"}},
            {"religious.acceptance", new List<string> {"acceptance"}},
            {"religious.synod", new List<string> {"synod"}},
            {"religious.readmission", new List<string> {"readmission"}},
            {"religious.interment", new List<string> {"interment"}},
            {"social.transfer", new List<string> {"transfer"}},
            {"social.reception", new List<string> {"reception"}},
            {"social.resignation", new List<string> {"resignation"}},
            {"social.entrance", new List<string> {"entrance"}},
            {"life.accident", new List<string> {"accident"}},
            {"geo.travel.departure", new List<string> {"departure"}},
            {"geo.travel.arrival", new List<string> {"arrival"}}
        };

        private readonly Dictionary<string, string> GenderCategoryIdentifiers = new Dictionary<string, string>();

        private string _contentDumpPath;
        public Dictionary<string, string> EventRoleIdentifiers = new Dictionary<string, string>();

        public IFileStorage SourceRepository;

        public Personography()
        {
            ClearChangeTrack();

            SourceRepository = new MoravianLivesGitHubFileStorage().ResolveStorage();

            Identifier(entry => entry.Id)
                .Configure(config =>
                {
                    config.StaleRecordTimespan = TimeSpan.FromSeconds(1);
                    config.Collection = "Bucknell.MoravianLives.GitHub";
                    config.SourceIdentifierPath = "person.@xml:id";
                    config.Set = "Personography";

                    config.CollectionFullIdentifier = $"{config.Collection}.{config.Set}";
                })
                .Prepare(() =>
                {
                    // Prefetch gender entries
                    GenderCategoryIdentifiers["f"] = Person.Descriptor.Gender.Fetch("female").Id;
                    GenderCategoryIdentifiers["m"] = Person.Descriptor.Gender.Fetch("male").Id;

                    // Prefetch unique Event types
                    EventCategoryIdentifiers["birth"] = Event.Category.Fetch("life.birth").Id;
                    EventCategoryIdentifiers["death"] = Event.Category.Fetch("life.death").Id;

                    // Prefetch unique Event roles
                    EventRoleIdentifiers["birth"] = Event.Role.Fetch("life.infant").Id;
                    EventRoleIdentifiers["death"] = Event.Role.Fetch("life.deceased").Id;
                })
                .SourceModel(raw =>
                {
                    // First - Persons.
                    var source = SourceRepository
                        .GetDynamic("Projects/Personography", "ML_personography-1.xml")
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

                    return _personReference.GetReference(Configuration.CollectionFullIdentifier, sourceId, null);
                })
                .ComplexTransform(entry =>
                {

                    var mustMentionEntry = false;

                    if (entry.targetModel == null) return;
                    if (entry.sourceData?.Contents == null) return;
                    if (entry.timeLog == null) return;

                    entry.timeLog.Log("Entity serialization");

                    var serializedSource = entry.sourceData.Contents.ToJson();
                    var serializedModel = entry.targetModel.ToJson();
                    var source = entry.sourceData?.Contents;

                    // Debug help

                    if (entry.sourceData.Id == "mlper000215") // Anna Rosina Anders
                    {
                    }

                    if (entry.sourceData.Id == "mlper000227") // Sarah Cennick
                    {
                    }

                    if (entry.sourceData.Id == "mlper000566") // Nicolas Zinzendorf
                    {
                    }


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

                    var gender = source.StrVal("person.@sex");

                    if (gender == null)
                    {
                        ScopedLog.Log($"{entry.sourceData.Id}: missing [Gender] value");
                    }
                    else
                    {
                        if (!GenderCategoryIdentifiers.ContainsKey(gender))
                        {
                            ScopedLog.Log($"{entry.sourceData.Id}: [Gender] unknown value '{gender}'", Message.EContentType.Info);
                            mustMentionEntry = true;
                        }
                        else
                            entry.targetModel.Gender = GenderCategoryIdentifiers[gender];
                    }

                    entry.timeLog.Log("First name resolution");

                    var firstName = source?.StrVal(new[]
                    {
                        "person.persName.forename[0].#text",
                        "person.persName.forename[0]",
                        "person.persName.forename.#text",
                        "person.persName.forename"
                    });

                    var firstNameVariants = source
                        ?.SelectTokens("person.persName.forename[?(@.@type == 'variant')].#text").ToList();

                    var midName = source?.StrVal(new[]
                    {
                        "person.persName.forename[0].addName",
                        "person.persName.forename.addName"
                    });

                    if (midName != null)
                        firstName += " " + midName;

                    var lastName = source?.StrVal(new[]
                    {
                        "person.persName.surname.#text",
                        "person.persName.surname[?(@.@type == 'birth')].#text",
                        "person.persName.surname"
                    });

                    var surnameVariants = source?.SelectTokens("person.persName.surname").ToList();

                    if (surnameVariants.Count > 0)
                        if (surnameVariants[0].Type == JTokenType.Array)
                            surnameVariants = source?.SelectTokens("person.persName.surname[*]").ToList();

                    var fullName = $"{firstName} {lastName}".Trim();

                    //Birth
                    entry.timeLog.Log("Birth event information");

                    var birthEventData = new PrimaryEventData
                    {
                        Token = source?.JValue(new[]
                        {
                            "person.birth.date[?(@.@type == 'birth' && @.@calendar == 'Gregorian' && @.@resp == 'memoir')]",
                            "person.birth.date[?(@.@type == 'birth' && @.@calendar == 'Gregorian')]",
                            "person.birth.date[?(@.@type == 'birth' && @.@calendar != 'Julian')]"
                        })
                    };

                    if (birthEventData.Token != null) // There's a birthday, so let's parse.
                    {
                        birthEventData.Text = birthEventData.Token.StrVal("@when-iso") ??
                                              birthEventData.Token.StrVal("#text");
                        birthEventData.Timestamp = HealDatetime("Birthdate", entry.sourceData.Id, birthEventData.Text);
                    }

                    birthEventData.PlaceIdentifier = source.StrVal("person.birth.placeName.@ref");
                    birthEventData.PlaceName = source.StrVal("person.birth.placeName.#text");

                    if (birthEventData.PlaceIdentifier != null)
                        birthEventData.Location = _locationReference.GetReference(Placeography.DefaultDomain,
                            birthEventData.PlaceIdentifier, new Location() { Name = birthEventData.PlaceName });


                    // Death
                    entry.timeLog.Log("Death event information");

                    var deathEventData = new PrimaryEventData
                    {
                        Token = source?.JValue(new[]
                        {
                            "person.death.date[?(@.@type == 'death' && @.@calendar == 'Gregorian' && @.@resp == 'memoir')]",
                            "person.death.date[?(@.@type == 'death' && @.@calendar == 'Gregorian')]",
                            "person.death.date[?(@.@type == 'death' && @.@calendar != 'Julian')]",
                            "person.death.date"
                        })
                    };

                    if (deathEventData.Token != null) // 
                    {
                        deathEventData.Text = deathEventData.Token.StrVal("@when-iso") ??
                                              deathEventData.Token.StrVal("#text");
                        deathEventData.Timestamp = HealDatetime("Deathdate", entry.sourceData.Id, deathEventData.Text);
                    }

                    deathEventData.PlaceIdentifier = source.StrVal("person.death.placeName.@ref");
                    deathEventData.PlaceName = source.StrVal("person.death.placeName.#text");

                    if (deathEventData.PlaceIdentifier != null)
                        deathEventData.Location = _locationReference.GetReference(Placeography.DefaultDomain,
                            deathEventData.PlaceIdentifier, new Location() { Name = deathEventData.PlaceName });


                    // Add name variants

                    entry.timeLog.Log("Name variant detection");

                    entry.targetModel.Name ??= new HistoricString();
                    entry.targetModel.Name.SetVariant(fullName);
                    entry.targetModel.Firstname ??= new HistoricString();
                    entry.targetModel.Lastname ??= new HistoricString();
                    entry.targetModel.Addname ??= new HistoricString();
                    entry.targetModel.Firstname.SetVariant(firstName);
                    entry.targetModel.Lastname.SetVariant(lastName);
                    entry.targetModel.Addname.SetVariant(midName);

                    foreach (var sv in surnameVariants)
                    {
                        if (sv == null) continue;

                        if (!(sv is JObject)) continue;

                        var qsv = (JObject)sv;
                        var variantName = $"{firstName} {qsv.StrVal("#text")}".Trim();
                        var variantComment = qsv.StrVal("@type");

                        if (qsv.StrVal("@resp") != null)
                            variantComment += ", " + qsv.StrVal("@resp");

                        variantComment = variantComment.Trim();

                        //entry.targetModel.Name.SetVariant(variantName, null, variantComment);
                        entry.targetModel.Lastname.SetVariant(variantName, null, variantComment);
                    }

                    foreach (var fnv in firstNameVariants)
                        entry.targetModel.Firstname.SetVariant(fnv.ToString(), null, "firstname variant");

                    if (birthEventData.Timestamp != null)
                    {
                        var targetNameVariant = entry.targetModel.Name.Variants.FirstOrDefault().Value.Variants
                            .FirstOrDefault();
                        if (targetNameVariant != null)
                        {
                            targetNameVariant.Period = birthEventData.Timestamp;
                            targetNameVariant.Period.End = null;

                            var nameResp = birthEventData.Token.StrVal("@resp");
                            if (nameResp != null)
                                targetNameVariant.Comments ??= $"Source: {nameResp}";
                        }
                    }
                    // read memoir information
                    entry.timeLog.Log("Memoir information detection");

                    var MemoirArchive = source?.StrVal(new[]
                    {
                        "person.listBibl.msDesc.msIdentifier.institution.orgName",
                     });

                    var Memoirshelfmark = source?.StrVal(new[]
                    {
                        "person.listBibl.msDesc.msIdentifier.institution.idno",
                     });

                    var MemoirLink = source.StrVal("person.listBibl.msDesc.msIdentifier.institution.ptr.@target");

                    entry.targetModel.MemoirArchive ??= MemoirArchive;
                    entry.targetModel.MemoirShelfmark ??= Memoirshelfmark;
                    entry.targetModel.MemoirLink ??= MemoirLink;

                    // Finally, event handling.

                    entry.timeLog.Log("Unique event: Birth");

                    if (birthEventData.Timestamp != null)
                    {
                        var birthEventModel = DataSets.Store(Event.FetchUnique(entry.targetModel.Id, EventRoleIdentifiers["birth"], EventCategoryIdentifiers["birth"]));

                        birthEventModel.Name ??= "Birth of " + entry.targetModel.Name;
                        birthEventModel.Date ??= birthEventData.Timestamp;
                        birthEventModel.Place ??= birthEventData.Location?.Id;
                        birthEventModel.Source ??= $"{Configuration.CollectionFullIdentifier}:{entry.sourceData.Id}";
                    }

                    entry.timeLog.Log("Unique event: Death");

                    if (deathEventData.Timestamp != null)
                    {
                        var deathEventModel = DataSets.Store(Event.FetchUnique(entry.targetModel.Id,
                            EventRoleIdentifiers["death"], EventCategoryIdentifiers["death"]));

                        deathEventModel.Name ??= "Death of " + entry.targetModel.Name;
                        deathEventModel.Date ??= deathEventData.Timestamp;
                        deathEventModel.Place ??= deathEventData.Location?.Id;
                        deathEventModel.Source ??= $"{Configuration.CollectionFullIdentifier}:{entry.sourceData.Id}";
                    }


                    // Listed Events.

                    var events = source?.SelectTokens("person.event[*]").Select(i => (JObject)i).ToList();

                    foreach (var @event in events)
                    {
                        var mustMentionEvent = false;

                        var serializedEvent = @event.ToJson();

                        Location eventLocation = null;
                        HistoricDateTime eventDatetime = null;

                        // What?
                        var rawEventType = @event.StrVal("@type");
                        var eventType = EventTypeHealMap.ConvertByOptionsMap(rawEventType);

                        if (eventType == null)
                        {
                            ScopedLog.Log($"{entry.sourceData.Id}: [Event] unknown type '{rawEventType}'", Message.EContentType.Critical);
                            ScopedLog.Log($"{entry.sourceData.Id}: {serializedEvent}", Message.EContentType.Critical);
                            continue;
                        }

                        if (!EventRolePerEventType.ContainsKey(eventType))
                        {
                            ScopedLog.Log($"{entry.sourceData.Id}: [Event] no event role described for event type '{rawEventType}'", Message.EContentType.Critical);
                            ScopedLog.Log($"{entry.sourceData.Id}: {serializedEvent}", Message.EContentType.Critical);

                            continue;
                        }

                        var eventRole = EventRolePerEventType[eventType];

                        var eventCategoryModelId = Event.Category.Fetch(eventType).Id;


                        // Where?
                        var rawEventPlace = @event.StrVal("@where");

                        if (rawEventPlace == null)
                        {
                            ScopedLog.Log($"{entry.sourceData.Id}: [Event] no location listed",
                                Message.EContentType.Info);
                            mustMentionEvent = true;
                        }
                        else
                        {
                            eventLocation =
                                _locationReference.GetReference(Placeography.DefaultDomain, rawEventPlace);
                        }

                        //When?

                        var rawEventTime = @event.StrVal(new[] { "@when-iso", "@when" });

                        if (rawEventTime == null)
                        {
                            ScopedLog.Log($"{entry.sourceData.Id}: [Event] no Date/time (@when-iso/@when)",
                                Message.EContentType.Info);
                            mustMentionEvent = true;
                        }

                        eventDatetime = HealDatetime("Eventdate", entry.sourceData.Id, rawEventTime);

                        // All set? Let's search for event, or create.

                        var eventRoleId = Event.Role.Fetch(eventRole).Id;

                        var eventLocationId = eventLocation?.Id;

                        var eventModel = Event.Where(i =>
                            i.Date == eventDatetime && i.Place == eventLocationId && i.Roles.Any(j =>
                                j.RoleId == eventRoleId && j.PersonId == entry.targetModel.Id)).FirstOrDefault();

                        if (eventModel == null)
                            eventModel = new Event
                            {
                                Date = eventDatetime,
                                Place = eventLocationId,
                                Roles = new List<Event.RoleDescriptor>
                                    {new Event.RoleDescriptor {PersonId = entry.targetModel.Id, RoleId = eventRoleId}}
                            };

                        eventModel.Categories ??= new List<string>();

                        eventModel.Categories.Ensure(eventCategoryModelId);

                        DataSets.Store(eventModel);


                        var eventDescription = @event.StrVal(new[] { "desc.#text", "desc" });

                        //Anyone else mentioned?
                        var otherPersonRefId = @event.StrVal("@ref");

                        string eventPlaceDescriptionPart = null;

                        if (otherPersonRefId != null)
                        {
                            string otherPersonName = null;

                            if (eventCategoryModelId == "marriage")
                                if (eventDescription.StartsWith("married "))
                                {
                                    var eventDescriptionParts = eventDescription.Substring(8)
                                        .Split(" at ", StringSplitOptions.RemoveEmptyEntries);

                                    otherPersonName = eventDescriptionParts[0].Trim();

                                    if (eventDescriptionParts.Length > 1)
                                        if (eventLocation.Name == null)
                                        {
                                            eventLocation.Name = eventDescriptionParts[1].Trim();
                                            ScopedLog.Log(
                                                $"{entry.sourceData.Id}: [Event:{eventCategoryModelId}] place name assigned from Event Description - {eventLocation.Name}",
                                                Message.EContentType.Info);
                                            mustMentionEvent = true;
                                        }
                                }

                            // {"@type":"ordination","@where":"#mlpla000040","@ref":"#mlper000557","desc":"ordained as a deacon at Gracehill Samuel Benade"}
                            if (eventCategoryModelId == "ordination")
                                if (eventDescription.StartsWith("ordained"))
                                {
                                    var eventDescriptionParts = eventDescription.Substring(8)
                                        .Split(" at ", StringSplitOptions.RemoveEmptyEntries);

                                    if (eventDescriptionParts.Length > 1)
                                        if (eventLocation.Name == null)
                                        {
                                            eventLocation.Name = eventDescriptionParts[1].Trim();
                                            ScopedLog.Log(
                                                $"{entry.sourceData.Id}: [Event:{eventCategoryModelId}] place name assigned from Event Description - {eventLocation.Name}",
                                                Message.EContentType.Info);
                                            mustMentionEvent = true;
                                        }
                                }

                            var otherPersonModel = _personReference.GetReference(Configuration.CollectionFullIdentifier, otherPersonRefId, new Person() { Name = otherPersonName });
                            eventModel.Roles.Ensure(new Event.RoleDescriptor { PersonId = otherPersonModel.Id, RoleId = eventRoleId });
                            ScopedLog.Log($"{entry.sourceData.Id}: [Event:{eventCategoryModelId}] referenced Person - {otherPersonModel.Name ?? otherPersonModel.Id} as {eventRoleId}", Message.EContentType.Info);
                        }

                        var placeName = eventLocation?.Name != null ? eventLocation.Name + ", " : "";

                        eventModel.Name ??= $"{eventCategoryModelId.ToTitleCase()} - {placeName}{eventDatetime}";

                        eventModel.Description ??= eventDescription;

                        if (mustMentionEvent) ScopedLog.Log($"{entry.sourceData.Id}: {serializedEvent}", Message.EContentType.MoreInfo);
                    }

                    if (mustMentionEntry) ScopedLog.Log($"{entry.sourceData.Id}: {serializedModel}", Message.EContentType.MoreInfo);

                })
                .OnCommit(() =>
                {
                    _personReference.Save();
                    _locationReference.Save();
                });
        }


        private HistoricDateTime HealDatetime(string field, string identifier, string originalText)
        {
            if (originalText == null) return null;
            var corrected = false;

            // Mistyped textual values
            var correctedText = originalText;

            var (correctTerm, value) = DatetimeHealMap.FirstOrDefault(i => i.Value.Any(originalText.Contains));

            if (correctTerm != null)
                foreach (var incorrectTerm in value.Where(originalText.Contains))
                {
                    correctedText = originalText.Replace(incorrectTerm, correctTerm);
                    corrected = true;
                    break;
                }

            if (!corrected)
                if (originalText.Contains('-'))
                {
                    var parts = originalText.Split('-').ToList();


                    if (parts.Count == 2)
                        if (parts[0].Length == 6)
                        {
                            var compositePart = parts[0];

                            parts.RemoveAt(0);
                            parts.Add(compositePart.Substring(4, 2));
                            parts.Add(compositePart.Substring(0, 4));

                            parts.Reverse();

                            corrected = true;
                        }

                    if (parts.Count == 3)
                    {
                        // 1740-20-08

                        var convertedParts = parts.Select(i =>
                        {
                            var success = int.TryParse(i, out var result);
                            return success ? result : -1;
                        }).ToList();

                        var canTry = convertedParts.All(i => i != -1);

                        if (canTry)
                            if (convertedParts[0] > 1000)
                                if (convertedParts[1] > 12) // Improper date composition
                                {
                                    var partA = convertedParts[1];
                                    var partB = convertedParts[2];

                                    convertedParts[1] = partB;
                                    convertedParts[2] = partA;

                                    corrected = true;
                                }

                        correctedText = string.Join('-', convertedParts);
                    }
                }

            if (corrected)
                ScopedLog.Log($"{identifier}: [{field}] heal {originalText} -> {correctedText}", Message.EContentType.Maintenance);

            return corrected ? correctedText : originalText;
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