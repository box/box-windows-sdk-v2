using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public interface IAuthentication {
        System.Threading.Tasks.Task<AccessToken> RetrieveTokenAsync(NetworkSession networkSession = null);

        System.Threading.Tasks.Task<AccessToken> RefreshTokenAsync(NetworkSession networkSession = null);

        System.Threading.Tasks.Task<string> RetrieveAuthorizationHeaderAsync(NetworkSession networkSession = null);

        System.Threading.Tasks.Task RevokeTokenAsync(NetworkSession networkSession = null);

        System.Threading.Tasks.Task<AccessToken> DownscopeTokenAsync(IReadOnlyList<string> scopes, string resource = null, string sharedLink = null, NetworkSession networkSession = null);

    }
}