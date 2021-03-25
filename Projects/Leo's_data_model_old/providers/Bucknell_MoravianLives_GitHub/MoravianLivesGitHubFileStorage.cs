using Zen.Provider.GitHub.Storage;

namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub
{
    [GitHubFileStorageConfiguration(Owner = "moravianlives",
        Repository = "ML",
        Branch = "origin/master",
        Url = "https://github.com/moravianlives/ML",
        Token = "554257c25dcdb13e324e0925dd96a5efe8dbb831",
        UseSystemTempSpace = true,
        BasePath = "Projects/TEI_Memoirs")]
    public class MoravianLivesGitHubFileStorage : GitHubFileStorage
    {
    }
}