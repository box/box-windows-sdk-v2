using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class SignTemplatesManagerTests {
        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetSignTemplates() {
            BoxClient client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
            SignTemplates signTemplates = await client.SignTemplates.GetSignTemplatesAsync(queryParams: new GetSignTemplatesQueryParams() { Limit = 2 });
            Assert.IsTrue(NullableUtils.Unwrap(signTemplates.Entries).Count >= 0);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetSignTemplate() {
            BoxClient client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
            SignTemplates signTemplates = await client.SignTemplates.GetSignTemplatesAsync(queryParams: new GetSignTemplatesQueryParams() { Limit = 2 });
            Assert.IsTrue(NullableUtils.Unwrap(signTemplates.Entries).Count >= 0);
            if (NullableUtils.Unwrap(signTemplates.Entries).Count > 0) {
                SignTemplate signTemplate = await client.SignTemplates.GetSignTemplateByIdAsync(templateId: NullableUtils.Unwrap(NullableUtils.Unwrap(signTemplates.Entries)[0].Id));
                Assert.IsTrue(signTemplate.Id == NullableUtils.Unwrap(signTemplates.Entries)[0].Id);
                Assert.IsTrue(NullableUtils.Unwrap(signTemplate.SourceFiles).Count > 0);
                Assert.IsTrue(signTemplate.Name != "");
                Assert.IsTrue(NullableUtils.Unwrap(signTemplate.ParentFolder).Id != "");
            }
        }

    }
}