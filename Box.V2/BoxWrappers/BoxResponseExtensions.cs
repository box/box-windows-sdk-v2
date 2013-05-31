using Box.V2.Exceptions;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2
{
    public static class BoxResponseExtensions
    {
        public static IBoxResponse<T> ParseResults<T>(this IBoxResponse<T> response, IBoxConverter converter)
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
                        response.Error = converter.Parse<BoxError>(response.ContentString);
                        if (response.Error != null && !string.IsNullOrWhiteSpace(response.Error.Name))
                            throw new BoxException(string.Format("{0}: {1}", response.Error.Name, response.Error.Description));
                        throw new BoxException(response.ContentString);
                    }
                    break;
            }
            return response;
        }
    }
}
