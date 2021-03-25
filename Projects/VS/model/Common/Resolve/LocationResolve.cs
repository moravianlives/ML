using System.Linq;

namespace edu.bucknell.project.moravianLives.model.Common.Resolve
{
    public class LocationResolve : ModelResolve<Location>
    {
        public LocationResolve()
        {
            Resolve(referenceModel =>
            {
                Location targetModel = null;

                if (referenceModel == null) return null;

                // Does the Reference model have an Id? Search for it.
                if (referenceModel?.Id != null) {
                    targetModel =  Location.Get(referenceModel.Id);
                    if (targetModel != null) return targetModel;
                }

                // Ok, so no ID. Search time!
                // Simplest search is by similar name and Parent.

                if (referenceModel.Name?.Value != null && referenceModel.Parent?.Value != null)
                    targetModel = Location.Where(i => i.Name.Value == referenceModel.Name.Value && i.Parent.Value == referenceModel.Parent.Value).FirstOrDefault();

                if (targetModel != null) return targetModel;

                // Name, regardless of culture.
                targetModel = Location.Where(i => i.Name.Value == referenceModel.Name.Value).FirstOrDefault();

                if (targetModel != null) return targetModel;

                referenceModel = referenceModel.Save();

                return referenceModel;
            });
        }
    }
}
