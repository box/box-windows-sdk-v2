using Box.V2.Converter;
using Box.V2.Exceptions;
using Box.V2.Services;
using System;
using System.Diagnostics;

namespace Box.V2
{
    /// <summary>
    /// Extends the BoxResponse class with convenience methods
    /// </summary>
    internal static class BoxResponseExtensions
    {
        /// <summary>
        /// Parses the BoxResponse with the provided converter
        /// </summary>
        /// <typeparam name="T">The return type of the Box response</typeparam>
        /// <param name="response">The response to parse</param>
        /// <param name="converter">The converter to use for the conversion</param>
        /// <returns></returns>
        internal static IBoxResponse<T> ParseResults<T>(this IBoxResponse<T> response, IBoxConverter converter)
            where T : class
        {
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    if (!string.IsNullOrWhiteSpace(response.ContentString))
                        response.ResponseObject = converter.Parse<T>(response.ContentString);
                    break;
                case ResponseStatus.Error:
                    if (!string.IsNullOrWhiteSpace(response.ContentString))
                    {
                        try
                        { 
                            response.Error = converter.Parse<BoxError>(response.ContentString);
                            
                            if (response.Error != null && !string.IsNullOrWhiteSpace(response.Error.Name))
                                throw new BoxException(string.Format("{0}: {1}", response.Error.Name, response.Error.Description)) { StatusCode = response.StatusCode };
                        }
                        catch (Exception)
                        {
                            Debug.WriteLine(string.Format("Unable to parse error message: {0}", response.ContentString));
                        }
                    }
                    throw new BoxException() { StatusCode = response.StatusCode };
            }
            return response;
        }
    }
}
