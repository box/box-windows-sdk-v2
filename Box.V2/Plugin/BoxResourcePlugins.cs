using Box.V2.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box.V2;
using Box.V2.Managers;

namespace Box.V2.Plugins
{

    public class BoxResourcePlugins
    {
        private readonly Dictionary<Type, ResourceBuilder> _plugins = new Dictionary<Type, ResourceBuilder>();
        private object _lock = new object();

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
            ResourceBuilder builder;
            if (!_plugins.TryGetValue(typeof(T), out builder))
                throw new InvalidOperationException(string.Format("The resource {0} has not been registered", typeof(T).Name));

            return (T)builder.Resource;
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
