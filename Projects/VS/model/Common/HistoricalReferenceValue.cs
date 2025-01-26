using Zen.Pebble.FlexibleData.String.Localization.Concrete;
using Zen.Pebble.FlexibleData.String.Localization.Interface;
using Zen.Module.Data.MongoDB;
using Zen.Base.Module.Data;

namespace edu.bucknell.project.moravianLives.model.Common
{
    [DataConfigAttribute(ConnectionBundleType = typeof(MongoDbDefaultBundle))]
    [DataEnvironmentMappingAttribute(Origin = "prd", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "uat", Target = "dev")]
    [DataEnvironmentMappingAttribute(Origin = "STA", Target = "dev")]
    public class HistoricalReferenceValue<T> : ITemporalCommented<T>
    {
        public T Value { get; set; }
        public HistoricPeriod Period { get; set; }
        public string Comments { get; set; }

        public static implicit operator HistoricalReferenceValue<T>(T source) => new HistoricalReferenceValue<T> { Value = source };
    }
}