using System;
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
    /// The manager that represents all of the terms of service endpoints.
    /// </summary>
    public class BoxTermsOfServiceManager : BoxResourceManager, IBoxTermsOfServiceManager
    {
        public BoxTermsOfServiceManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }


        /// <summary>
        /// Retrieves information about a terms of service by ID.
        /// </summary>
        /// <param name="tosId">Id of the terms of service.</param>
        /// <returns>A full terms of service object is returned if the id is valid and if the user has access to the terms of service.</returns>
        public async Task<BoxTermsOfService> GetTermsOfServicesByIdAsync(string tosId)
        {
            tosId.ThrowIfNullOrWhiteSpace("tosId");

            var request = new BoxRequest(_config.TermsOfServicesUri, tosId);

            IBoxResponse<BoxTermsOfService> response = await ToResponseAsync<BoxTermsOfService>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves information about all terms of service
        /// </summary>
        /// <param name="tosType">The type of terms of service to be retrieved - managed or external.</param>
        /// <returns>A a collection of terms of service objects is returned if the id if the user has access.</returns>
        public async Task<BoxTermsOfServiceCollection<BoxTermsOfService>> GetTermsOfServicesAsync(string tosType = null)
        {
            BoxRequest request = new BoxRequest(_config.TermsOfServicesUri)
                .Param("tos_type", tosType);

            IBoxResponse<BoxTermsOfServiceCollection<BoxTermsOfService>> response = await ToResponseAsync<BoxTermsOfServiceCollection<BoxTermsOfService>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Create a terms of service object.
        /// </summary>
        /// <param name="termsOfServicesRequest">Terms of services request object in order to create a terms of service object.</param>
        /// <returns>The terms of service object that was successfully created.</returns>
        public async Task<BoxTermsOfService> CreateTermsOfServicesAsync(BoxTermsOfServicesRequest termsOfServicesRequest)
        {
            BoxRequest request = new BoxRequest(_config.TermsOfServicesUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize<BoxTermsOfServicesRequest>(termsOfServicesRequest));

            IBoxResponse<BoxTermsOfService> response = await ToResponseAsync<BoxTermsOfService>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Updates current information on a terms of service object.
        /// </summary>
        /// <param name="tosId">The terms of service id.</param>
        /// <param name="termsOfServicesRequest">The update session request for new terms of service.</param>
        /// <returns>The updated session information for terms of service object.</returns>
        public async Task<BoxTermsOfService> UpdateTermsOfServicesAsync(string tosId, BoxTermsOfServicesRequest termsOfServicesRequest)
        {
            BoxRequest request = new BoxRequest(_config.TermsOfServicesUri, tosId)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize<BoxTermsOfServicesRequest>(termsOfServicesRequest));

            IBoxResponse<BoxTermsOfService> response = await ToResponseAsync<BoxTermsOfService>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves information on the user status of the terms of service.
        /// </summary>
        /// <param name="tosId">The terms of service id.</param>
        /// <param name="userId">The user id, if null this will default to current user.</param>
        /// <returns>The user status for terms of service objects.</returns>
        public async Task<BoxTermsOfServiceUserStatusesCollection<BoxTermsOfServiceUserStatuses>> GetTermsOfServiceUserStatusesAsync(string tosId, string userId = null)
        {
            tosId.ThrowIfNullOrWhiteSpace("tosId");

            BoxRequest request = new BoxRequest(_config.TermsOfServiceUserStatusesUri)
                .Param("tos_id", tosId)
                .Param("user_id", userId);

            IBoxResponse<BoxTermsOfServiceUserStatusesCollection<BoxTermsOfServiceUserStatuses>> response = await ToResponseAsync<BoxTermsOfServiceUserStatusesCollection<BoxTermsOfServiceUserStatuses>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Create a terms of service status for user.
        /// </summary>
        /// <param name="termsOfServiceUserStatusCreateRequest">The request object for terms of service user status.</param>
        /// <returns>The status of the terms of service for a user.</returns>
        public async Task<BoxTermsOfServiceUserStatuses> CreateBoxTermsOfServiceUserStatusesAsync(BoxTermsOfServiceUserStatusCreateRequest termsOfServiceUserStatusCreateRequest)
        {
            termsOfServiceUserStatusCreateRequest.ThrowIfNull("TermsOfService");
            termsOfServiceUserStatusCreateRequest.ThrowIfNull("User");

            BoxRequest request = new BoxRequest(_config.TermsOfServiceUserStatusesUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize<BoxTermsOfServiceUserStatusCreateRequest>(termsOfServiceUserStatusCreateRequest));

            IBoxResponse<BoxTermsOfServiceUserStatuses> response = await ToResponseAsync<BoxTermsOfServiceUserStatuses>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Updates information on a terms of service for a user.
        /// </summary>
        /// <param name="tosId">The terms of service id.</param>
        /// <param name="isAccepted">The current state of the terms of service.</param>
        /// <returns>The updated session information for a terms of service user status.</returns>
        public async Task<BoxTermsOfServiceUserStatuses> UpdateTermsofServiceUserStatusesAsync(string tosId, bool isAccepted)
        {
            tosId.ThrowIfNullOrWhiteSpace("tosId");

            BoxRequest request = new BoxRequest(_config.TermsOfServiceUserStatusesUri, tosId)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(new BoxTermsOfServiceUserStatusesRequest() { IsAccepted = isAccepted }));

            IBoxResponse<BoxTermsOfServiceUserStatuses> response = await ToResponseAsync<BoxTermsOfServiceUserStatuses>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
    }
}
