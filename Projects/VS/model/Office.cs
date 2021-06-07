using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using edu.bucknell.project.moravianLives.model.Common;
using Zen.Base.Extension;
using Zen.Base.Module;
using Zen.Base.Module.Data.CommonAttributes;
using Zen.Pebble.FlexibleData.Historical;
using Zen.Pebble.FlexibleData.String.Localization;

namespace edu.bucknell.project.moravianLives.model
{
    public class Office: Data<Office>, IDataId

    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToShortGuid();
        public string Value { get; set; }

    }
}
