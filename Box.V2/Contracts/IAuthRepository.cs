using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Auth
{
    public interface IAuthRepository
    {
        OAuthSession Session { get; }

        Task<OAuthSession> Authenticate(string authCode);

        Task<OAuthSession> RefreshAccessToken(string accessToken);

        Uri AuthCodeUri { get; }
    }
}
