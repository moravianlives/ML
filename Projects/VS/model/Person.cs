using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using edu.bucknell.project.moravianLives.model.Common;
using Zen.Base.Common;
using Zen.Base.Extension;
using Zen.Base.Module;
using Zen.Base.Module.Data.CommonAttributes;
using Zen.Pebble.FlexibleData.Common.Interface;
using Zen.Pebble.FlexibleData.String.Localization;
using Zen.Pebble.FlexibleData.String.Localization.Interface;
using Zen.Base.Module.Data;
using Zen.Pebble.FlexibleData.Historical;
using edu.bucknell.framework.Service.Configuration.Database.ConnectionBundle;

namespace edu.bucknell.project.moravianLives.model
{
    [DataConfigAttribute(ConnectionBundleType = typeof(MongoGenericBundle))]
    [DataEnvironmentMappingAttribute(Origin = "prd", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "uat", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "STA", Target = "dev")]
    public class Person : Data<Person>, IFacts, IDataId
    {
        [System.ComponentModel.DataAnnotations.Key] public string Id { get; set; } = Guid.NewGuid().ToShortGuid();

        [Display] public HistoricString Name { get; set; }

        [Display] public HistoricString Firstname { get; set; }
        [Display] public HistoricString Lastname { get; set; }
        [Display] public HistoricString Addname { get; set; }
        public string MLid { get; set; }
        public HistoricDateTime BirthDate { get; set; }
        public HistoricDateTime DeathDate { get; set; }
        public string Birthplace { get; set; }
        public string Deathplace { get; set; }

        public RelationshipVariants Relationships { get; set; }
        public TemporalCommented<DataReference<Organization>> Organizations { get; set; }

        public List<officeRecord> officerecd { get; set; }
        //public string MemoirArchive { get; set; }

        //public string MemoirShelfmark { get; set; }
        //public string MemoirLink { get; set; }
        //public string MemoirLang { get; set; }

        public MemoirDesc MemoirInfo { get; set; }

        public string Gender { get; set; }

        public List<OfficeString> officeinfo { get; set; }

        #region Implementation of IFacts

        public Dictionary<string, string> Facts { get; set; } = new Dictionary<string, string>();

        #endregion

        public class Profile : Person
        {
            public class EventReference
            {
                public EventReference()
                {
                }

                public EventReference(Event source)
                {
                    Date = source.Date.Value;
                    DateDescriptor = source.Date.ToString();
                    Facts = source.Facts;
                }

                public string DateDescriptor { get; set; }

                public DateTime? Date { get; set; }

                public Dictionary<string, string> Facts { get; set; }
                public string Type { get; set; }
            }

            #region Overrides of Data<Person>

            public static Profile GetByPerson(string personId)
            {
                var res = Get(personId).ToJson().FromJson<Profile>();

                res.Events = Event.ByPerson(personId).OrderBy(i => i.Date?.Value)
                    .ToDictionary(i => i.Id, i => new EventReference(i)).ToList();

                if (res.Facts.ContainsKey("deathISODate") && res.Facts.ContainsKey("birthISO"))
                {
                    var span =
                        (DateTime.Parse(res.Facts["deathISODate"]) - DateTime.Parse(res.Facts["birthISO"])).Years();
                    res.Facts["Age"] = span.ToString();
                }

                if (res.Facts.ContainsKey("birthPlace"))
                    res.Facts["birthPlaceName"] = Location.Get(res.Facts["birthPlace"])?.Name?.Value;

                if (res.Facts.ContainsKey("deathPlace"))
                    res.Facts["deathPlaceName"] = Location.Get(res.Facts["deathPlace"])?.Name?.Value;


                return res;
            }

            public List<KeyValuePair<string, EventReference>> Events { get; set; }

            #endregion
        }

        public class RelationshipVariants : VariantTemporalMap<Category, DataReference<Person>>
        {
        }

        public class MemoirDesc
        {
            public string archive { get; set; }
            public string shelfmark { get; set; }
            public string link { get; set; }
            public string lang { get; set; }
        }
        public class Descriptor
        {
            public class Gender : Category<Person, Gender>
            {
            }
        }

        public class officeRecord
        {
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public Office officeitem { get; set; }
        }


        public class Category : Category<Person, Category>
        {
        }

        public class ExternalReferences : Data<ExternalReferences>, IDataId
        {
            [System.ComponentModel.DataAnnotations.Key] public string Id { get; set; }

            public List<ExternalReference> References { get; set; } = new List<ExternalReference>();

            public ExternalReference FetchReference(string domain, string key)
            {
                References ??= new List<ExternalReference>();

                var referenceEntry = References.FirstOrDefault(j => j.Domain == domain && j.Key == key) ??
                                     References.FirstOrDefault(j => j.Domain == domain);

                if (referenceEntry == null)
                {
                    referenceEntry = new ExternalReference {Domain = domain};
                    References.Add(referenceEntry);
                }

                referenceEntry.Key = key;

                return referenceEntry;
            }

            public class ExternalReference
            {
                public ExternalReference()
                {
                }

                public ExternalReference(string domain, string key)
                {
                    Domain = domain;
                    Key = key;
                }

                public string Domain { get; set; }
                public string Key { get; set; }
            }
        }
    }
}