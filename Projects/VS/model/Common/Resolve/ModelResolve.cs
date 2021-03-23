using System;
using System.Collections.Generic;
using Zen.Base.Module;

namespace edu.bucknell.project.moravianLives.model.Common.Resolve
{
    public class ModelResolve<T> where T : Data<T>
    {
        private Func<T , T> ModelResolveFunction { get; set; }

        public ModelResolve<T> Resolve(Func<T , T> function)
        {
            ModelResolveFunction = function;
            return this;
        }

        public T Resolve(T defaultModel)
        {
            return ModelResolveFunction(defaultModel);
        }
    }
}