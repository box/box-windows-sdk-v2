using Box.V2.Auth;
using Box.V2.Contracts;
using Box.V2.Services;
using Box.V2.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Managers
{
    public class BoxFoldersManager : BoxResourceManager
    {
        IBoxService _service;

        public BoxFoldersManager(IBoxConfig boxConfig, IBoxService service, IAuthRepository auth)
            : base(boxConfig, auth)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves the files and/or folders contained in the provided folder id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        public async void GetFolderItems(string id, int limit, int offset = 0)
        {
            BoxRequest query = new BoxRequest(RequestMethod.GET, _boxConfig.BoxApiUri, string.Format(@"/folders/{0}/items", id))
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString());
            AddAuthentication(query);

            var response = await _service.ToResponse<object>(query);
        }
    }
}
