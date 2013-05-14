using Box.V2.Auth;
using Box.V2.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Managers
{
    public class BoxSharedItemsManager : BoxResourceManager
    {
        public BoxSharedItemsManager(IBoxConfig boxConfig, IAuthRepository auth)
            : base(boxConfig, auth)
        {

        }

    }
}
