using Zen.Base.Common;
using Zen.Base.Extension;
using Zen.Base.Module;
using Zen.Pebble.FlexibleData.String.Localization.Concrete;
using Zen.Pebble.FlexibleData.String.Localization.Interface;

namespace edu.bucknell.project.moravianLives.model.Common
{
    public class HistoricalReferenceEntry<T> : ITemporalCommented<DataReference<T>> where T : Data<T>
    {
        public DataReference<T> Value { get; set; }
        public HistoricPeriod Period { get; set; }
        public string Comments { get; set; }

        public static implicit operator HistoricalReferenceEntry<T>(T source) => new HistoricalReferenceEntry<T> { Value = source.TypedReference() };
    }
}