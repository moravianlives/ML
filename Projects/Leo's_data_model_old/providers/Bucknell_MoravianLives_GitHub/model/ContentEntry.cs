using Newtonsoft.Json.Linq;

namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.model
{
    public class ContentEntry
    {
        public string Id { get; set; }
        public JObject Contents { get; set; }
    }
}