using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using edu.bucknell.project.moravianLives.model.Common;
using Zen.Base.Common;
using Zen.Base.Extension;
using Zen.Base.Module;
using Zen.Pebble.FlexibleData.Common.Interface;
using Zen.Pebble.FlexibleData.String.Localization;

namespace edu.bucknell.project.moravianLives.model
{
    public class Organization : Data<Organization>
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToShortGuid();

        [Display] public HistoricString Name { get; set; }

        public List<string> Categories { get; set; }
        public RelationshipVariants Relationships { get; set; }

        public class RelationshipVariants : VariantTemporalMap<Category, DataReference<Organization>>
        {
        }

        public class Category : Category<Organization, Category>
        {
        }
    }
}