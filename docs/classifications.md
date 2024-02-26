Classifications
===============

Classifications are a type of metadata that allows users and applications 
to define and assign a content classification to files and folders.

Classifications use the metadata APIs to add and remove classifications, and
assign them to files. For more details on metadata templates please see the
[metadata documentation](./metadata.md).

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Classifications](#classifications)
  - [Add initial classifications](#add-initial-classifications)
  - [List all classifications](#list-all-classifications)
  - [Add another classification](#add-another-classification)
  - [Update a classification](#update-a-classification)
  - [Delete a classification](#delete-a-classification)
  - [Delete all classifications](#delete-all-classifications)
  - [Add classification to file](#add-classification-to-file)
  - [Update classification on file](#update-classification-on-file)
  - [Get classification on file](#get-classification-on-file)
  - [Remove classification from file](#remove-classification-from-file)
  - [Add classification to folder](#add-classification-to-folder)
  - [Update classification on folder](#update-classification-on-folder)
  - [Get classification on folder](#get-classification-on-folder)
  - [Remove classification from folder](#remove-classification-from-folder)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Add initial classifications
---------------------------

If an enterprise does not already have a classification defined, the first classification(s)
can be added with the `MetadataManager.CreateMetadataTemplate(BoxMetadataTemplate metadataTemplate)` method.

<!-- sample post_metadata_templates_schema classifications -->
```c# 
var field = new BoxMetadataTemplateField
{
    DisplayName = "Classification",
    Type = "enum",
    Key = "Box_Security_Classification_Key",
    Options = new List<BoxMetadataTemplateFieldOption>() 
    {
        new BoxMetadataTemplateFieldOption
        {
            Key = "Classified"
        }
    }
};

var metadataTemplate = new BoxMetadataTemplate
{
    DisplayName = "Classification",
    TemplateKey = "securityClassification-6VMVochwUWo",
    Scope = "enterprise",
    Fields = new List<BoxMetadataTemplateField>() { field },
};

var template = await client.MetadataManager.CreateMetadataTemplate(metadataTemplate);
```

List all classifications
------------------------

To retrieve a list of all the classifications in an enterprise call the
`MetadataManager.GetMetadataTemplate(string scope, string template)`
method to get the classifications template, which will contain a list of all the 
classifications.

<!-- sample get_metadata_templates_enterprise_securityClassification-6VMVochwUWo_schema -->
```c#
var template = await client.MetadataManager.GetMetadataTemplate("enterprise", "securityClassification-6VMVochwUWo");
```

Add another classification
--------------------------

To add another classification, call the 
`MetadataManager.UpdateMetadataTemplate(IEnumerable<BoxMetadataTemplateUpdate> metadataTemplateUpdate, string scope, string template)` method
with proper parameters.

<!-- sample put_metadata_templates_enterprise_securityClassification-6VMVochwUWo_schema add -->
```c#
var update = new BoxMetadataTemplateUpdate
{
    Op = MetadataTemplateUpdateOp.addEnumOption,
    FieldKey = "Box_Security_Classification_Key",
    Data = new
    {
        key = "Sensitive"
    }
};

var template = await client.MetadataManager
    .UpdateMetadataTemplate(new List<BoxMetadataTemplateUpdate>() { update }, "enterprise", "securityClassification-6VMVochwUWo");
```

Update a classification
-----------------------

To update a classification, call the
`MetadataManager.UpdateMetadataTemplate(IEnumerable<BoxMetadataTemplateUpdate> metadataTemplateUpdate, string scope, string template)` method
with proper parameters.

<!-- sample put_metadata_templates_enterprise_securityClassification-6VMVochwUWo_schema update -->
```c#
var update = new BoxMetadataTemplateUpdate
{
    Op = MetadataTemplateUpdateOp.editEnumOption,
    FieldKey = "Box_Security_Classification_Key",
    EnumOptionKey = "Sensitive",
    Data = new
    {
        key = "Very Sensitive"
    }
};

var template = await client.MetadataManager
    .UpdateMetadataTemplate(new List<BoxMetadataTemplateUpdate>() { update }, "enterprise", "securityClassification-6VMVochwUWo");
```

Add classification to file
--------------------------

To add a classification to a file, call 
`MetadataManager.SetFileMetadataAsync(string fileId, Dictionary<string, object> metadata, string scope, string template)`
with the name of the classification template, as well as the details of the classification
to add to the file.

<!-- sample post_files_id_metadata_enterprise_securityClassification-6VMVochwUWo -->
```c#
var fileId = "0";
var classification = new Dictionary<string, object>
{
    { "Box_Security_Classification_Key", "Sensitive" }
};

var classificationData = await client.MetadataManager.SetFileMetadataAsync(fileId, classification, "enterprise", "securityClassification-6VMVochwUWo");
```

Update classification on file
-----------------------------

To update a classification on a file, call 
`MetadataManager.UpdateFileMetadataAsync(string fileId, List<BoxMetadataUpdate> updates, string scope, string template)`
with the name of the classification template, as well as the details of the classification
to add to the file.

<!-- sample put_files_id_metadata_enterprise_securityClassification-6VMVochwUWo -->
```c#
var fileId = "0";
var update = new BoxMetadataUpdate
{
    Op = MetadataUpdateOp.replace,
    Path = "/Box_Security_Classification_Key",
    Value = "Very Sensitive"
};

var classificationData = await client.MetadataManager
    .UpdateFileMetadataAsync(fileId, new List<BoxMetadataUpdate>() { update }, "enterprise", "securityClassification-6VMVochwUWo");
```


Get classification on file
--------------------------

Retrieve the classification on a file by calling
`MetadataManager.GetFileMetadataAsync(string fileId, string scope, string template)`
on a file.

<!-- sample get_files_id_metadata_enterprise_securityClassification-6VMVochwUWo -->
```c#
var fileId = "0";

var fileMetadata = await client.MetadataManager.GetFileMetadataAsync(fileId, "enterprise", "securityClassification-6VMVochwUWo");
```

Remove classification from file
-------------------------------

A classification can be removed from a file by calling
`MetadataManager.DeleteFileMetadataAsync(string fileId, string scope, string template)`.

<!-- sample delete_files_id_metadata_enterprise_securityClassification-6VMVochwUWo -->
```c#
var fileId = "0";

var fileMetadata = await client.MetadataManager.DeleteFileMetadataAsync(fileId, "enterprise", "securityClassification-6VMVochwUWo");
```


Add classification to folder
--------------------------

To add a classification to a folder, call 
`MetadataManager.SetFolderMetadataAsync(string folderId, Dictionary<string, object> metadata, string scope, string template)`
with the name of the classification template, as well as the details of the classification
to add to the folder.

<!-- sample post_folders_id_metadata_enterprise_securityClassification-6VMVochwUWo -->
```c#
var folderId = "0";
var classification = new Dictionary<string, object>
{
    { "Box_Security_Classification_Key", "Sensitive" }
};

var classificationData = await client.MetadataManager.SetFolderMetadataAsync(folderId, classification, "enterprise", "securityClassification-6VMVochwUWo");
```

Update classification on folder
-----------------------------

To update a classification on a folder, call 
`MetadataManager.UpdateFolderMetadataAsync(string folderId, List<BoxMetadataUpdate> updates, string scope, string template)`.
with the name of the classification template, as well as the details of the classification
to add to the folder.

<!-- sample put_folders_id_metadata_enterprise_securityClassification-6VMVochwUWo -->
```c#
var folderId = "0";
var update = new BoxMetadataUpdate
{
    Op = MetadataUpdateOp.replace,
    Path = "/Box_Security_Classification_Key",
    Value = "Very Sensitive"
};

var classificationData = await client.MetadataManager
    .UpdateFolderMetadataAsync(folderId, new List<BoxMetadataUpdate>(){ update }, "enterprise", "securityClassification-6VMVochwUWo");
```

Get classification on folder
--------------------------

Retrieve the classification on a folder by calling
`MetadataManager.GetFolderMetadataAsync(string folderId, string scope, string template)`
on a folder.

<!-- sample get_folders_id_metadata_enterprise_securityClassification-6VMVochwUWo -->
```c#
var folderId = "0";

var folderMetadata = await client.MetadataManager.GetFolderMetadataAsync(folderId, "enterprise", "securityClassification-6VMVochwUWo");
```

Remove classification from folder
-------------------------------

A classification can be removed from a folder by calling
`MetadataManager.DeleteFolderMetadataAsync(string folderId, string scope, string template)`.

<!-- sample delete_folders_id_metadata_enterprise_securityClassification-6VMVochwUWo -->
```c#
var folderId = "0";

var folderMetadata = await client.MetadataManager.DeleteFolderMetadataAsync(folderId, "enterprise", "securityClassification-6VMVochwUWo");
```