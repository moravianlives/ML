using edu.bucknell.project.moravianLives.model.Common.Resolve;
using System.Linq;

namespace edu.bucknell.project.moravianLives.model.Common.Reference
{
    public class LocationReference : ModelReference<Location, Location.ExternalReferences, LocationResolve>
    {
        public LocationReference()
        {
            GetReference(query =>
            {
                var (domain, identifier, referenceModel) = query;

                var targetModel = CacheFetch(domain, identifier);

                if (targetModel != null)
                {
                    if (referenceModel?.Name != null) targetModel.Name ??= referenceModel.Name;
                    return targetModel;
                }

                var referenceProbe = Location.ExternalReferences.Where(i =>
                    i.References.Any(j =>
                        j.Domain == domain &&
                        j.Key == identifier)).FirstOrDefault();

                // This will fetch the target model OR create an empty one. Let's also use the local DataSets so we can preserve any changes.

                if (referenceProbe != null)
                    targetModel = Set.Fetch(referenceProbe?.Id);

                else
                    targetModel = Set.Fetch(Resolve(referenceModel));




                // Default name? Apply.
                if (referenceModel?.Name != null) targetModel.Name ??= referenceModel.Name;

                //Let's check/save the reference too.
                var targetReference = ReferenceSet.Fetch(targetModel.Id);
                targetReference.FetchReference(domain, identifier);

                CacheStore(domain, identifier, targetModel);

                return targetModel;
            });
        }
    }
}