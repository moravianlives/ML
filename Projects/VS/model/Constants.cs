using Zen.Module.Data.MongoDB;
using Zen.Base.Module.Data;

namespace edu.bucknell.project.moravianLives.model
{
    [DataConfigAttribute(ConnectionBundleType = typeof(MongoDbDefaultBundle))]
    [DataEnvironmentMappingAttribute(Origin = "prd", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "uat", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "STA", Target = "dev")]
    public static class Constants
    {
        public static class Cultures
        {
            public static string English_UnitedStates = "en-US";
            public static string German = "de-DE";
        }
    }
}
