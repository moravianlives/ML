using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Zen.Pebble.FlexibleData.Common.Interface;
using Zen.Pebble.FlexibleData.Culture;
using Zen.Pebble.FlexibleData.Historical;
using Zen.Pebble.FlexibleData.String.Localization.Concrete;
using Zen.Pebble.FlexibleData.String.Localization.Interface;

namespace Zen.Pebble.FlexibleData.String.Localization
{
    public class OfficeString : VariantTemporalMap<string, string>
    {

        public string titleRef;
        public string placeName;
        public string placeRef;
        public string whenIso;
        public string notBefore;
        public string notAfter;
        public string from;
        public string to;

        private CultureInfo _culture = CultureInfo.CurrentCulture;
        public OfficeString() { }

        public OfficeString(string source, string culture = null, string comments = null)
        {
            _culture = culture.ToCultureInfo() ?? CultureInfo.CurrentCulture;
            SetVariant(source, culture, comments);
        }

        public string Value
        {
            get
            {
                // First case: Do we have any entries at all?
                if (Variants == null) return null;
                if (Variants.Count == 0) return null;

                // DO we have a variant for the current culture? Otherwise pick whatever we have.
                return Variants.ContainsKey(_culture.Name) ? Variants[_culture.Name].Variants.FirstOrDefault()?.Value : Variants.FirstOrDefault().Value.Variants.FirstOrDefault()?.Value;
            }
            set
            {
                // Auto-resolved, so ignore. Setter preserved for serialization purposes.
            }
        }

        public OfficeString SetVariant(string value, string culture = null, string placeName = null, string titleRef = null, string placeRef = null, string whenIso = null, string notBefore = null, string notAfter = null, string from = null, string to = null)
        {
            if (value == null || placeName == null) return null;

            if (string.IsNullOrEmpty(value?.Trim())) value = null;
            if (string.IsNullOrEmpty(placeName?.Trim())) placeName = null;

            value = value?.Trim();
            placeName = placeName?.Trim();

            if (Variants == null) Variants = new Dictionary<string, VariantList<TemporalCommented<string>>>();
            var cultureProbe = culture.ToCultureInfo()?.Name ?? _culture.Name;
            TemporalCommented<string> targetEntry = Variants[cultureProbe].Variants.FirstOrDefault(i => i.Value?.Equals(value) == true);
            targetEntry.Value = value;
            return this;
        }

    }
}
