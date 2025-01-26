using Zen.Module.Data.MongoDB;
using Zen.Base.Module.Data;


namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.model
{
    [DataConfigAttribute(ConnectionBundleType = typeof(MongoDbDefaultBundle))]
    [DataEnvironmentMappingAttribute(Origin = "prd", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "uat", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "STA", Target = "dev")]
    public static class Extensions
    {
        public static ContentEntry ToContentEntry(this object source, string sourceIdentifierPath = null)
        {
            var res = new ContentEntry();

            var dynamicModel = source as dynamic;

            res.Id = dynamicModel.SelectToken(sourceIdentifierPath ?? "place.@xml:id").ToString();
            res.Contents = dynamicModel;
            return res;
        }
    }
}