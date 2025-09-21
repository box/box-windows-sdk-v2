using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class ShieldInformationBarrierReportsManagerTests {
        [RetryableTest]
        public async System.Threading.Tasks.Task TestShieldInformationBarrierReports() {
            BoxClient client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
            string enterpriseId = Utils.GetEnvVar(name: "ENTERPRISE_ID");
            ShieldInformationBarrier barrier = await new CommonsManager().GetOrCreateShieldInformationBarrierAsync(client: client, enterpriseId: enterpriseId);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(barrier.Status)) == "draft");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(barrier.Type)) == "shield_information_barrier");
            Assert.IsTrue(NullableUtils.Unwrap(barrier.Enterprise).Id == enterpriseId);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(barrier.Enterprise).Type) == "enterprise");
            string barrierId = NullableUtils.Unwrap(barrier.Id);
            ShieldInformationBarrierReports existingReports = await client.ShieldInformationBarrierReports.GetShieldInformationBarrierReportsAsync(queryParams: new GetShieldInformationBarrierReportsQueryParams(shieldInformationBarrierId: barrierId));
            if (NullableUtils.Unwrap(existingReports.Entries).Count > 0) {
                return;
            }
            ShieldInformationBarrierReport createdReport = await client.ShieldInformationBarrierReports.CreateShieldInformationBarrierReportAsync(requestBody: new ShieldInformationBarrierReference() { ShieldInformationBarrier = new ShieldInformationBarrierBase() { Id = barrierId, Type = ShieldInformationBarrierBaseTypeField.ShieldInformationBarrier } });
            Assert.IsTrue(NullableUtils.Unwrap(NullableUtils.Unwrap(createdReport.ShieldInformationBarrier).ShieldInformationBarrier).Id == barrierId);
            ShieldInformationBarrierReport retrievedReport = await client.ShieldInformationBarrierReports.GetShieldInformationBarrierReportByIdAsync(shieldInformationBarrierReportId: NullableUtils.Unwrap(createdReport.Id));
            Assert.IsTrue(retrievedReport.Id == createdReport.Id);
            ShieldInformationBarrierReports retrievedReports = await client.ShieldInformationBarrierReports.GetShieldInformationBarrierReportsAsync(queryParams: new GetShieldInformationBarrierReportsQueryParams(shieldInformationBarrierId: barrierId));
            Assert.IsTrue(NullableUtils.Unwrap(retrievedReports.Entries).Count > 0);
        }

    }
}