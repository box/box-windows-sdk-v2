using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;

namespace Box.V2.Managers
{
    public class BoxDevicePinManager : BoxResourceManager, IBoxDevicePinManager
    {
        public BoxDevicePinManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Gets all the device pins within a given enterprise. Must be an enterprise admin with the manage enterprise scope to make this call.
        /// </summary>
        /// <param name="enterpriseId">Box enterprise id.</param>
        /// <param name="marker">Needs not be passed or can be empty for first invocation of the API. Use the one returned in response for each subsequent call.</param>
        /// <param name="limit">Default value is 100. Max value is 10000.</param>
        /// <param name="direction">Default is "asc". Valid values are asc, desc. Case in-sensitive, ASC/DESC works just fine.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns all the device pins within a given enterprise up to limit amount.</returns>
        public async Task<BoxCollectionMarkerBased<BoxDevicePin>> GetEnterpriseDevicePinsAsync(string enterpriseId, string marker = null,
                                                                                               int limit = 100,
                                                                                               BoxSortDirection direction = BoxSortDirection.ASC,
                                                                                               bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.EnterprisesUri, string.Format(Constants.GetEnterpriseDevicePinsPathString, enterpriseId))
                .Param("limit", limit.ToString())
                .Param("marker", marker)
                .Param("direction", direction.ToString());

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxDevicePin>(request, limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxDevicePin>> response = await ToResponseAsync<BoxCollectionMarkerBased<BoxDevicePin>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Gets information about an individual device pin.
        /// </summary>
        /// <param name="id">Device pin id.</param>
        /// <returns>Information about the device pin.</returns>
        public async Task<BoxDevicePin> GetDevicePin(string id)
        {
            var request = new BoxRequest(_config.DevicePinUri, id);

            IBoxResponse<BoxDevicePin> response = await ToResponseAsync<BoxDevicePin>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Delete individual device pin.
        /// </summary>
        /// <param name="id">Device pin id.</param>
        /// <returns>True if successfully deleted.</returns>
        public async Task<bool> DeleteDevicePin(string id)
        {
            BoxRequest request = new BoxRequest(_config.DevicePinUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxDevicePin> response = await ToResponseAsync<BoxDevicePin>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

    }
}
