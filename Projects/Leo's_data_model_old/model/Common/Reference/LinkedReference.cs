using System;
using System.Collections.Generic;
using Zen.Base.Module;

namespace edu.bucknell.project.moravianLives.model.Common.Reference
{
    public class LinkedReference<T, TU> where T : Data<T> where TU : Data<TU>
    {
        private readonly Dictionary<string, T> _cache = new Dictionary<string, T>();
        public Set<TU> ReferenceSet = new Set<TU>();
        public Set<T> Set = new Set<T>();

        private Func<(string Domain, string Identifier, string defaultLabel), T> LinkedReferenceFunction { get; set; }

        public LinkedReference<T, TU> ResolveReference(Func<(string Domain, string Identifier, string defaultLabel), T> function)
        {
            LinkedReferenceFunction = function;
            return this;
        }
        public T ResolveReference(string domain, string identifier)
        {
            return LinkedReferenceFunction((domain, identifier, null));
        }
        public T ResolveReference(string domain, string identifier, string defaultLabel)
        {
            return LinkedReferenceFunction((domain, identifier, defaultLabel));
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