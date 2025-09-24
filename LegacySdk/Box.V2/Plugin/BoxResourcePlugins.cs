using System;
using System.Collections.Generic;
using Box.V2.Extensions;

namespace Box.V2.Plugins
{

    public class BoxResourcePlugins
    {
        private readonly Dictionary<Type, ResourceBuilder> _plugins = new Dictionary<Type, ResourceBuilder>();
        private readonly object _lock = new object();

        public void Register<T>(Func<T> func)
            where T : class
        {
            lock (_lock)
            {
                _plugins.Add(typeof(T), new ResourceBuilder(func));
            }
        }

        public T Get<T>()
        {
            return !_plugins.TryGetValue(typeof(T), out ResourceBuilder builder)
                ? throw new InvalidOperationException(string.Format("The resource {0} has not been registered", typeof(T).Name))
                : (T)builder.Resource;
        }

        /// <summary>
        /// Builder that allows for lazy creation of the resource
        /// </summary>
        protected class ResourceBuilder : IDisposable
        {
            private Func<object> _func;
            private object _cachedValue;

            public ResourceBuilder(Func<object> resourceBuilder)
            {
                resourceBuilder.ThrowIfNull("resourceBuilder");

                _func = resourceBuilder;
            }

            public object Resource
            {
                get
                {
                    return _cachedValue ?? (_cachedValue = _func());
                }
            }

            public void Dispose()
            {
                _func = null;
            }
        }

    }
}
