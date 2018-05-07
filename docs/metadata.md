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
- [Add Metadata to a File](#add-metadata-to-a-file)
- [Get Metadata on a File](#get-metadata-on-a-file)
- [Update Metadata on a File](#update-metadata-on-a-file)
- [Remove Metadata from a File](#remove-metadata-from-a-file)
- [Add Metadata to a Folder](#add-metadata-to-a-folder)
- [Get Metadata on a Folder](#get-metadata-on-a-folder)
- [Update Metadata on a Folder](#update-metadata-on-a-folder)
- [Remove Metadata from a Folder](#remove-metadata-from-a-folder)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Create Metadata Template
------------------------

To create a new metadata template, call
`MetadataManager.CreateMetadataTemplate(BoxMetadataTemplate template)`.

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
[API Documentation](https://docs.box.com/reference#update-metadata-schema)
for more information on the operations available.

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

```c#
BoxMetadataTemplate template = await client.MetadataManager
    .GetMetadataTemplate("enterprise", "marketingCollateral");
```

### Get by ID

To get a specific metadata template by its ID, call the
`MetadataManager.GetMetadataTemplateById(string templateId)`
method with the ID of the template.

```c#
BoxMetadataTemplate template = await client.MetadataManager
    .GetMetadataTemplateById("17f2d715-6acb-45f2-b96a-28b15efc9faa");
```

Get Enterprise Metadata Templates
---------------------------------

Get all metadata templates for the current enterprise and scope by calling
`MetadataManager.GetEnterpriseMetadataAsync(string scope = "enterprise")`.

```c#
BoxEnterpriseMetadataTemplateCollection<BoxMetadataTemplate> templates = await client.MetadataManager
    .GetEnterpriseMetadataAsync();
```

Add Metadata to a File
----------------------

Metadata can be created on a file by calling
`MetadataManager.CreateFileMetadataAsync(string fileId, Dictionary<string, object> metadata, string scope, string template)`
with a metadata template and an object of key/value pairs to add as metadata.

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

Get Metadata on a File
----------------------

Retrieve a specific metadata template on a file by calling
`MetadataManager.GetFileMetadataAsync(string fileId, string scope, string template)`
with the ID of the file and which template to fetch.

```c#
Dictionary<string, object> metadata = await client.MetadataManager.
    .GetFileMetadataAsync(fileId: "11111", "enterprise", "marketingCollateral");
```

You can also get all metadata on a file by calling `MetadataManager.GetAllFileMetadataTemplatesAsync(string fileId)`.

```c#
BoxMetadataTemplateCollection<Dictionary<string, object>> metadataInstances = await client.MetadataManager
    .GetAllFileMetadataTemplatesAsync(fileId: "11111");
```

Update Metadata on a File
-------------------------

Update a file's metadata by calling
`MetadataManager.UpdateFileMetadataAsync(string fileId, List<BoxMetadataUpdate> updates, string scope, string template)`
with the [JSON Patch](http://jsonpatch.com/) formatted operations to perform on the metadata.

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

Remove Metadata from a File
---------------------------

A metadata template can be removed from a file by calling
`MetadataManager.DeleteFileMetadataAsync(string fileId, string scope, string template)`
with the ID of the file and the metadata template to remove.

```c#
await client.MetadataManager.DeleteFileMetadataAsync("11111", "enterprise", "marketingCollateral");
```

Add Metadata to a Folder
------------------------

Metadata can be created on a folder by calling
`MetadataManager.CreateFolderMetadataAsync(string folderId, Dictionary<string, object> metadata, string scope, string template)`
with a metadata template and an object of key/value pairs to add as metadata.

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

Get Metadata on a Folder
------------------------

Retrieve a specific metadata template on a folder by calling
`MetadataManager.GetFolderMetadataAsync(string folderId, string scope, string template)`
with the ID of the folder and which template to fetch.

```c#
Dictionary<string, object> metadata = await client.MetadataManager.
    .GetFolderMetadataAsync(folderId: "11111", "enterprise", "marketingCollateral");
```

You can also get all metadata on a folder by calling
`MetadataManager.GetAllFolderMetadataTemplatesAsync(string folderId)`.

```c#
BoxMetadataTemplateCollection<Dictionary<string, object>> metadataInstances = await client.MetadataManager
    .GetAllFolderMetadataTemplatesAsync(folderId: "11111");
```

Update Metadata on a Folder
---------------------------

Update a folder's metadata by calling
`MetadataManager.UpdateFolderMetadataAsync(string folderId, List<BoxMetadataUpdate> updates, string scope, string template)`
with the [JSON Patch](http://jsonpatch.com/) formatted operations to perform on the metadata.

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

Remove Metadata from a Folder
-----------------------------

A metadata template can be removed from a folder by calling
`MetadataManager.DeleteFolderMetadataAsync(string folderId, string scope, string template)`
with the ID of the folder and the metadata template to remove.

```c#
await client.MetadataManager.DeleteFolderMetadataAsync("11111", "enterprise", "marketingCollateral");
```
