using edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.Sample;
using edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub.tracker;

namespace edu.bucknell.project.moravianLives.provider.Bucknell_MoravianLives_GitHub
{
    public class GitHubImport 
    {
        public void Run()
        {

            new Placeography().Run();

            //Person.RemoveAll();
            //Person.ExternalReferences.RemoveAll();

            new Personography().Run();


            new Memoirs().Run();

            new SampleImport().Run();



            // new PersonTracker().Process();
        }
    }
}