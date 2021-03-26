using System.Collections.Generic;
using System.IO;
using Zen.Base.Extension;
using Zen.Base.Module;

namespace edu.bucknell.project.moravianLives.model.Extensions
{
    public static class Common
    {
        public static void TryAdd<T>(this List<T> collection, T content)
        {
            if (collection == null) throw new InvalidDataException(nameof(collection) + " can't be null.");
            if (!collection.Contains(content)) collection.Add(content);
        }

        public static void TryAdd<T>(this List<T> collection, List<T> content)
        {
            foreach (var i in content) collection.TryAdd(i);
        }

        public static string ModelHash<T>(this T source) where T : Data<T> { return source.ToJson().Sha512Hash(); }

        public static void AddFact(this IFacts factSource, string key, string value)
        {
            if (key == null) return;

            if (value == null)
                if (factSource.Facts.ContainsKey(key))
                {
                    factSource.Facts.Remove(key);
                    return;
                }


            factSource.Facts[key] = value;
        }

        public static string CleanString(this string source)
        {
            if (source is null) return null;

            var probe = source.Trim()
                .Replace('\n', ' ')
                .Replace('\t', ' ')
                .Replace("  ", " ")
                .Replace("  ", " ")
                .Replace("  ", " ")
                .Replace("  ", " ");

            return probe;
        }
    }
}