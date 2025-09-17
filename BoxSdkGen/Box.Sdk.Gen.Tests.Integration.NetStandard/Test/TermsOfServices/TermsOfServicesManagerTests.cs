using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class TermsOfServicesManagerTests {
        public BoxClient client { get; }

        public TermsOfServicesManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetTermsOfServices() {
            TermsOfService tos = await new CommonsManager().GetOrCreateTermsOfServicesAsync();
            TermsOfService updatedTos1 = await client.TermsOfServices.UpdateTermsOfServiceByIdAsync(termsOfServiceId: tos.Id, requestBody: new UpdateTermsOfServiceByIdRequestBody(status: UpdateTermsOfServiceByIdRequestBodyStatusField.Disabled, text: "TOS"));
            Assert.IsTrue(StringUtils.ToStringRepresentation(updatedTos1.Status?.Value) == "disabled");
            Assert.IsTrue(updatedTos1.Text == "TOS");
            TermsOfService updatedTos2 = await client.TermsOfServices.UpdateTermsOfServiceByIdAsync(termsOfServiceId: tos.Id, requestBody: new UpdateTermsOfServiceByIdRequestBody(status: UpdateTermsOfServiceByIdRequestBodyStatusField.Disabled, text: "Updated TOS"));
            Assert.IsTrue(StringUtils.ToStringRepresentation(updatedTos2.Status?.Value) == "disabled");
            Assert.IsTrue(updatedTos2.Text == "Updated TOS");
            TermsOfServices listTos = await client.TermsOfServices.GetTermsOfServiceAsync();
            Assert.IsTrue(NullableUtils.Unwrap(listTos.TotalCount) > 0);
        }

    }
}