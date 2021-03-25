using System.Linq;

namespace edu.bucknell.project.moravianLives.model.Common.Resolve
{
    public class PersonResolve : ModelResolve<Person>
    {
        public PersonResolve()
        {
            Resolve(referenceModel =>
            {
                Person targetModel = null;

                // Does the Reference model have an Id? Search for it.
                if (referenceModel.Id != null) {
                    targetModel =  Person.Get(referenceModel.Id);
                    if (targetModel != null) return targetModel;
                }

                // Ok, so no ID. Search time!
                // Name, regardless of culture.
                targetModel = Person.Where(i => i.Name.Value == referenceModel.Name.Value).FirstOrDefault();

                if (targetModel != null) return targetModel;

                referenceModel = referenceModel.Save();

                return referenceModel;
            });
        }
    }
}
