using Box.V2.Auth;
using Box.V2.Contracts;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Managers
{
    public class BoxFilesManager : BoxResourceManager
    {
        public BoxFilesManager(IBoxConfig boxConfig, IBoxService boxService, IAuthRepository auth)
            : base(boxConfig, auth)
        {

        }

    }
}
