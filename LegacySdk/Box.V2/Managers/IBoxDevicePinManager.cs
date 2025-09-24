using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    public interface IBoxDevicePinManager
    {
        /// <summary>
        /// Gets all the device pins within a given enterprise. Must be an enterprise admin with the manage enterprise scope to make this call.
        /// </summary>
        /// <param name="enterpriseId">Box enterprise id.</param>
        /// <param name="marker">Needs not be passed or can be empty for first invocation of the API. Use the one returned in response for each subsequent call.</param>
        /// <param name="limit">Default value is 100. Max value is 10000.</param>
        /// <param name="direction">Default is "asc". Valid values are asc, desc. Case in-sensitive, ASC/DESC works just fine.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns all the device pins within a given enterprise up to limit amount.</returns>
        Task<BoxCollectionMarkerBased<BoxDevicePin>> GetEnterpriseDevicePinsAsync(string enterpriseId, string marker = null,
            int limit = 100,
            BoxSortDirection direction = BoxSortDirection.ASC,
            bool autoPaginate = false);

        /// <summary>
        /// Gets information about an individual device pin.
        /// </summary>
        /// <param name="id">Device pin id.</param>
        /// <returns>Information about the device pin.</returns>
        Task<BoxDevicePin> GetDevicePin(string id);

        /// <summary>
        /// Delete individual device pin.
        /// </summary>
        /// <param name="id">Device pin id.</param>
        /// <returns>True if successfully deleted.</returns>
        Task<bool> DeleteDevicePin(string id);
    }
}
