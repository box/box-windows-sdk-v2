using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IConvertMarkdownToBoxNoteManager {
        /// <summary>
    /// Creates a Box Note (`.boxnote` file) from supported source content. See the `content_format` field for supported formats.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createNoteConvertV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createNoteConvertV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<NotesConvertResponseV2026R0> CreateNoteConvertV2026R0Async(NotesConvertRequestBodyV2026R0 requestBody, CreateNoteConvertV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}