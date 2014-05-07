using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Exceptions
{
    /// <summary>
    /// An exception to represent that the current access/refresh tokens are in an 
    /// unrecoverable state. This can either be due to the tokens being revoked or expired.
    /// A new session must be created by going through the OAuth workflow again
    /// </summary>
    public class BoxSessionInvalidatedException : BoxException
    {

    }
}
