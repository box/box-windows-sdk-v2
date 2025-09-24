using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Services;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the sign templates endpoints.
    /// </summary>
    public class BoxSignTemplatesManager : BoxResourceManager, IBoxSignTemplatesManager
    {
        public BoxSignTemplatesManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Retrieves information about a sign template by ID.
        /// </summary>
        /// <param name="signTemplateId">Id of the sign template.</param>
        /// <returns>A full SignTemplate object is returned if the id is valid and if the user has access to the sign template.</returns>
        public async Task<BoxSignTemplate> GetSignTemplateByIdAsync(string signTemplateId)
        {
            signTemplateId.ThrowIfNullOrWhiteSpace("signTemplateId");

            var request = new BoxRequest(_config.SignTemplatesEndpointWithPathUri, signTemplateId)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxSignTemplate> response = await ToResponseAsync<BoxSignTemplate>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves information about all sign templates.
        /// </summary>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="nextMarker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>A a collection of sign templates is returned if the id if the user has access.</returns>
        public async Task<BoxCollectionMarkerBased<BoxSignTemplate>> GetSignTemplatesAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.SignTemplatesEndpointUri)
                .Param("limit", limit.ToString())
                .Param("marker", nextMarker)
                .Method(RequestMethod.Get);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxSignTemplate>(request, limit).ConfigureAwait(false);
            }
            else
            {
                var response = await ToResponseAsync<BoxCollectionMarkerBased<BoxSignTemplate>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }
    }
}
