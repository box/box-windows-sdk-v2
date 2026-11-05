using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class ShuttleManagerTests {
        public BoxClient client { get; }

        public ShuttleManagerTests() {
            client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateGetUpdateListDeleteShuttleSystemV2026R0() {
            string systemName = Utils.GetUUID();
            ShuttleSystemV2026R0 createdSystem = await client.Shuttle.CreateShuttleSystemV2026R0Async(requestBody: new ShuttleSystemCreateRequestV2026R0(name: systemName, systemType: ShuttleSystemCreateRequestV2026R0SystemTypeField.Windows));
            Assert.IsTrue(createdSystem.Name == systemName);
            Assert.IsTrue(createdSystem.SystemType == "windows");
            Assert.IsTrue(StringUtils.ToStringRepresentation(createdSystem.Type?.Value) == "shuttle_system");
            ShuttleSystemV2026R0 systemFromApi = await client.Shuttle.GetShuttleSystemByIdV2026R0Async(shuttleSystemId: createdSystem.Id);
            Assert.IsTrue(systemFromApi.Id == createdSystem.Id);
            Assert.IsTrue(systemFromApi.Name == systemName);
            string updatedSystemName = Utils.GetUUID();
            ShuttleSystemV2026R0 updatedSystem = await client.Shuttle.UpdateShuttleSystemByIdV2026R0Async(shuttleSystemId: createdSystem.Id, requestBody: new ShuttleSystemUpdateRequestV2026R0() { Name = updatedSystemName });
            Assert.IsTrue(updatedSystem.Name == updatedSystemName);
            ShuttleSystemsV2026R0 systems = await client.Shuttle.GetShuttleSystemsV2026R0Async();
            Assert.IsTrue(systems.Entries.Count > 0);
            ShuttleAgentCredentialsV2026R0 credentials = await client.Shuttle.CreateShuttleSystemIssueRegistrationTokenV2026R0Async(shuttleSystemId: createdSystem.Id);
            Assert.IsTrue(credentials.RegistrationToken != "");
            await client.Shuttle.DeleteShuttleSystemByIdV2026R0Async(shuttleSystemId: createdSystem.Id);
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttleSystemByIdV2026R0Async(shuttleSystemId: createdSystem.Id));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestShuttleJobsWithNonExistentIdV2026R0() {
            const string resourceId = "123456";
            ShuttleJobsV2026R0 jobs = await client.Shuttle.GetShuttleJobsV2026R0Async();
            Assert.IsTrue(jobs.Entries.Count >= 0);
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.CreateShuttleJobV2026R0Async(requestBody: new ShuttleJobCreateRequestV2026R0(name: Utils.GetUUID(), jobType: ShuttleJobCreateRequestV2026R0JobTypeField.Transfer, sourceSystemId: resourceId)));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttleJobByIdV2026R0Async(shuttleJobId: resourceId));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.UpdateShuttleJobByIdV2026R0Async(shuttleJobId: resourceId, requestBody: new ShuttleJobUpdateRequestV2026R0() { Name = Utils.GetUUID() }));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.DeleteShuttleJobByIdV2026R0Async(shuttleJobId: resourceId));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.CreateShuttleJobStartV2026R0Async(shuttleJobId: resourceId));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.CreateShuttleJobStopV2026R0Async(shuttleJobId: resourceId));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.CreateShuttleJobPrepareMigrationV2026R0Async(shuttleJobId: resourceId));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestShuttleRunsWithNonExistentIdV2026R0() {
            const string resourceId = "123456";
            ShuttleRunsV2026R0 runs = await client.Shuttle.GetShuttleRunsV2026R0Async(queryParams: new GetShuttleRunsV2026R0QueryParams(jobId: resourceId));
            Assert.IsTrue(runs.Entries.Count >= 0);
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttleRunByIdV2026R0Async(shuttleRunId: resourceId));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestShuttleMappingsWithNonExistentIdV2026R0() {
            const string resourceId = "123456";
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttlePathMappingsV2026R0Async(queryParams: new GetShuttlePathMappingsV2026R0QueryParams(jobId: resourceId)));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttlePathMappingByIdV2026R0Async(shuttlePathMappingId: resourceId));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.UpdateShuttlePathMappingByIdV2026R0Async(shuttlePathMappingId: resourceId, requestBody: new ShuttlePathMappingUpdateRequestV2026R0() { IsPathSkipped = true }));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.DeleteShuttlePathMappingByIdV2026R0Async(shuttlePathMappingId: resourceId));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.CreateShuttlePathMappingImportV2026R0Async(requestBody: new ShuttlePathMappingImportCreateRequestV2026R0(jobId: resourceId, fileId: resourceId)));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttleSubjectMappingsV2026R0Async(queryParams: new GetShuttleSubjectMappingsV2026R0QueryParams(jobId: resourceId)));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.CreateShuttleSubjectMappingV2026R0Async(requestBody: new ShuttleSubjectMappingCreateRequestV2026R0(jobId: resourceId, discoveredSubjectId: resourceId)));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttleSubjectMappingByIdV2026R0Async(shuttleSubjectMappingId: resourceId));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.UpdateShuttleSubjectMappingByIdV2026R0Async(shuttleSubjectMappingId: resourceId, requestBody: new ShuttleSubjectMappingUpdateRequestV2026R0() { IsSubjectSkipped = true }));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.DeleteShuttleSubjectMappingByIdV2026R0Async(shuttleSubjectMappingId: resourceId));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.CreateShuttleSubjectMappingImportV2026R0Async(requestBody: new ShuttleSubjectMappingImportCreateRequestV2026R0(jobId: resourceId, fileId: resourceId)));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttleDiscoveredSubjectsV2026R0Async(queryParams: new GetShuttleDiscoveredSubjectsV2026R0QueryParams(jobId: resourceId)));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttlePathEntriesV2026R0Async(queryParams: new GetShuttlePathEntriesV2026R0QueryParams(jobId: resourceId, side: GetShuttlePathEntriesV2026R0QueryParamsSideField.Source)));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttleMappingErrorsV2026R0Async(queryParams: new GetShuttleMappingErrorsV2026R0QueryParams(jobId: resourceId)));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestShuttleReportsWithNonExistentIdV2026R0() {
            const string resourceId = "123456";
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttleReportsV2026R0Async(queryParams: new GetShuttleReportsV2026R0QueryParams(runId: resourceId)));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.CreateShuttleReportV2026R0Async(requestBody: new ShuttleReportCreateRequestV2026R0(runId: resourceId, format: ShuttleReportCreateRequestV2026R0FormatField.Csv, sections: Array.AsReadOnly(new [] {"summary"}))));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttleReportByIdV2026R0Async(shuttleReportId: resourceId));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestShuttleJobSchedulesWithNonExistentIdV2026R0() {
            const string resourceId = "123456";
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttleJobSchedulesV2026R0Async(queryParams: new GetShuttleJobSchedulesV2026R0QueryParams(jobId: resourceId)));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.GetShuttleJobScheduleByIdV2026R0Async(shuttleJobScheduleId: resourceId));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.UpdateShuttleJobScheduleByIdV2026R0Async(shuttleJobScheduleId: resourceId, requestBody: new ShuttleJobScheduleUpdateRequestV2026R0() { RunMode = ShuttleJobScheduleUpdateRequestV2026R0RunModeField.Simulation }));
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.DeleteShuttleJobScheduleByIdV2026R0Async(shuttleJobScheduleId: resourceId));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateShuttleJobScheduleWithNonExistentIdV2026R0() {
            const string resourceId = "123456";
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.CreateShuttleJobScheduleV2026R0Async(requestBody: new ShuttleJobScheduleCreateRequestV2026R0(jobId: resourceId, runMode: ShuttleJobScheduleCreateRequestV2026R0RunModeField.Transfer, trigger: new ShuttleJobScheduleTriggerByTimeV2026R0(type: ShuttleJobScheduleTriggerByTimeV2026R0TypeField.ByTime, startAt: "2030-01-01T09:00:00", timeZone: "America/Los_Angeles", repeat: ShuttleJobScheduleTriggerByTimeV2026R0RepeatField.Daily)) { State = ShuttleJobScheduleCreateRequestV2026R0StateField.Paused }));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateShuttlePathMappingWithNonExistentIdV2026R0() {
            const string resourceId = "123456";
            await Assert.That.IsExceptionAsync(async() => await client.Shuttle.CreateShuttlePathMappingV2026R0Async(requestBody: new ShuttlePathMappingCreateRequestV2026R0(jobId: resourceId, source: new ShuttlePathMappingAccountLocationInputV2026R0(type: ShuttlePathMappingAccountLocationInputV2026R0TypeField.Account, path: "/finance", account: new ShuttlePathMappingAccountInputV2026R0() { Email = "user@example.com" }))));
        }

    }
}