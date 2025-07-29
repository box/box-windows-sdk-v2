using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class ShieldInformationBarriersManagerTests {
        [RetryableTest]
        public async System.Threading.Tasks.Task TestShieldInformationBarriers() {
            BoxClient client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
            string enterpriseId = Utils.GetEnvVar(name: "ENTERPRISE_ID");
            ShieldInformationBarrier barrier = await new CommonsManager().GetOrCreateShieldInformationBarrierAsync(client: client, enterpriseId: enterpriseId);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(barrier.Status)) == "draft");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(barrier.Type)) == "shield_information_barrier");
            Assert.IsTrue(NullableUtils.Unwrap(barrier.Enterprise).Id == enterpriseId);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(barrier.Enterprise).Type) == "enterprise");
            string barrierId = NullableUtils.Unwrap(barrier.Id);
            ShieldInformationBarrier barrierFromApi = await client.ShieldInformationBarriers.GetShieldInformationBarrierByIdAsync(shieldInformationBarrierId: barrierId);
            Assert.IsTrue(NullableUtils.Unwrap(barrierFromApi.Id) == barrierId);
            ShieldInformationBarriers barriers = await client.ShieldInformationBarriers.GetShieldInformationBarriersAsync();
            Assert.IsTrue(NullableUtils.Unwrap(barriers.Entries).Count == 1);
            await Assert.That.IsExceptionAsync(async() => await client.ShieldInformationBarriers.UpdateShieldInformationBarrierStatusAsync(requestBody: new UpdateShieldInformationBarrierStatusRequestBody(id: barrierId, status: UpdateShieldInformationBarrierStatusRequestBodyStatusField.Disabled)));
        }

    }
}