using System;
using System.Collections.Generic;

namespace Box.V2
{
    /// <summary>
    /// Extends the BoxRequest object with convenience methods
    /// </summary>
    internal static class BoxRequestExtensions
    {
        internal static T Param<T>(this T request, string name, string value) where T : IBoxRequest
        {
            name.ThrowIfNullOrWhiteSpace("name");

            // Don't add a parameter that does not have a value
            if (string.IsNullOrWhiteSpace(value))
                return request;

            request.Parameters[name] = value;

            return request;
        }

        internal static T Param<T>(this T request, string name, List<string> values) where T : IBoxRequest
        {
            name.ThrowIfNullOrWhiteSpace("name");

            // Don't add a parameter that does not have a value
            if (values == null || values.Count == 0)
                return request;

            request.Parameters[name] = string.Join(",", values);

            return request;
        }

        internal static T Header<T>(this T request, string name, string value) where T : IBoxRequest
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            // Don't add a parameter that does not have a value
            if (string.IsNullOrWhiteSpace(value))
                return request;

            request.HttpHeaders[name] = value;

            return request;
        }

        internal static T Method<T>(this T request, RequestMethod method) where T : IBoxRequest
        {
            request.Method = method;

            return request;
        }

        internal static T Payload<T>(this T request, string value) where T : IBoxRequest
        {
            value.ThrowIfNullOrWhiteSpace("value");

            request.Payload = value;

            return request;
        }

        internal static T Payload<T>(this T request, string name, string value) where T : IBoxRequest
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(value))
                return request;

            request.PayloadParameters[name] = value;

            return request;
        }

        internal static T Authorize<T>(this T request, string accessToken) where T : IBoxRequest
        {
            accessToken.ThrowIfNullOrWhiteSpace("accessToken");

            request.Authorization = accessToken;
            request.Header("Authorization", string.Format("Bearer {0}", accessToken));

            return request;
        }

        internal static T FormPart<T>(this T request, IBoxFormPart formPart) where T : BoxMultiPartRequest
        {
            formPart.ThrowIfNull("formPart");

            request.Parts.Add(formPart);

            return request;
        }


        /// <summary>
        /// Checks if the object is null 
        /// </summary>
        /// <typeparam name="T">Type of the object being checked</typeparam>
        /// <param name="param"></param>
        /// <param name="name"></param>
        internal static T ThrowIfNull<T>(this T param, string name) where T : class
        {
            if (param == null)
                throw new ArgumentNullException(name);

            return param;
        }

        /// <summary>
        /// Checks if a string is null or whitespace
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static string ThrowIfNullOrWhiteSpace(this string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Required field cannot be null or whitespace", name);

            return value;
        }
    }
}
