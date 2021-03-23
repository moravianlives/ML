using edu.bucknell.project.moravianLives.model.Common.Resolve;
using System;
using System.Collections.Generic;
using Zen.Base.Extension;
using Zen.Base.Module;
using Zen.Base.Module.Data.CommonAttributes;

namespace edu.bucknell.project.moravianLives.model.Common.Reference
{
    public class ModelReference<T, TU, RU> where T : Data<T>, IDataId where TU : Data<TU>, IDataId where RU: ModelResolve<T>
    {
        private readonly Dictionary<string, T> _cache = new Dictionary<string, T>();
        public Set<TU> ReferenceSet = new Set<TU>();
        public Set<T> Set = new Set<T>();
        public RU Resolver = typeof(RU).CreateInstance<RU>();

        private Func<(string Domain, string Identifier, T defaultModel), T> LinkedReferenceFunction { get; set; }

        public ModelReference<T, TU, RU> GetReference(Func<(string Domain, string Identifier, T defaultModel), T> function)
        {
            LinkedReferenceFunction = function;
            return this;
        }

        public T Resolve(T model)
        {
            return Resolver.Resolve(model);
        }

        public T GetReference(string domain, string identifier)
        {
            return LinkedReferenceFunction((domain, identifier, null));
        }
        public T GetReference(string domain, string identifier, T defaultModel)
        {
            return LinkedReferenceFunction((domain, identifier, defaultModel));
        }

        private string DomainKeyPair(string domain, string identifier)
        {
            return domain + "|" + identifier;
        }

        public T CacheFetch(string domain, string identifier)
        {
            var key = DomainKeyPair(domain, identifier);
            return _cache.ContainsKey(key) ? _cache[key] : null;
        }

        public void CacheStore(string domain, string identifier, T model)
        {
            _cache[DomainKeyPair(domain, identifier)] = model;
            Set.Store(model);
        }

        public void Save()
        {
            Set.Save();
            ReferenceSet.Save();
        }
    }
}