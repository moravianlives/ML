namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.model
{
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