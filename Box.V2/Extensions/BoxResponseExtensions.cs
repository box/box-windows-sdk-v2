using Box.V2.Converter;
using Box.V2.Exceptions;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Box.V2.Extensions
{
    /// <summary>
    /// Extends the BoxResponse class with convenience methods
    /// </summary>
    public static class BoxResponseExtensions
    {
        /// <summary>
        /// Parses the BoxResponse with the provided converter
        /// </summary>
        /// <typeparam name="T">The return type of the Box response</typeparam>
        /// <param name="response">The response to parse</param>
        /// <param name="converter">The converter to use for the conversion</param>
        /// <returns></returns>
        public static IBoxResponse<T> ParseResults<T>(this IBoxResponse<T> response, IBoxConverter converter)
            where T : class
        {
            BoxException exToThrow = null;

            switch (response.Status)
            {
                case ResponseStatus.Success:
                case ResponseStatus.Pending:
                    if (!string.IsNullOrWhiteSpace(response.ContentString))
                        response.ResponseObject = converter.Parse<T>(response.ContentString);
                    break;
                case ResponseStatus.Forbidden:
                    var errorMsg = response.Headers.WwwAuthenticate.FirstOrDefault();
                    if (errorMsg != null)
                    {
                        var err = new BoxError() { Code = response.StatusCode.ToString(), Description = "Forbidden", Message = errorMsg.ToString() };
                        throw new BoxException(err.Message, err);
                    }
                    else if (!string.IsNullOrWhiteSpace(response.ContentString))
                    {
                        response.Error = converter.Parse<BoxError>(response.ContentString);
                        throw new BoxException(response.ContentString, response.Error) {StatusCode = response.StatusCode};
                    }
                    else
                    {
                        throw new BoxException("Forbidden");
                    }
                default:
                    if (!string.IsNullOrWhiteSpace(response.ContentString))
                    {
                        try
                        {
                            switch (response.StatusCode)
                            {
                                case System.Net.HttpStatusCode.Conflict:
                                    if (response is IBoxResponse<BoxPreflightCheck>)
                                    {
                                        BoxPreflightCheckConflictError<BoxFile> err = converter.Parse<BoxPreflightCheckConflictError<BoxFile>>(response.ContentString);
                                        exToThrow = new BoxPreflightCheckConflictException<BoxFile>(response.ContentString, err);
                                    } else
                                    {
                                        BoxConflictError<T> error = converter.Parse<BoxConflictError<T>>(response.ContentString);
                                        exToThrow = new BoxConflictException<T>(response.ContentString, error);
                                    }
                                  
                                    break;
                                default:
                                    response.Error = converter.Parse<BoxError>(response.ContentString);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(string.Format("Unable to parse error message: {0}", response.ContentString));
                        }

                        throw exToThrow == null ?
                            new BoxException(response.ContentString, response.Error) { StatusCode = response.StatusCode } :
                            exToThrow;
                    }
                    throw new BoxException(response.ContentString) { StatusCode = response.StatusCode };
            }
            return response;
        }


        /// <summary>
        /// Attempt to extract the number of pages in a preview from the HTTP response headers. The response contains a "Link" 
        /// element in the header that includes a link to the last page of the preview. This method uses that information
        /// to extract the total number of pages
        /// </summary>
        /// <param name="response">The http response that includes total page information in its header</param>
        /// <returns>Total number of pages in the preview</returns>
        public static int BuildPagesCount<T>(this IBoxResponse<T> response) where T : class
        {
            int count = 1;
            IEnumerable<string> values = new List<string>();

            if (response.Headers.TryGetValues("link", out values)) // headers names are case-insensitve
            {
                var links = values.First().Split(',');
                var last = links.FirstOrDefault(x => x.ToUpperInvariant().Contains("REL=\"LAST\""));
                if (last != null)
                {
                    string lastPageLink = last.Split(';')[0];

                    Regex rgx = new Regex(@"page=[0-9]+", RegexOptions.IgnoreCase);
                    MatchCollection matches = rgx.Matches(lastPageLink);

                    if (matches.Count > 0)
                    {
                        try
                        {
                            count = Convert.ToInt32(matches[0].Value.Split('=')[1]);
                        }
                        catch (FormatException) { }
                    }
                }
            }
            return count;
        }
    }
}
