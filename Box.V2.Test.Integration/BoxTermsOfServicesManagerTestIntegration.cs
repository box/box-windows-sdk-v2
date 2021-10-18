using System;
using System.Threading.Tasks;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxTermsOfServicesManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        // create terms of service
        [TestMethod]
        public async Task CreateTermsOfServices_ValidResponse()
        {
            var termsOfService = await Client.TermsOfServiceManager.CreateTermsOfServicesAsync(new BoxTermsOfServicesRequest
            {
                Status = "enabled",
                TosType = "managed",
                Text = "Test Text"
            });

            Assert.IsNotNull(termsOfService, "Terms of Service was not created");
        }

        // retrieve all terms of services
        [TestMethod]
        public async Task GetTermsOfServices_ValidResponse()
        {
            var termsOfService = await Client.TermsOfServiceManager.GetTermsOfServicesAsync();
            Assert.IsNotNull(termsOfService, "Returned Terms of Service successfully");
        }

        // retrieve terms of service by id
        [TestMethod]
        [Obsolete]
        public async Task GetTermsOfServicesById_ValidResponse()
        {
            var termsOfService = await Client.TermsOfServiceManager.GetTermsOfServicesByIdAsync("2778");
            Assert.IsNotNull(termsOfService, "Successfully returned Terms of Service with specified ID");

            var termsOfServiceUserStatuses = await Client.TermsOfServiceManager.CreateBoxTermsOfServiceUserStatusesAsync(new BoxTermsOfServiceUserStatusesRequest
            {
                TermsOfService = termsOfService,
                IsAccepted = true
            });
            Assert.IsNotNull(termsOfServiceUserStatuses, "User Status for Terms of Service was successfully created");
        }

        // update terms of service information
        [TestMethod]
        public async Task UpdateTermsOfServices_ValidReponse()
        {
            var termsOfService = await Client.TermsOfServiceManager.UpdateTermsOfServicesAsync("2778", new BoxTermsOfServicesRequest
            {
                Status = "enabled",
                Text = "Test Text"
            });
            Assert.AreEqual(termsOfService.Text, "Test Text");
        }

        // get terms of user status for terms of service
        [TestMethod]
        public async Task GetTermsOfServiceUserStatuses_ValidResponse()
        {
            var termsOfServiceUserStatuses = await Client.TermsOfServiceManager.GetTermsOfServiceUserStatusesAsync("2778");
            Assert.IsNotNull(termsOfServiceUserStatuses, "Returned user status for Terms of Service successfully");
        }

        // update user status on terms of service
        [TestMethod]
        public async Task UpdateTermsOfServiceUserStatuses_ValidResponse()
        {
            var termsOfServiceUserStatuses = await Client.TermsOfServiceManager.UpdateTermsofServiceUserStatusesAsync("1939280", true);
            Assert.AreEqual(termsOfServiceUserStatuses.IsAccepted, true);
        }
    }
}
