using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using edu.bucknell.project.moravianLives.model.Common;
using Zen.Base.Extension;
using Zen.Base.Module;
using Zen.Pebble.FlexibleData.Historical;
using Zen.Pebble.FlexibleData.String.Localization;

namespace edu.bucknell.project.moravianLives.model
{
    public class Location : Data<Location>
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();

        [Display] public HistoricString Name { get; set; }

        public HistoricalValue<string> Parent { get; set; }
        public List<string> Categories { get; set; }

        public Geography Geography { get; set; }

        #region Overrides of Data<Location>

        public override void BeforeSave()
        {
            Id ??= Name.Value.Md5Hash();
        }

        #endregion

        public class Category : Category<Location, Category>
        {
        }

        public class ExternalReferences : Data<ExternalReferences>
        {
            [Key] public string Id { get; set; }

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