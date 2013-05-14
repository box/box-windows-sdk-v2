using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Web
{
    public static class BoxRequestExtensions
    {
        public static T Param<T>(this T query, string name, string value) where T : IBoxRequest
        {
            query.Parameters[name] = value;

            return query;
        }

        public static T Header<T>(this T query, string name, string value) where T : IBoxRequest
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            query.HttpHeaders.Add(new KeyValuePair<string, string>(name, value));

            return query;
        }

        public static T Method<T>(this T query, RequestMethod method) where T : IBoxRequest
        {
            query.Method = method;

            return query;
        }
    }
}
