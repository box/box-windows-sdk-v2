Watermarking
============

Watermarking allows you to place a semi-transparent overlay on an embedded file preview that displays a viewer's email
address or user ID and the time of access over a file's content. Once a watermark is applied to a file or folder via
the API, the watermark will be displayed on any file preview.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Get Watermark on a File](#get-watermark-on-a-file)
- [Apply Watermark to a File](#apply-watermark-to-a-file)
- [Remove Watermark from a File](#remove-watermark-from-a-file)
- [Get Watermark on a Folder](#get-watermark-on-a-folder)
- [Apply Watermark to a Folder](#apply-watermark-to-a-folder)
- [Remove Watermark from a Folder](#remove-watermark-from-a-folder)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Get Watermark on a File
-----------------------

To get watermark information for a file call the `FilesManager.GetWatermarkAsync(string id)` method
with the ID of the file.

<!-- sample get_files_id_watermark -->
```c#
BoxWatermark watermark = await client.FilesManager.GetWatermarkAsync("11111");
```

Apply Watermark to a File
-------------------------

To apply or update the watermark to a file call the
`FilesManager.ApplyWatermarkAsync(string id, BoxApplyWatermarkRequest applyWatermarkRequest = null)`
method with the ID of the file.

<!-- sample put_files_id_watermark -->
```c#
BoxWatermark watermark = await client.FilesManager.ApplyWatermarkAsync("11111");
```

Remove Watermark from a File
----------------------------

A file's watermark can be removed by calling `FilesManager.RemoveWatermarkAsync(string id)`
with the ID of the watermarked file.

<!-- sample delete_files_id_watermark -->
```c#
await client.FilesManager.RemoveWatermarkAsync("11111");
```

Get Watermark on a Folder
-------------------------

To get watermark information for a folder call the `FoldersManager.GetWatermarkAsync(string id)` method
with the ID of the folder.

<!-- sample get_folders_id_watermark -->
```c#
BoxWatermark watermark = await client.FoldersManager.GetWatermarkAsync("22222");
```

Apply Watermark to a Folder
---------------------------

To apply or update the watermark for a folder call the
`FoldersManager.ApplyWatermarkAsync(string id, BoxApplyWatermarkRequest applyWatermarkRequest = null)`
method with the ID of the folder.

<!-- sample put_folders_id_watermark -->
```c#
BoxWatermark watermark = await client.FoldersManager.ApplyWatermarkAsync("22222");
```

Remove Watermark from a Folder
------------------------------

A folder's watermark can be removed by calling `FoldersManager.RemoveWatermarkAsync(string id)`
with the ID of the watermarked folder.

<!-- sample delete_folders_id_watermark -->
```c#
await client.FoldersManager.RemoveWatermarkAsync("22222");
```