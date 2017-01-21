using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Services;
using Box.V2.Managers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Box.V2.Next.Managers
{
    /// <summary>
    /// File objects represent that metadata about individual files in Box, with attributes describing who created the file, 
    /// when it was last modified, and other information. 
    /// </summary>
    public class BoxFilesManagerV2 : BoxResourceManager
    {
        // public BoxFilesManagerV2(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
        public BoxFilesManagerV2(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Retrieves information about a file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="repHint"></param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A full file object is returned if the ID is valid and if the user has access to the file.</returns>
        public async Task<object> GetInformationAsync(string id, string repHints = null, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Param(ParamFields, fields)
                .Header(Constants.RequestParameters.XRepHints, repHints);

            IBoxResponse<object> response = await ToResponseAsync<object>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
    }
}
