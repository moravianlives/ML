using Newtonsoft.Json.Linq;
using Zen.Module.Data.MongoDB;
using Zen.Base.Module.Data;


namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.model
{
    [DataConfigAttribute(ConnectionBundleType = typeof(MongoDbDefaultBundle))]
    [DataEnvironmentMappingAttribute(Origin = "prd", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "uat", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "STA", Target = "dev")]
    public class ContentEntry
    {
        public string Id { get; set; }
        public JObject Contents { get; set; }
    }
}