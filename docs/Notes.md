# INotesManager


- [Convert content to Box Note](#convert-content-to-box-note)

## Convert content to Box Note

Creates a Box Note (`.boxnote` file) from supported source content. See the `content_format` field for supported formats.

This operation is performed by calling function `CreateNoteConvertV2026R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2026.0/post-notes-convert/).

<!-- sample post_notes_convert_v2026.0 -->
```
await downscopedClient.Notes.CreateNoteConvertV2026R0Async(requestBody: new NotesConvertRequestBodyV2026R0(content: markdownContent, contentFormat: NotesConvertRequestBodyV2026R0ContentFormatField.Markdown, parent: new FolderReferenceV2026R0(id: "0"), name: noteName));
```

### Arguments

- requestBody `NotesConvertRequestBodyV2026R0`
  - Request body of createNoteConvertV2026R0 method
- headers `CreateNoteConvertV2026R0Headers`
  - Headers of createNoteConvertV2026R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `NotesConvertResponseV2026R0`.

The note was created successfully.


