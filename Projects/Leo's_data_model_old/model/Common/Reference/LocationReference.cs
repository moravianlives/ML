using System.Linq;

namespace edu.bucknell.project.moravianLives.model.Common.Reference
{
    public class LocationReference : LinkedReference<Location, Location.ExternalReferences>
    {
        public LocationReference()
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

                var referenceProbe = Location.ExternalReferences.Where(i =>
                    i.References.Any(j =>
                        j.Domain == domain &&
                        j.Key == identifier)).FirstOrDefault();

                // This will fetch the target model OR create an empty one. Let's also use the local DataSets so we can preserve any changes.
                targetModel = Set.Fetch(referenceProbe?.Id);

                // Default name? Apply.
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