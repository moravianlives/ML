using System.Linq;

namespace edu.bucknell.project.moravianLives.model.Common.Resolve
{
    public class SourceResolve : ModelResolve<Source>
    {
        public SourceResolve()
        {
            Resolve(referenceModel =>
            {
                Source targetModel = null;

                // Does the Reference model have an Id? Search for it.
                if (referenceModel.Id != null)
                {
                    targetModel = Source.Get(referenceModel.Id);
                    if (targetModel != null) return targetModel;
                }

                // Ok, so no ID. Search time!
                // Name, regardless of culture.
                targetModel = Source.Where(i => i.Name.Value == referenceModel.Name.Value).FirstOrDefault();

                if (targetModel != null) return targetModel;

                referenceModel = referenceModel.Save();

                return referenceModel;
            });
        }
    }
}
