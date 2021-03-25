using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Zen.Base.Extension;
using Zen.Base.Module;
using Zen.Base.Module.Data;
using Zen.Base.Module.Data.Connection;
using Zen.Pebble.FlexibleData.String.Localization;

namespace edu.bucknell.project.moravianLives.model.Common
{
    [DataConfig(UseCaching = false)]
    public class Category<T, TU> : Data<Category<T, TU>>, IStorageCollectionResolver where T : Data<T>
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Display]
        public HistoricString Name { get; set; }
        public string Parent { get; set; }

        #region Implementation of IStorageCollectionResolver

        public string GetStorageCollectionName() { return $"{Info<T>.Settings.StorageCollectionName}.{typeof(TU).Name}"; }

        #endregion

        public static Category<T, TU> Fetch(string descriptor, Category<T, TU> parent = null)
        {
            // Handle hierarchical calls
            if (descriptor.LastIndexOf('.') != -1)
            {
                var last = descriptor.Substring(descriptor.LastIndexOf('.') + 1).TrimStart();

                var parentDescriptor = descriptor.Substring(0, descriptor.LastIndexOf('.'));
                parent = Fetch(parentDescriptor, parent);

                descriptor = last;
            }

            var key = descriptor.ToFriendlyUrl().Trim().ToLower();
            var result = Get(key);

            if (result != null) return result;

            result = new Category<T, TU> { Id = key, Name = descriptor, Parent = parent?.Id };
            result.Save();

            return result;
        }

        public Category<T, TU> FetchChild(string descriptor)
        {
            var key = descriptor.ToFriendlyUrl().Trim().ToLower();

            var result = Where(i => i.Id == key && i.Parent == Id).FirstOrDefault();

            return result ?? new Category<T, TU> { Id = key, Name = descriptor, Parent = Id }.Save();
        }
    }
}