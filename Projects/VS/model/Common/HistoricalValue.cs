using System.Collections.Generic;
using System.Linq;
using Zen.Pebble.FlexibleData.Common.Interface;

namespace edu.bucknell.project.moravianLives.model.Common {
    public class HistoricalValue<T> : IVariantList<HistoricalReferenceValue<T>> where T : class
    {
        public HistoricalValue() { } // Empty constructor for serialization purposes

        public HistoricalValue(T source) { Variants = new List<HistoricalReferenceValue<T>> { source }; }

        public T Value
        {
            get => Variants.FirstOrDefault()?.Value;
            // ReSharper disable once ValueParameterNotUsed
            // Ignored: This Setter exists for serialization purposes only (Mongo would ignore read-only properties).
            set { }
        }
        public List<HistoricalReferenceValue<T>> Variants { get; set; }

        public static implicit operator HistoricalValue<T>(T source) { return source == null ? null : new HistoricalValue<T>(source); }
    }
}