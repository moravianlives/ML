using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using edu.bucknell.project.moravianLives.model.Common;
using Zen.Base.Common;
using Zen.Base.Extension;
using Zen.Base.Module;
using Zen.Base.Module.Data.CommonAttributes;
using Zen.Pebble.FlexibleData.String.Localization;
using Zen.Pebble.FlexibleData.String.Localization.Concrete;
using Zen.Base.Module.Data;
using edu.bucknell.framework.Service.Configuration.Database.ConnectionBundle;

namespace edu.bucknell.project.moravianLives.model
{
    [DataConfigAttribute(ConnectionBundleType = typeof(MongoGenericBundle))]
    [DataEnvironmentMappingAttribute(Origin = "prd", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "uat", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "STA", Target = "dev")]
    public class Source : Data<Source>, IDataId
    {

        [System.ComponentModel.DataAnnotations.Key] public string Id { get; set; } = Guid.NewGuid().ToShortGuid();

        [Display] public HistoricString Name { get; set; }

        public HistoricPeriod ActivityPeriod { get; set; }
        public HistoricalReference<Source> Association { get; set; }
        public DataReference<Source> ParentSource { get; set; }
        public List<string> Categories { get; set; }
        public string Content { get; set; }
        public Dictionary<string, List<string>> Roles { get; set; }
        public string License { get; set; }

        public class Role : Category<Source, Role>
        {
        }

        public class Category : Category<Source, Category>
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
                    referenceEntry = new ExternalReference { Domain = domain };
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

        public class Media : Data<Media>
        {
            [System.ComponentModel.DataAnnotations.Key] public string Id { get; set; }

            public Dictionary<string, List<string>> References { get; set; } = new Dictionary<string, List<string>>();
        }

        public class ResourceDescription : Data<ResourceDescription>
        {
            [System.ComponentModel.DataAnnotations.Key] public string Id { get; set; }

            public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
        }
    }
}