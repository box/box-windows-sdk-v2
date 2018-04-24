Watermarking
============

Watermarking allows you to place a semi-transparent overlay on an embedded file preview that displays a viewer's email
address or user ID and the time of access over a file's content. Once a watermark is applied to a file or folder via
the API, the watermark will be displayed on any file preview.

Get Watermark on a File
-----------------------

To get watermark information for a file call the `FilesManager.GetWatermarkAsync(string id)` method
with the ID of the file.

```c#
BoxWatermark watermark = await client.FilesManager.GetWatermarkAsync("11111");
```

Apply Watermark to a File
-------------------------

To apply or update the watermark to a file call the
`FilesManager.ApplyWatermarkAsync(string id, BoxApplyWatermarkRequest applyWatermarkRequest = null)`
method with the ID of the file.

```c#
BoxWatermark watermark = await client.FilesManager.ApplyWatermarkAsync("11111");
```

Remove Watermark from a File
----------------------------

A file's watermark can be removed by calling `FilesManager.RemoveWatermarkAsync(string id)`
with the ID of the watermarked file.

```c#
await client.FilesManager.RemoveWatermarkAsync("11111");
```

Get Watermark on a Folder
-------------------------

To get watermark information for a folder call the `FoldersManager.GetWatermarkAsync(string id)` method
with the ID of the folder.

```c#
BoxWatermark watermark = await client.FoldersManager.GetWatermarkAsync("22222");
```

Apply Watermark to a Folder
---------------------------

To apply or update the watermark for a folder call the
`FoldersManager.ApplyWatermarkAsync(string id, BoxApplyWatermarkRequest applyWatermarkRequest = null)`
method with the ID of the folder.

```c#
BoxWatermark watermark = await client.FoldersManager.ApplyWatermarkAsync("22222");
```

Remove Watermark from a Folder
------------------------------

A folder's watermark can be removed by calling `FoldersManager.RemoveWatermarkAsync(string id)`
with the ID of the watermarked folder.

```c#
await client.FoldersManager.RemoveWatermarkAsync("22222");
```