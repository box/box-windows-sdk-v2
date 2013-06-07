using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2
{
    public static class BoxRequestExtensions
    {
        public static T Param<T>(this T request, string name, string value) where T : IBoxRequest
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            // Don't add a parameter that does not have a value
            if (string.IsNullOrWhiteSpace(value))
                return request;

            request.Parameters[name] = value;

            return request;
        }

        public static T Header<T>(this T request, string name, string value) where T : IBoxRequest
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            // Don't add a parameter that does not have a value
            if (string.IsNullOrWhiteSpace(value))
                return request;

            request.HttpHeaders[name] = value;

            return request;
        }

        public static T Method<T>(this T request, RequestMethod method) where T : IBoxRequest
        {
            request.Method = method;

            return request;
        }

        public static T Payload<T>(this T request, string value) where T : IBoxRequest
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            request.Payload = value;

            return request;
        }

        public static T Payload<T>(this T request, string name, string value) where T : IBoxRequest
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(value))
                return request;

            request.PayloadParameters[name] = value;

            return request;
        }

        public static T Authorize<T>(this T request, string accessToken) where T : IBoxRequest
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException();

            request.Authorization = accessToken;
            request.Header("Authorization", string.Format("Bearer {0}", accessToken));

            return request;
        }

        public static T FormPart<T>(this T request, IBoxFormPart formPart) where T : BoxMultiPartRequest
        {
            if (formPart == null)
                throw new ArgumentNullException();

            request.Parts.Add(formPart);

            return request;
        }


        /// <summary>
        /// Checks if the object is null 
        /// </summary>
        /// <typeparam name="T">Type of the object being checked</typeparam>
        /// <param name="param"></param>
        /// <param name="name"></param>
        public static T ThrowIfNull<T>(this T param, string name) where T : class
        {
            if (param == null)
                throw new ArgumentNullException(name);

            return param;
        }
    }
}
