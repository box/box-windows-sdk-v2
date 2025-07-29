using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class ShieldInformationBarrierSegmentRestrictionsManagerTests {
        [RetryableTest]
        public async System.Threading.Tasks.Task TestShieldInformationBarrierSegmentRestrictions() {
            BoxClient client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
            string enterpriseId = Utils.GetEnvVar(name: "ENTERPRISE_ID");
            ShieldInformationBarrier barrier = await new CommonsManager().GetOrCreateShieldInformationBarrierAsync(client: client, enterpriseId: enterpriseId);
            string barrierId = NullableUtils.Unwrap(barrier.Id);
            ShieldInformationBarrierSegment segment = await client.ShieldInformationBarrierSegments.CreateShieldInformationBarrierSegmentAsync(requestBody: new CreateShieldInformationBarrierSegmentRequestBody(shieldInformationBarrier: new ShieldInformationBarrierBase() { Id = barrierId, Type = ShieldInformationBarrierBaseTypeField.ShieldInformationBarrier }, name: Utils.GetUUID()) { Description = "barrier segment description" });
            string segmentId = NullableUtils.Unwrap(segment.Id);
            ShieldInformationBarrierSegment segmentToRestrict = await client.ShieldInformationBarrierSegments.CreateShieldInformationBarrierSegmentAsync(requestBody: new CreateShieldInformationBarrierSegmentRequestBody(shieldInformationBarrier: new ShieldInformationBarrierBase() { Id = barrierId, Type = ShieldInformationBarrierBaseTypeField.ShieldInformationBarrier }, name: Utils.GetUUID()) { Description = "barrier segment description" });
            string segmentToRestrictId = NullableUtils.Unwrap(segmentToRestrict.Id);
            ShieldInformationBarrierSegmentRestriction segmentRestriction = await client.ShieldInformationBarrierSegmentRestrictions.CreateShieldInformationBarrierSegmentRestrictionAsync(requestBody: new CreateShieldInformationBarrierSegmentRestrictionRequestBody(restrictedSegment: new CreateShieldInformationBarrierSegmentRestrictionRequestBodyRestrictedSegmentField() { Id = segmentToRestrictId, Type = CreateShieldInformationBarrierSegmentRestrictionRequestBodyRestrictedSegmentTypeField.ShieldInformationBarrierSegment }, shieldInformationBarrierSegment: new CreateShieldInformationBarrierSegmentRestrictionRequestBodyShieldInformationBarrierSegmentField() { Id = segmentId, Type = CreateShieldInformationBarrierSegmentRestrictionRequestBodyShieldInformationBarrierSegmentTypeField.ShieldInformationBarrierSegment }, type: CreateShieldInformationBarrierSegmentRestrictionRequestBodyTypeField.ShieldInformationBarrierSegmentRestriction));
            string segmentRestrictionId = NullableUtils.Unwrap(segmentRestriction.Id);
            Assert.IsTrue(segmentRestriction.ShieldInformationBarrierSegment.Id == segmentId);
            ShieldInformationBarrierSegmentRestrictions segmentRestrictions = await client.ShieldInformationBarrierSegmentRestrictions.GetShieldInformationBarrierSegmentRestrictionsAsync(queryParams: new GetShieldInformationBarrierSegmentRestrictionsQueryParams(shieldInformationBarrierSegmentId: segmentId));
            Assert.IsTrue(NullableUtils.Unwrap(segmentRestrictions.Entries).Count > 0);
            ShieldInformationBarrierSegmentRestriction segmentRestrictionFromApi = await client.ShieldInformationBarrierSegmentRestrictions.GetShieldInformationBarrierSegmentRestrictionByIdAsync(shieldInformationBarrierSegmentRestrictionId: segmentRestrictionId);
            Assert.IsTrue(NullableUtils.Unwrap(segmentRestrictionFromApi.Id) == segmentRestrictionId);
            Assert.IsTrue(segmentRestrictionFromApi.ShieldInformationBarrierSegment.Id == segmentId);
            Assert.IsTrue(segmentRestrictionFromApi.RestrictedSegment.Id == segmentToRestrictId);
            Assert.IsTrue(NullableUtils.Unwrap(segmentRestrictionFromApi.ShieldInformationBarrier).Id == barrierId);
            await client.ShieldInformationBarrierSegmentRestrictions.DeleteShieldInformationBarrierSegmentRestrictionByIdAsync(shieldInformationBarrierSegmentRestrictionId: segmentRestrictionId);
            await Assert.That.IsExceptionAsync(async() => await client.ShieldInformationBarrierSegmentRestrictions.GetShieldInformationBarrierSegmentRestrictionByIdAsync(shieldInformationBarrierSegmentRestrictionId: segmentRestrictionId));
            await client.ShieldInformationBarrierSegments.DeleteShieldInformationBarrierSegmentByIdAsync(shieldInformationBarrierSegmentId: segmentId);
            await client.ShieldInformationBarrierSegments.DeleteShieldInformationBarrierSegmentByIdAsync(shieldInformationBarrierSegmentId: segmentToRestrictId);
        }

    }
}