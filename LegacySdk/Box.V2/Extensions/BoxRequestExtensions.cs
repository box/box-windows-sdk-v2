using System;
using System.Collections.Generic;
using Box.V2.Config;

namespace Box.V2.Extensions
{
    /// <summary>
    /// Extends the BoxRequest object with convenience methods
    /// </summary>
    public static class BoxRequestExtensions
    {
        public static T Param<T>(this T request, string name, string value) where T : IBoxRequest
        {
            name.ThrowIfNullOrWhiteSpace("name");

            // Don't add a parameter that does not have a value
            if (string.IsNullOrWhiteSpace(value))
                return request;

            request.Parameters[name] = value;

            return request;
        }

        public static T Param<T>(this T request, string name, IEnumerable<string> values) where T : IBoxRequest
        {
            name.ThrowIfNullOrWhiteSpace("name");

            // Don't add a parameter that does not have a value
            if (values != null)
            {
                // Rather than use an extention method to determine if the
                // collection has any values, do the join and check the
                // result - it'll be empty if there are no values.
                var joinedValues = string.Join(",", values);

                if (!string.IsNullOrEmpty(joinedValues))
                {
                    request.Parameters[name] = joinedValues;
                }
            }

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
            value.ThrowIfNullOrWhiteSpace("value");

            request.Payload = value;

            return request;
        }

        public static T Payload<T>(this T request, string name, string value) where T : IBoxRequest
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                return request;
            }

            request.PayloadParameters[name] = value;

            return request;
        }

        public static T Authorize<T>(this T request, string accessToken) where T : IBoxRequest
        {
            accessToken.ThrowIfNullOrWhiteSpace("accessToken");

            request.Authorization = accessToken;
            request.Header(Constants.AuthHeaderKey, accessToken);

            return request;
        }

        public static T FormPart<T>(this T request, IBoxFormPart formPart) where T : BoxMultiPartRequest
        {
            formPart.ThrowIfNull("formPart");

            request.Parts.Add(formPart);

            return request;
        }

        public static T Part<T>(this T request, IBoxPart part) where T : BoxBinaryRequest
        {
            request.Part = part;

            return request;
        }
    }
}
