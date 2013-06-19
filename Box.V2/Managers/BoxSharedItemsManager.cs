using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Services;

namespace Box.V2.Managers
{
    public class BoxSharedItemsManager : BoxResourceManager
    {
        public BoxSharedItemsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { } 
    }
}
