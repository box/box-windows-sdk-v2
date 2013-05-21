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

        Task<OAuthSession> AuthenticateAsync(string authCode);

        Task<OAuthSession> RefreshAccessTokenAsync(string accessToken);

        Task LogoutAsync();

        Uri AuthCodeUri { get; }
    }
}
