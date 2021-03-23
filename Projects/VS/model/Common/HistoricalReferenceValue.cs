using Zen.Pebble.FlexibleData.String.Localization.Concrete;
using Zen.Pebble.FlexibleData.String.Localization.Interface;

namespace edu.bucknell.project.moravianLives.model.Common
{
    public class HistoricalReferenceValue<T> : ITemporalCommented<T>
    {
        public T Value { get; set; }
        public HistoricPeriod Period { get; set; }
        public string Comments { get; set; }

        public static implicit operator HistoricalReferenceValue<T>(T source) => new HistoricalReferenceValue<T> { Value = source };
    }
}