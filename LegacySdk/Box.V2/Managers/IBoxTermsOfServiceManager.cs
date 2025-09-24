using System;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the terms of service endpoints.
    /// </summary>
    public interface IBoxTermsOfServiceManager
    {
        /// <summary>
        /// Retrieves information about a terms of service by ID.
        /// </summary>
        /// <param name="tosId">Id of the terms of service.</param>
        /// <returns>A full terms of service object is returned if the id is valid and if the user has access to the terms of service.</returns>
        Task<BoxTermsOfService> GetTermsOfServicesByIdAsync(string tosId);

        /// <summary>
        /// Retrieves information about all terms of service
        /// </summary>
        /// <param name="tosType">The type of terms of service to be retrieved - managed or external.</param>
        /// <returns>A a collection of terms of service objects is returned if the id if the user has access.</returns>
        Task<BoxTermsOfServiceCollection<BoxTermsOfService>> GetTermsOfServicesAsync(string tosType = null);

        /// <summary>
        /// Create a terms of service object.
        /// </summary>
        /// <param name="termsOfServicesRequest">Terms of services request object in order to create a terms of service object.</param>
        /// <returns>The terms of service object that was successfully created.</returns>
        Task<BoxTermsOfService> CreateTermsOfServicesAsync(BoxTermsOfServicesRequest termsOfServicesRequest);

        /// <summary>
        /// Updates current information on a terms of service object.
        /// </summary>
        /// <param name="tosId">The terms of service id.</param>
        /// <param name="termsOfServicesRequest">The update session request for new terms of service.</param>
        /// <returns>The updated session information for terms of service object.</returns>
        Task<BoxTermsOfService> UpdateTermsOfServicesAsync(string tosId, BoxTermsOfServicesRequest termsOfServicesRequest);

        /// <summary>
        /// Retrieves information on the user status of the terms of service.
        /// </summary>
        /// <param name="tosId">The terms of service id.</param>
        /// <param name="userId">The user id, if null this will default to current user.</param>
        /// <returns>The user status for terms of service objects.</returns>
        Task<BoxTermsOfServiceUserStatusesCollection<BoxTermsOfServiceUserStatuses>> GetTermsOfServiceUserStatusesAsync(string tosId, string userId = null);

        /// <summary>
        /// Create a terms of service status for user.
        /// </summary>
        /// <param name="termsOfServiceUserStatusCreateRequest">The request object for terms of service user status.</param>
        /// <returns>The status of the terms of service for a user.</returns>
        Task<BoxTermsOfServiceUserStatuses> CreateBoxTermsOfServiceUserStatusesAsync(BoxTermsOfServiceUserStatusCreateRequest termsOfServiceUserStatusCreateRequest);

        /// <summary>
        /// Updates information on a terms of service for a user.
        /// </summary>
        /// <param name="tosId">The terms of service id.</param>
        /// <param name="isAccepted">The current state of the terms of service.</param>
        /// <returns>The updated session information for a terms of service user status.</returns>
        Task<BoxTermsOfServiceUserStatuses> UpdateTermsofServiceUserStatusesAsync(string tosId, bool isAccepted);
    }
}
