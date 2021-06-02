Metadata
========

Metadata allows users and applications to define and store custom data associated
with their files/folders. Metadata consists of key:value pairs that belong to
files/folders. For example, an important contract may have key:value pairs of
`"clientNumber":"820183"` and `"clientName":"bioMedicalCorp"`.

Metadata that belongs to a file/folder is grouped by templates. Templates allow
the metadata service to provide a multitude of services, such as pre-defining sets
of key:value pairs or schema enforcement on specific fields.

Each file/folder can have multiple distinct template instances associated with it,
and templates are also grouped by scopes. Currently, the only scopes support are
`enterprise` and `global`. Enterprise scopes are defined on a per enterprises basis,
whereas global scopes are Box application-wide.

In addition to `enterprise` scoped templates, every file on Box has access to the
`global` `properties` template. The Properties template is a bucket of free form
key:value string pairs, with no additional schema associated with it. Properties
are ideal for scenarios where applications want to write metadata to file objects
in a flexible way, without pre-defined template structure.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Create Metadata Template](#create-metadata-template)
- [Update Metadata Template](#update-metadata-template)
- [Get Metadata Template](#get-metadata-template)
  - [Get by template scope and key](#get-by-template-scope-and-key)
  - [Get by ID](#get-by-id)
- [Get Enterprise Metadata Templates](#get-enterprise-metadata-templates)
- [Set Metadata on a File](#set-metadata-on-a-file)
- [Get Metadata on a File](#get-metadata-on-a-file)
- [Remove Metadata from a File](#remove-metadata-from-a-file)
- [Set Metadata on a Folder](#set-metadata-on-a-folder)
- [Get Metadata on a Folder](#get-metadata-on-a-folder)
- [Remove Metadata from a Folder](#remove-metadata-from-a-folder)
- [Execute Metadata Query](#execute-metadata-query)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Create Metadata Template
------------------------

To create a new metadata template, call
`MetadataManager.CreateMetadataTemplate(BoxMetadataTemplate template)`.

<!-- sample post_metadata_templates_schema -->
```c#
var templateParams = new BoxMetadataTemplate()
{
    TemplateKey = "marketingCollateral",
    DisplayName = "Marketing Collateral",
    Scope = "enterprise",
    Fields = new List<BoxMetadataTemplateField>()
    {
        new BoxMetadataTemplateField()
        {
            Type = "enum",
            Key = "audience",
            DisplayName = "Audience",
            Options = new List<BoxMetadataTemplateFieldOption>()
            {
                new BoxMetadataTemplateFieldOption() { Key = "internal" },
                new BoxMetadataTemplateFieldOption() { Key = "external" }
            }
        },
        new BoxMetadataTemplateField()
        {
            Type = "string",
            Key = "author",
            DisplayName = "Author"
        }
    }
};
BoxMetadataTemplate template = await client.MetadataManager.CreateMetadataTemplate(templateParams);
```

Update Metadata Template
------------------------

To update a metadata template, call the
`MetadataManager.UpdateMetadataTemplate(IEnumerable<BoxMetadataTemplateUpdate> metadataTemplateUpdate, string scope, string template)`
method with the operations to perform on the template.  See the
[API Documentation](https://developer.box.com/en/reference/put-metadata-templates-id-id-schema/)
for more information on the operations available.

<!-- sample put_metadata_templates_id_id_schema -->
```c#
var updates = new List<BoxMetadataTemplateUpdate>()
{
    new BoxMetadataTemplateUpdate()
    {
        Op = MetadataTemplateUpdateOp.addEnumOption,
        FieldKey = "fy",
        Data = new {
            key = "FY20"
        }
    },
    new BoxMetadataTemplateUpdate()
    {
        Op = MetadataTemplateUpdateOp.editTemplate,
        Data = new {
            hidden = false
        }
    }
};
BoxMetadataTemplate updatedTemplate = await client.MetadataManager
    .UpdateMetadataTemplate(updates, "enterprise", "marketingCollateral");
```

Get Metadata Template
---------------------

### Get by template scope and key

To retrieve a specific metadata template by its scope and template key, call the
`MetadataManager.GetMetadataTemplate(string scope, string template)`
method with the scope and template key.

<!-- sample get_metadata_templates_id_id_schema -->
```c#
BoxMetadataTemplate template = await client.MetadataManager
    .GetMetadataTemplate("enterprise", "marketingCollateral");
```

### Get by ID

To get a specific metadata template by its ID, call the
`MetadataManager.GetMetadataTemplateById(string templateId)`
method with the ID of the template.

<!-- sample get_metadata_templates_id -->
```c#
BoxMetadataTemplate template = await client.MetadataManager
    .GetMetadataTemplateById("17f2d715-6acb-45f2-b96a-28b15efc9faa");
```

Get Enterprise Metadata Templates
---------------------------------

Get all metadata templates for the current enterprise and scope by calling
`MetadataManager.GetEnterpriseMetadataAsync(string scope = "enterprise")`.

<!-- sample get_metadata_templates_enterprise -->
```c#
BoxEnterpriseMetadataTemplateCollection<BoxMetadataTemplate> templates = await client.MetadataManager
    .GetEnterpriseMetadataAsync();
```

Get Global Metadata Templates
---------------------------------

Get all metadata templates available to all enterprises by calling
`MetadataManager.GetEnterpriseMetadataAsync(string scope = "global")`.

<!-- sample get_metadata_templates_global -->
```c#
BoxEnterpriseMetadataTemplateCollection<BoxMetadataTemplate> templates = await client.MetadataManager
    .GetEnterpriseMetadataAsync("global");
```

Set Metadata on a File
----------------------

To set metadata on a file, call
`MetadataManager.SetFileMetadataAsync(string fileId, Dictionary<string, object> metadata, string scope, string template)`
with the scope and template key of the metadata template, as well as a `Dictionary` containing the metadata keys
and values to set.

> __Note:__ This method will unconditionally apply the provided metadata, overwriting existing metadata
> for the keys provided.  To specifically create or update metadata, see the `CreateFileMetadataAsync()`
> and `UpdateFileMetadataAsync()` methods below.

```c#
var metadataValues = new Dictionary<string, object>()
{
    { "audience", "internal" },
    { "documentType", "Q1 plans" },
    { "competitiveDocument", "no" },
    { "status", "active" },
    { "author": "M. Jones" },
    { "currentState": "proposal" }
};
Dictionary<string, object> metadata = await client.MetadataManager
    .SetFileMetadataAsync(fileId: "11111", metadataValues, "enterprise", "marketingCollateral");
```

To add new metadata to a file, call 
`MetadataManager.CreateFileMetadataAsync(string fileId, Dictionary<string, object> metadata, string scope, string template)`
with a metadata template and a `Dictionary` of key/value pairs to add as metadata.

> __Note:__: This method will only succeed if the provided metadata template is not current applied to the file,
> otherwise it will fail with a Conflict error.

<!-- sample post_files_id_metadata_id_id -->
```c#
var metadataValues = new Dictionary<string, object>()
{
    { "audience", "internal" },
    { "documentType", "Q1 plans" },
    { "competitiveDocument", "no" },
    { "status", "active" },
    { "author": "M. Jones" },
    { "currentState": "proposal" }
};
Dictionary<string, object> metadata = await client.MetadataManager
    .CreateFileMetadataAsync(fileId: "11111", metadataValues, "enterprise", "marketingCollateral");
```

Update a file's existing metadata by calling
`MetadataManager.UpdateFileMetadataAsync(string fileId, List<BoxMetadataUpdate> updates, string scope, string template)`
with a list of update operations to apply.

> __Note:__ This method will only succeed if the provided metadata template has already been applied to
> the file; if the file does not have existing metadata, this method will fail with a Not Found error.
> This is useful in cases where you know the file will already have metadata applied, since it will
> save an API call compared to `SetFileMetadataAsync()`.

<!-- sample put_files_id_metadata_id_id -->
```c#
var updates = new List<BoxMetadataUpdate>()
{
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.test,
        Path = "/competitiveDocument",
        Value = "no"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.remove,
        Path = "/competitiveDocument"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.test,
        Path = "/status",
        Value = "active"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.replace,
        Path = "/competitiveDocument",
        Value = "inactive"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.test,
        Path = "/author",
        Value = "Jones"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.copy,
        From="/author",
        Path = "/editor"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.test,
        Path = "/currentState",
        Value = "proposal"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.move,
        From = "/currentState",
        Path = "/previousState"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.add,
        Path = "/currentState",
        Value = "reviewed"
    }
};
Dictionary<string, object> updatedMetadata = await client.MetadataManager
    .UpdateFileMetadataAsync("11111", updates, "enterprise", "marketingCollateral");
```

Get Metadata on a File
----------------------

Retrieve a specific metadata template on a file by calling
`MetadataManager.GetFileMetadataAsync(string fileId, string scope, string template)`
with the ID of the file and which template to fetch.

<!-- sample get_files_id_metadata_id_id -->
```c#
Dictionary<string, object> metadata = await client.MetadataManager.
    .GetFileMetadataAsync(fileId: "11111", "enterprise", "marketingCollateral");
```

You can also get all metadata on a file by calling `MetadataManager.GetAllFileMetadataTemplatesAsync(string fileId)`.

<!-- sample get_files_id_metadata -->
```c#
BoxMetadataTemplateCollection<Dictionary<string, object>> metadataInstances = await client.MetadataManager
    .GetAllFileMetadataTemplatesAsync(fileId: "11111");
```

Remove Metadata from a File
---------------------------

A metadata template can be removed from a file by calling
`MetadataManager.DeleteFileMetadataAsync(string fileId, string scope, string template)`
with the ID of the file and the metadata template to remove.

<!-- sample delete_files_id_metadata_id_id -->
```c#
await client.MetadataManager.DeleteFileMetadataAsync("11111", "enterprise", "marketingCollateral");
```

Set Metadata on a Folder
------------------------

To set metadata on a folder, call
`MetadataManager.SetFolderMetadataAsync(string folderId, Dictionary<string, object> metadata, string scope, string template)`
with the scope and template key of the metadata template, as well as a `Dictionary` containing the metadata keys
and values to set.

> __Note:__ This method will unconditionally apply the provided metadata, overwriting existing metadata
> for the keys provided.  To specifically create or update metadata, see the `CreateFileMetadataAsync()`
> and `UpdateFileMetadataAsync()` methods below.

```c#
var metadataValues = new Dictionary<string, object>()
{
    { "audience", "internal" },
    { "documentType", "Q1 plans" },
    { "competitiveDocument", "no" },
    { "status", "active" },
    { "author": "M. Jones" },
    { "currentState": "proposal" }
};
Dictionary<string, object> metadata = await client.MetadataManager
    .SetFolderMetadataAsync(folderId: "11111", metadataValues, "enterprise", "marketingCollateral");
```

To add new metadata to a folder, call 
`MetadataManager.CreateFolderMetadataAsync(string folderId, Dictionary<string, object> metadata, string scope, string template)`
with a metadata template and a `Dictionary` of key/value pairs to add as metadata.

> __Note:__: This method will only succeed if the provided metadata template is not current applied to the folder,
> otherwise it will fail with a Conflict error.

<!-- sample post_folders_id_metadata_id_id -->
```c#
var metadataValues = new Dictionary<string, object>()
{
    { "audience", "internal" },
    { "documentType", "Q1 plans" },
    { "competitiveDocument", "no" },
    { "status", "active" },
    { "author": "M. Jones" },
    { "currentState": "proposal" }
};
Dictionary<string, object> metadata = await client.MetadataManager
    .CreateFolderMetadataAsync(folderId: "11111", metadataValues, "enterprise", "marketingCollateral");
```

Update a folder's existing metadata by calling
`MetadataManager.UpdateFolderMetadataAsync(string folderId, List<BoxMetadataUpdate> updates, string scope, string template)`
with a list of update operations to apply.

> __Note:__ This method will only succeed if the provided metadata template has already been applied to
> the folder; if the folder does not have existing metadata, this method will fail with a Not Found error.
> This is useful in cases where you know the folder will already have metadata applied, since it will
> save an API call compared to `SetFolderMetadataAsync()`.

<!-- sample put_folders_id_metadata_id_id -->
```c#
var updates = new List<BoxMetadataUpdate>()
{
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.test,
        Path = "/competitiveDocument",
        Value = "no"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.remove,
        Path = "/competitiveDocument"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.test,
        Path = "/status",
        Value = "active"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.replace,
        Path = "/competitiveDocument",
        Value = "inactive"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.test,
        Path = "/author",
        Value = "Jones"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.copy,
        From="/author",
        Path = "/editor"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.test,
        Path = "/currentState",
        Value = "proposal"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.move,
        From = "/currentState",
        Path = "/previousState"
    },
    new BoxMetadataUpdate()
    {
        Op = MetadataUpdateOp.add,
        Path = "/currentState",
        Value = "reviewed"
    }
};
Dictionary<string, object> updatedMetadata = await client.MetadataManager
    .UpdateFolderMetadataAsync("11111", updates, "enterprise", "marketingCollateral");
```

Get Metadata on a Folder
------------------------

Retrieve a specific metadata template on a folder by calling
`MetadataManager.GetFolderMetadataAsync(string folderId, string scope, string template)`
with the ID of the folder and which template to fetch.

<!-- sample get_folders_id_metadata_id_id -->
```c#
Dictionary<string, object> metadata = await client.MetadataManager.
    .GetFolderMetadataAsync(folderId: "11111", "enterprise", "marketingCollateral");
```

You can also get all metadata on a folder by calling
`MetadataManager.GetAllFolderMetadataTemplatesAsync(string folderId)`.

<!-- sample get_folders_id_metadata -->
```c#
BoxMetadataTemplateCollection<Dictionary<string, object>> metadataInstances = await client.MetadataManager
    .GetAllFolderMetadataTemplatesAsync(folderId: "11111");
```

Remove Metadata from a Folder
-----------------------------

A metadata template can be removed from a folder by calling
`MetadataManager.DeleteFolderMetadataAsync(string folderId, string scope, string template)`
with the ID of the folder and the metadata template to remove.

<!-- sample delete_folders_id_metadata_id_id -->
```c#
await client.MetadataManager.DeleteFolderMetadataAsync("11111", "enterprise", "marketingCollateral");
```

Execute Metadata Query
------------------------
There are two types of methods for executing a metadata query, methods without the fields parameter and with it. The method with the fields parameters returns a `BoxItem` object. The method without the fields parameters returns data that is a `BoxMetadataQueryItem` and is **deprecated**. The API will eventually not support this method and the other method should be used instead. Examples of these two types are shown below.

The `MetadataManager.ExecuteMetadataQueryAsync(string from, string ancestorFolderId, IEnumerable<string> fields, string query, Dictionary<string, object> queryParameters, string indexName, List<BoxMetadataQueryOrderBy> orderBy, int limit, string marker, bool autoPaginate)` method queries files and folders based on their metadata and allows for fields to be passed in. A returned `BoxItem` must be cast to a `BoxFile` or `BoxFolder` to get its metadata.
```c#
var queryParams = new Dictionary<string, object>();
queryParams.Add("arg", "Bob Dylan");
List<string> fields = new List<string>();
fields.Add("id");
fields.Add("name");
fields.Add("sha1");
fields.Add("metadata.enterprise_240748.catalogImages.photographer");
BoxCollectionMarkerBased<BoxItem> items = await _metadataManager.ExecuteMetadataQueryAsync(from: "enterprise_67890.catalogImages", query: "photographer = :arg", fields: fields, queryParameters: queryParams, ancestorFolderId: "0", autoPaginate: true);
BoxFile file = (BoxFile) items.Entries[0];
BoxFolder folder = (BoxFolder) items.Entries[1];
string metadataFile = file.Metadata["enterprise_240748"]["catalogImages"]["photographer"].Value;
string metadataFolder = folder.Metadata["enterprise_240748"]["catalogImages"]["photographer"].Value;
```

**Deprecated**

The `MetadataManager.ExecuteMetadataQueryAsync(string from, string ancestorFolderId, string query = null, Dictionary<string, object> queryParameters, string indexName, List<BoxMetadataQueryOrderBy> orderBy, int limit, string marker, bool autoPaginate)` method queries files and folders based on their metadata.
```c#
var queryParams = new Dictionary<string, object>();
queryParams.Add("arg", 100);
List<BoxMetadataQueryOrderBy> orderByList = new List<BoxMetadataQueryOrderBy>();
var orderBy = new BoxMetadataQueryOrderBy()
{
    FieldKey = "amount",
    Direction = BoxSortDirection.ASC
};
orderByList.Add(orderBy);
BoxCollectionMarkerBased<BoxMetadataQueryItem> items = await _metadataManager.ExecuteMetadataQueryAsync(from: "enterprise_123456.someTemplate", query: "amount >= :arg", queryParameters: queryParams, ancestorFolderId: "5555", indexName: "amountAsc", orderBy: orderByList, autoPaginate: true);
```



