
using System;
using Zen.Base.Extension;
using Zen.Base.Module;
using Zen.Base.Module.Data.CommonAttributes;
using Zen.Base.Module.Data;
using edu.bucknell.framework.Service.Configuration.Database.ConnectionBundle;




namespace edu.bucknell.project.moravianLives.model
{
    [DataConfigAttribute(ConnectionBundleType = typeof(MongoGenericBundle))]
    [DataEnvironmentMappingAttribute(Origin = "prd", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "uat", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "STA", Target = "dev")]
    public class Relation : Data<Relation>, IDataId

    {
        [System.ComponentModel.DataAnnotations.Key] public string Id { get; set; } = Guid.NewGuid().ToShortGuid();
        public string relationName { get; set; }
        public string personName { get; set; }
        public string MLid { get; set; }

        public DateTime notBefore { get; set; }
        public DateTime notAfter { get; set; }

    }
}
