using edu.bucknell.project.moravianLives.model.Common;
using System.Linq;
using Zen.Base.Common;

namespace edu.bucknell.project.moravianLives.model.Process
{
    [Priority(Level = -999)]
    public class ComposePersonFacts : IMoravianLivesDataOnboarding
    {
        public void Run()
        {
            // Compile all data for all People, and create the Facts.

            var people = Person.All();

            foreach (var person in people)
            {
                var events = Event.Where(i => i.Roles.Any(j => j.PersonId == person.Id));

            }


        }
    }
}
