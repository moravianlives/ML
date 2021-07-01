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
using Zen.Module.Data.MongoDB;
using Zen.Base.Module.Data;

namespace edu.bucknell.project.moravianLives.model
{
    [DataConfigAttribute(ConnectionBundleType = typeof(MongoDbDefaultBundle))]
    [DataEnvironmentMappingAttribute(Origin = "prd", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "uat", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "STA", Target = "dev")]
    public class Office: Data<Office>, IDataId

    {
        [System.ComponentModel.DataAnnotations.Key] public string Id { get; set; } = Guid.NewGuid().ToShortGuid();
        public string Value { get; set; }

    }
}
