using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IShuttleManager {
        /// <summary>
    /// Returns a paginated list of shuttle systems accessible to the
    /// authenticated user.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShuttleSystemsV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleSystemsV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleSystemsV2026R0> GetShuttleSystemsV2026R0Async(GetShuttleSystemsV2026R0QueryParams? queryParams = default, GetShuttleSystemsV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a new shuttle system. Only Windows agent systems can be created
    /// through the public API; other system types must be configured through the
    /// Shuttle application.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShuttleSystemV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttleSystemV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleSystemV2026R0> CreateShuttleSystemV2026R0Async(ShuttleSystemCreateRequestV2026R0 requestBody, CreateShuttleSystemV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns details for a single shuttle system identified by its ID,
    /// including its type and connection state.
    /// </summary>
    /// <param name="shuttleSystemId">
    /// The unique identifier for the shuttle system.
    /// Example: "6a315c0754833bfc5df067bf"
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleSystemByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleSystemV2026R0> GetShuttleSystemByIdV2026R0Async(string shuttleSystemId, GetShuttleSystemByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates the mutable properties of an existing shuttle system identified by
    /// its ID. Only the fields supplied in the request body are changed.
    /// </summary>
    /// <param name="shuttleSystemId">
    /// The unique identifier for the shuttle system.
    /// Example: "6a315c0754833bfc5df067bf"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateShuttleSystemByIdV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of updateShuttleSystemByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleSystemV2026R0> UpdateShuttleSystemByIdV2026R0Async(string shuttleSystemId, ShuttleSystemUpdateRequestV2026R0 requestBody, UpdateShuttleSystemByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Permanently deletes the shuttle system identified by its ID. This does not
    /// affect content that has already been migrated.
    /// </summary>
    /// <param name="shuttleSystemId">
    /// The unique identifier for the shuttle system.
    /// Example: "6a315c0754833bfc5df067bf"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteShuttleSystemByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteShuttleSystemByIdV2026R0Async(string shuttleSystemId, DeleteShuttleSystemByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Issues a new installation token for the Windows shuttle system identified
    /// by its ID. Paste the returned token into the Windows agent installer to
    /// register the agent with this system.
    /// </summary>
    /// <param name="shuttleSystemId">
    /// The unique identifier for the shuttle system.
    /// Example: "6a315c0754833bfc5df067bf"
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttleSystemIssueRegistrationTokenV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleAgentCredentialsV2026R0> CreateShuttleSystemIssueRegistrationTokenV2026R0Async(string shuttleSystemId, CreateShuttleSystemIssueRegistrationTokenV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a paginated list of shuttle jobs accessible to the authenticated
    /// user.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShuttleJobsV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleJobsV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleJobsV2026R0> GetShuttleJobsV2026R0Async(GetShuttleJobsV2026R0QueryParams? queryParams = default, GetShuttleJobsV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a new shuttle job that describes a migration from the given source
    /// system into Box.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShuttleJobV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttleJobV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleJobV2026R0> CreateShuttleJobV2026R0Async(ShuttleJobCreateRequestV2026R0 requestBody, CreateShuttleJobV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns details for a single shuttle job identified by its ID, including
    /// its current state, mapping-preparation state, and configured source system.
    /// </summary>
    /// <param name="shuttleJobId">
    /// The unique identifier for the shuttle job.
    /// Example: "6a3ba68e810d9156194530a1"
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleJobByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleJobV2026R0> GetShuttleJobByIdV2026R0Async(string shuttleJobId, GetShuttleJobByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates the mutable properties of an existing shuttle job identified by its
    /// ID. Only the fields supplied in the request body are changed.
    /// </summary>
    /// <param name="shuttleJobId">
    /// The unique identifier for the shuttle job.
    /// Example: "6a3ba68e810d9156194530a1"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateShuttleJobByIdV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of updateShuttleJobByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleJobV2026R0> UpdateShuttleJobByIdV2026R0Async(string shuttleJobId, ShuttleJobUpdateRequestV2026R0 requestBody, UpdateShuttleJobByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Permanently deletes the shuttle job identified by its ID. This does not
    /// affect content that has already been migrated.
    /// </summary>
    /// <param name="shuttleJobId">
    /// The unique identifier for the shuttle job.
    /// Example: "6a3ba68e810d9156194530a1"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteShuttleJobByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteShuttleJobByIdV2026R0Async(string shuttleJobId, DeleteShuttleJobByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Starts the job in the given mode. When no body is provided the job runs in
    /// simulation mode.
    /// </summary>
    /// <param name="shuttleJobId">
    /// The unique identifier for the shuttle job.
    /// Example: "6a3ba68e810d9156194530a1"
    /// </param>
    /// <param name="requestBody">
    /// Request body of createShuttleJobStartV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttleJobStartV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task CreateShuttleJobStartV2026R0Async(string shuttleJobId, ShuttleJobStartRequestV2026R0? requestBody = null, CreateShuttleJobStartV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Stops a running shuttle job identified by its ID. Any work already in
    /// progress is allowed to finish before the job transitions to a stopped
    /// state.
    /// </summary>
    /// <param name="shuttleJobId">
    /// The unique identifier for the shuttle job.
    /// Example: "6a3ba68e810d9156194530a1"
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttleJobStopV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task CreateShuttleJobStopV2026R0Async(string shuttleJobId, CreateShuttleJobStopV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Validates the configured account and path mappings before a transfer. Only
    /// relevant when account mapping is enabled.
    /// </summary>
    /// <param name="shuttleJobId">
    /// The unique identifier for the shuttle job.
    /// Example: "6a3ba68e810d9156194530a1"
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttleJobPrepareMigrationV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task CreateShuttleJobPrepareMigrationV2026R0Async(string shuttleJobId, CreateShuttleJobPrepareMigrationV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a paginated list of runs for the shuttle job identified by
    /// `job_id`.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShuttleRunsV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleRunsV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleRunsV2026R0> GetShuttleRunsV2026R0Async(GetShuttleRunsV2026R0QueryParams queryParams, GetShuttleRunsV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns details for a single shuttle run identified by its ID, including
    /// its mode, state, timing, and transfer statistics.
    /// </summary>
    /// <param name="shuttleRunId">
    /// The unique identifier for the shuttle run, in the form
    /// `{job_id}:{run_number}`.
    /// Example: "6a3ba68e810d9156194530a1:42"
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleRunByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleRunV2026R0> GetShuttleRunByIdV2026R0Async(string shuttleRunId, GetShuttleRunByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a paginated list of schedules for the shuttle job identified by
    /// `job_id`.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShuttleJobSchedulesV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleJobSchedulesV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleJobSchedulesV2026R0> GetShuttleJobSchedulesV2026R0Async(GetShuttleJobSchedulesV2026R0QueryParams queryParams, GetShuttleJobSchedulesV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a schedule that starts runs of the shuttle job identified by
    /// `job_id`. The schedule starts a run either at a wall-clock time or after
    /// another job's run meets a condition. Schedules cannot start a run in
    /// `scan` mode.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShuttleJobScheduleV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttleJobScheduleV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleJobScheduleV2026R0> CreateShuttleJobScheduleV2026R0Async(ShuttleJobScheduleCreateRequestV2026R0 requestBody, CreateShuttleJobScheduleV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves the shuttle job schedule identified by its ID.
    /// </summary>
    /// <param name="shuttleJobScheduleId">
    /// The unique identifier for the shuttle job schedule.
    /// Example: "7c1ba68e810d9156194530b2"
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleJobScheduleByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleJobScheduleV2026R0> GetShuttleJobScheduleByIdV2026R0Async(string shuttleJobScheduleId, GetShuttleJobScheduleByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates the mutable properties of an existing shuttle job schedule
    /// identified by its ID. Only the fields supplied in the request body are
    /// changed. The owning job cannot be changed.
    /// </summary>
    /// <param name="shuttleJobScheduleId">
    /// The unique identifier for the shuttle job schedule.
    /// Example: "7c1ba68e810d9156194530b2"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateShuttleJobScheduleByIdV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of updateShuttleJobScheduleByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleJobScheduleV2026R0> UpdateShuttleJobScheduleByIdV2026R0Async(string shuttleJobScheduleId, ShuttleJobScheduleUpdateRequestV2026R0 requestBody, UpdateShuttleJobScheduleByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Permanently deletes the shuttle job schedule identified by its ID. This
    /// does not affect runs that have already started.
    /// </summary>
    /// <param name="shuttleJobScheduleId">
    /// The unique identifier for the shuttle job schedule.
    /// Example: "7c1ba68e810d9156194530b2"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteShuttleJobScheduleByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteShuttleJobScheduleByIdV2026R0Async(string shuttleJobScheduleId, DeleteShuttleJobScheduleByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a paginated list of path mappings for the shuttle job identified
    /// by `job_id`.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShuttlePathMappingsV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttlePathMappingsV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttlePathMappingsV2026R0> GetShuttlePathMappingsV2026R0Async(GetShuttlePathMappingsV2026R0QueryParams queryParams, GetShuttlePathMappingsV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a path mapping that connects a source location on the migrated
    /// system to a destination in Box for the given shuttle job.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShuttlePathMappingV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttlePathMappingV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttlePathMappingV2026R0> CreateShuttlePathMappingV2026R0Async(ShuttlePathMappingCreateRequestV2026R0 requestBody, CreateShuttlePathMappingV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns details for a single shuttle path mapping identified by its ID,
    /// including the source location and destination, if any.
    /// </summary>
    /// <param name="shuttlePathMappingId">
    /// The unique identifier for the shuttle path mapping.
    /// Example: "6a3ba7a24ea17bc9461eeacd"
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttlePathMappingByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttlePathMappingV2026R0> GetShuttlePathMappingByIdV2026R0Async(string shuttlePathMappingId, GetShuttlePathMappingByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates the mutable properties of an existing shuttle path mapping
    /// identified by its ID. Only the fields supplied in the request body are
    /// changed.
    /// </summary>
    /// <param name="shuttlePathMappingId">
    /// The unique identifier for the shuttle path mapping.
    /// Example: "6a3ba7a24ea17bc9461eeacd"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateShuttlePathMappingByIdV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of updateShuttlePathMappingByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttlePathMappingV2026R0> UpdateShuttlePathMappingByIdV2026R0Async(string shuttlePathMappingId, ShuttlePathMappingUpdateRequestV2026R0 requestBody, UpdateShuttlePathMappingByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Permanently deletes the shuttle path mapping identified by its ID. This
    /// does not affect content that has already been migrated.
    /// </summary>
    /// <param name="shuttlePathMappingId">
    /// The unique identifier for the shuttle path mapping.
    /// Example: "6a3ba7a24ea17bc9461eeacd"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteShuttlePathMappingByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteShuttlePathMappingByIdV2026R0Async(string shuttlePathMappingId, DeleteShuttlePathMappingByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Accepts a spreadsheet of path mappings, already stored in Box, and replaces
    /// the job's path mappings.
    /// A file that is not a mapping workbook, or that exceeds the size or row
    /// limits (10 MiB Box download limit; 100,000 mapping rows), returns `400`
    /// synchronously and leaves the job unchanged.
    /// An accepted import always replaces every path mapping on the job. When all
    /// rows are valid the workbook is deployed in full. When any row is invalid
    /// the job ends with no path mappings and `mapping_state` becomes
    /// `paths_import_rejected` until the next import or the next manual
    /// path-mapping change. Row problems are listed by
    /// `GET /shuttle_mapping_errors?job_id=…` with category `path_import`, messages
    /// of the form `Row N: …`, at most 500 entries plus one overflow entry.
    /// Validation and deployment are asynchronous after acceptance. Poll
    /// `GET /shuttle_jobs/{shuttle_job_id}` while `mapping_state` is
    /// `importing_paths`. Only one import may be in flight per job.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShuttlePathMappingImportV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttlePathMappingImportV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task CreateShuttlePathMappingImportV2026R0Async(ShuttlePathMappingImportCreateRequestV2026R0 requestBody, CreateShuttlePathMappingImportV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Accepts a spreadsheet of subject (user and group) mappings, already stored
    /// in Box, and replaces the job's subject mappings.
    /// A file that is not a subject-mapping workbook, or that exceeds the size or
    /// row limits (10 MiB Box download limit; 50,000 mapping rows), returns `400`
    /// synchronously and leaves the job unchanged.
    /// An accepted import always replaces every subject mapping on the job. When
    /// all rows are valid the workbook is deployed in full. When any row is
    /// invalid the job ends with no subject mappings and `mapping_state` becomes
    /// `subjects_import_rejected` until the next import or the next manual
    /// subject-mapping change. Row problems are listed by
    /// `GET /shuttle_mapping_errors?job_id=…` with category `subject_import`,
    /// messages of the form `Row N: …`, at most 500 entries plus one overflow
    /// entry.
    /// Validation and deployment are asynchronous after acceptance. Poll
    /// `GET /shuttle_jobs/{shuttle_job_id}` while `mapping_state` is
    /// `importing_subjects`. Only one import may be in flight per job. The job
    /// must map subjects (`account_mapping` enabled) and its source scan must be
    /// finished; otherwise the request returns `409`.
    /// 
    /// Expected workbook format (API clients cannot download the UI template):
    /// sheets `Users` and/or `Groups` (a sheet named `Subjects` is also accepted).
    /// Columns are `Source User <Key>`, `Target User <Key>`, `Skip` on `Users`,
    /// and `Source Group <Key>`, `Target Group <Key>`, `Skip` on `Groups`, where
    /// `<Key>` is the primary identifier for that role and system:
    /// - Users: `Email` for most systems; `ID` for `SharePoint` (2007/2010/2013);
    ///   `Name` for Windows (`windows_union`, stored as the user id).
    /// - Groups: `Name` for most systems; `Email` for Google Drive admin groups.
    /// Source rows for a template can be taken from
    /// `GET /shuttle_discovered_subjects`.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShuttleSubjectMappingImportV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttleSubjectMappingImportV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task CreateShuttleSubjectMappingImportV2026R0Async(ShuttleSubjectMappingImportCreateRequestV2026R0 requestBody, CreateShuttleSubjectMappingImportV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a paginated list of subject mappings for the shuttle job identified
    /// by `job_id`.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShuttleSubjectMappingsV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleSubjectMappingsV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleSubjectMappingsV2026R0> GetShuttleSubjectMappingsV2026R0Async(GetShuttleSubjectMappingsV2026R0QueryParams queryParams, GetShuttleSubjectMappingsV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a subject mapping that connects a discovered source subject to a
    /// Box user or group for the given shuttle job.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShuttleSubjectMappingV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttleSubjectMappingV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleSubjectMappingV2026R0> CreateShuttleSubjectMappingV2026R0Async(ShuttleSubjectMappingCreateRequestV2026R0 requestBody, CreateShuttleSubjectMappingV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns details for a single shuttle subject mapping identified by its ID,
    /// including the source subject and Box target, if any.
    /// </summary>
    /// <param name="shuttleSubjectMappingId">
    /// The unique identifier for the shuttle subject mapping.
    /// Example: "6a3ba7b12ea17bc9461eeace"
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleSubjectMappingByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleSubjectMappingV2026R0> GetShuttleSubjectMappingByIdV2026R0Async(string shuttleSubjectMappingId, GetShuttleSubjectMappingByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates the mutable properties of an existing shuttle subject mapping
    /// identified by its ID. Only the fields supplied in the request body are
    /// changed.
    /// </summary>
    /// <param name="shuttleSubjectMappingId">
    /// The unique identifier for the shuttle subject mapping.
    /// Example: "6a3ba7b12ea17bc9461eeace"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateShuttleSubjectMappingByIdV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of updateShuttleSubjectMappingByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleSubjectMappingV2026R0> UpdateShuttleSubjectMappingByIdV2026R0Async(string shuttleSubjectMappingId, ShuttleSubjectMappingUpdateRequestV2026R0 requestBody, UpdateShuttleSubjectMappingByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Permanently deletes the shuttle subject mapping identified by its ID.
    /// </summary>
    /// <param name="shuttleSubjectMappingId">
    /// The unique identifier for the shuttle subject mapping.
    /// Example: "6a3ba7b12ea17bc9461eeace"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteShuttleSubjectMappingByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteShuttleSubjectMappingByIdV2026R0Async(string shuttleSubjectMappingId, DeleteShuttleSubjectMappingByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a paginated list of subjects discovered on the source system for
    /// the shuttle job identified by `job_id`.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShuttleDiscoveredSubjectsV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleDiscoveredSubjectsV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleDiscoveredSubjectsV2026R0> GetShuttleDiscoveredSubjectsV2026R0Async(GetShuttleDiscoveredSubjectsV2026R0QueryParams queryParams, GetShuttleDiscoveredSubjectsV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a paginated list of source or target path entries for a shuttle
    /// job. Call with only `job_id` and `side` to list the roots, then browse one
    /// level at a time using `path`, `path_id`, or both. Each entry includes
    /// `path`, `path_id`, `is_directory`, and a `scope` object (`account` or
    /// `shared_space`) so a mapping can be created from that row without extra
    /// browse state. Valid values depend on the selected system.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShuttlePathEntriesV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttlePathEntriesV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttlePathEntriesV2026R0> GetShuttlePathEntriesV2026R0Async(GetShuttlePathEntriesV2026R0QueryParams queryParams, GetShuttlePathEntriesV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a paginated list of mapping errors for a shuttle job. Only the latest
    /// record for each error is returned. The revision field identifies the mapping
    /// revision in which that error was generated.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShuttleMappingErrorsV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleMappingErrorsV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleMappingErrorsV2026R0> GetShuttleMappingErrorsV2026R0Async(GetShuttleMappingErrorsV2026R0QueryParams queryParams, GetShuttleMappingErrorsV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a paginated list of reports generated for the shuttle run
    /// identified by `run_id`.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShuttleReportsV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleReportsV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleReportsV2026R0> GetShuttleReportsV2026R0Async(GetShuttleReportsV2026R0QueryParams queryParams, GetShuttleReportsV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Requests asynchronous generation of a report for a shuttle run. The
    /// report is returned immediately in `pending` or `processing` status.
    /// Poll `GET /shuttle_reports/{shuttle_report_id}` while `status` is
    /// `pending` or `processing`. Stop when `status` is `complete`, `failed`,
    /// `canceled`, or `unknown`. Repeating this request with the same
    /// parameters reuses the existing report instead of creating a duplicate.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShuttleReportV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createShuttleReportV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleReportV2026R0> CreateShuttleReportV2026R0Async(ShuttleReportCreateRequestV2026R0 requestBody, CreateShuttleReportV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns details for a single shuttle report identified by its ID,
    /// including generation status and, once generation finishes, the Box
    /// output reference.
    /// </summary>
    /// <param name="shuttleReportId">
    /// The unique identifier for the shuttle report.
    /// Example: "6a3ba8d14f2c91e8370b4d12"
    /// </param>
    /// <param name="headers">
    /// Headers of getShuttleReportByIdV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShuttleReportV2026R0> GetShuttleReportByIdV2026R0Async(string shuttleReportId, GetShuttleReportByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}