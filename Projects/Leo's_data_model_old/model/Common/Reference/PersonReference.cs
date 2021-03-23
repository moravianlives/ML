using System.Linq;

namespace edu.bucknell.project.moravianLives.model.Common.Reference
{
    public class PersonReference : LinkedReference<Person, Person.ExternalReferences>
    {
        public PersonReference()
        {
            ResolveReference(query =>
            {
                var (domain, identifier, defaultLabel) = query;

                var targetModel = CacheFetch(domain, identifier);

                if (targetModel != null)
                {
                    if (defaultLabel != null) targetModel.Name ??= defaultLabel;
                    return targetModel;
                }

                var referenceProbe = Person.ExternalReferences.Where(i =>
                    i.References.Any(j =>
                        j.Domain == domain &&
                        j.Key == identifier)).FirstOrDefault();

                // This will fetch the target model OR create an empty one. Let's also use the local DataSets so we can preserve any changes.
                targetModel = Set.Fetch(referenceProbe?.Id);

                // Default name? Save it as well.
                if (defaultLabel != null) targetModel.Name ??= defaultLabel;

                //Let's check/save the reference too.
                var targetReference = ReferenceSet.Fetch(targetModel.Id);
                targetReference.FetchReference(domain, identifier);

                CacheStore(domain, identifier, targetModel);

                return targetModel;
            });
        }
    }
}