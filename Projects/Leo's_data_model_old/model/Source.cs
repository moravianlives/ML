using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using edu.bucknell.project.moravianLives.model.Common;
using Zen.Base.Common;
using Zen.Base.Extension;
using Zen.Base.Module;
using Zen.Pebble.FlexibleData.String.Localization;
using Zen.Pebble.FlexibleData.String.Localization.Concrete;

namespace edu.bucknell.project.moravianLives.model
{
    public class Source : Data<Source>
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToShortGuid();

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

        public class RawReference : Data<RawReference>
        {
            [Key] public string Id { get; set; }

            public List<DescriptorBlock> References { get; set; }

            public void Add(DescriptorBlock descriptorBlock)
            {
                var cs = descriptorBlock.ToJson().Md5Hash();

                if (References == null) References = new List<DescriptorBlock>();
                if (References.Any(reference => reference.ToJson().Md5Hash() == cs)) return;

                References.Add(descriptorBlock);
            }

            public class DescriptorBlock
            {
                public string Id { get; set; }
                public string Type { get; set; }
                public string Descriptor { get; set; }

                #region Overrides of Object

                public override string ToString()
                {
                    return $"{Type}: {Id} ({Descriptor})";
                }

                #endregion
            }
        }

        public class Media : Data<Media>
        {
            [Key] public string Id { get; set; }

            public Dictionary<string, List<string>> References { get; set; } = new Dictionary<string, List<string>>();
        }

        public class ResourceDescription : Data<ResourceDescription>
        {
            [Key] public string Id { get; set; }

            public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
        }
    }
}