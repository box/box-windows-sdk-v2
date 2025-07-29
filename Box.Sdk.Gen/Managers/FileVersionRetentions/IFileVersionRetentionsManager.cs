using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IFileVersionRetentionsManager {
        /// <summary>
    /// Retrieves all file version retentions for the given enterprise.
    /// 
    /// **Note**:
    /// File retention API is now **deprecated**. 
    /// To get information about files and file versions under retention,
    /// see [files under retention](e://get-retention-policy-assignments-id-files-under-retention) or [file versions under retention](e://get-retention-policy-assignments-id-file-versions-under-retention) endpoints.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getFileVersionRetentions method
    /// </param>
    /// <param name="headers">
    /// Headers of getFileVersionRetentions method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileVersionRetentions> GetFileVersionRetentionsAsync(GetFileVersionRetentionsQueryParams? queryParams = default, GetFileVersionRetentionsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns information about a file version retention.
    /// 
    /// **Note**:
    /// File retention API is now **deprecated**. 
    /// To get information about files and file versions under retention,
    /// see [files under retention](e://get-retention-policy-assignments-id-files-under-retention) or [file versions under retention](e://get-retention-policy-assignments-id-file-versions-under-retention) endpoints.
    /// </summary>
    /// <param name="fileVersionRetentionId">
    /// The ID of the file version retention.
    /// Example: "3424234"
    /// </param>
    /// <param name="headers">
    /// Headers of getFileVersionRetentionById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileVersionRetention> GetFileVersionRetentionByIdAsync(string fileVersionRetentionId, GetFileVersionRetentionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}