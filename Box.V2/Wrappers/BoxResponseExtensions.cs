using Box.V2.Converter;
using Box.V2.Exceptions;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

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


        /// <summary>
        /// Attempt to extract the number of pages in a preview from the HTTP response headers. The response contains a "Link" 
        /// element in the header that includes a link to the last page of the preview. This method uses that information
        /// to extract the total number of pages
        /// </summary>
        /// <param name="response">The http response that includes total page information in its header</param>
        /// <returns>Total number of pages in the preview</returns>
        internal static int BuildPagesCount<T>(this IBoxResponse<T> response) where T : class
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
