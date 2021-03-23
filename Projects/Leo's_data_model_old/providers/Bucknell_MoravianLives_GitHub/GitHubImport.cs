using edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.tracker;

namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub
{
    public class GitHubImport : IMoravianLivesDataOnboarding
    {
        public void Import()
        {
            new Placeography().Run();

            new Personography().Run();

            // new PersonTracker().Process();
        }
    }
}