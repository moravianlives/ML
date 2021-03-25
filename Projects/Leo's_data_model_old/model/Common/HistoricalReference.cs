using System.Collections.Generic;
using System.Linq;
using Zen.Base.Common;
using Zen.Base.Module;
using Zen.Pebble.FlexibleData.Common.Interface;

namespace edu.bucknell.project.moravianLives.model.Common
{
    public class HistoricalReference<T> : IVariant<HistoricalReferenceEntry<T>>, IValue<DataReference<T>> where T : Data<T>
    {
        public HistoricalReference() { } // Empty constructor for serialization purposes

        public HistoricalReference(T source) { Variants = new List<HistoricalReferenceEntry<T>> { source }; }

        public DataReference<T> Value
        {
            get => Variants?.FirstOrDefault()?.Value;
            // ReSharper disable once ValueParameterNotUsed
            // Ignored: This Setter exists for serialization purposes only (Mongo would ignores read-only properties).
            set { }
        }
        public List<HistoricalReferenceEntry<T>> Variants { get; set; }

        public static implicit operator HistoricalReference<T>(T source) { return source == null ? null : new HistoricalReference<T>(source); }

        public static implicit operator HistoricalReference<T>(string key) { return key == null ? null : new HistoricalReference<T>(Data<T>.Get(key)); }
    }
}