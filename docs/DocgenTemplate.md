# IDocgenTemplateManager


- [Create Box Doc Gen template](#create-box-doc-gen-template)
- [List Box Doc Gen templates](#list-box-doc-gen-templates)
- [Delete Box Doc Gen template](#delete-box-doc-gen-template)
- [Get Box Doc Gen template by ID](#get-box-doc-gen-template-by-id)
- [List all Box Doc Gen template tags in template](#list-all-box-doc-gen-template-tags-in-template)
- [Get list of all Box Doc Gen jobs for template](#get-list-of-all-box-doc-gen-jobs-for-template)

## Create Box Doc Gen template

Marks a file as a Box Doc Gen template.

This operation is performed by calling function `CreateDocgenTemplateV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/post-docgen-templates/).

<!-- sample post_docgen_templates_v2025.0 -->
```
await client.DocgenTemplate.CreateDocgenTemplateV2025R0Async(requestBody: new DocGenTemplateCreateRequestV2025R0(file: new FileReferenceV2025R0(id: file.Id)));
```

### Arguments

- requestBody `DocGenTemplateCreateRequestV2025R0`
  - Request body of createDocgenTemplateV2025R0 method
- headers `CreateDocgenTemplateV2025R0Headers`
  - Headers of createDocgenTemplateV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `DocGenTemplateBaseV2025R0`.

The file which has now been marked as a Box Doc Gen template.


## List Box Doc Gen templates

Lists Box Doc Gen templates on which the user is a collaborator.

This operation is performed by calling function `GetDocgenTemplatesV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-docgen-templates/).

<!-- sample get_docgen_templates_v2025.0 -->
```
await client.DocgenTemplate.GetDocgenTemplatesV2025R0Async();
```

### Arguments

- queryParams `GetDocgenTemplatesV2025R0QueryParams`
  - Query parameters of getDocgenTemplatesV2025R0 method
- headers `GetDocgenTemplatesV2025R0Headers`
  - Headers of getDocgenTemplatesV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `DocGenTemplatesV2025R0`.

Returns a collection of templates.


## Delete Box Doc Gen template

Unmarks file as Box Doc Gen template.

This operation is performed by calling function `DeleteDocgenTemplateByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/delete-docgen-templates-id/).

<!-- sample delete_docgen_templates_id_v2025.0 -->
```
await client.DocgenTemplate.DeleteDocgenTemplateByIdV2025R0Async(templateId: NullableUtils.Unwrap(createdDocgenTemplate.File).Id);
```

### Arguments

- templateId `string`
  - ID of the file which will no longer be marked as a Box Doc Gen template. Example: "123"
- headers `DeleteDocgenTemplateByIdV2025R0Headers`
  - Headers of deleteDocgenTemplateByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when a file is no longer marked as a Box Doc Gen template.


## Get Box Doc Gen template by ID

Lists details of a specific Box Doc Gen template.

This operation is performed by calling function `GetDocgenTemplateByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-docgen-templates-id/).

<!-- sample get_docgen_templates_id_v2025.0 -->
```
await client.DocgenTemplate.GetDocgenTemplateByIdV2025R0Async(templateId: NullableUtils.Unwrap(createdDocgenTemplate.File).Id);
```

### Arguments

- templateId `string`
  - The ID of a Box Doc Gen template. Example: 123
- headers `GetDocgenTemplateByIdV2025R0Headers`
  - Headers of getDocgenTemplateByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `DocGenTemplateV2025R0`.

Returns a template.


## List all Box Doc Gen template tags in template

Lists all tags in a Box Doc Gen template.

This operation is performed by calling function `GetDocgenTemplateTagsV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-docgen-templates-id-tags/).

<!-- sample get_docgen_templates_id_tags_v2025.0 -->
```
await client.DocgenTemplate.GetDocgenTemplateTagsV2025R0Async(templateId: NullableUtils.Unwrap(fetchedDocgenTemplate.File).Id);
```

### Arguments

- templateId `string`
  - ID of template. Example: 123
- queryParams `GetDocgenTemplateTagsV2025R0QueryParams`
  - Query parameters of getDocgenTemplateTagsV2025R0 method
- headers `GetDocgenTemplateTagsV2025R0Headers`
  - Headers of getDocgenTemplateTagsV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `DocGenTagsV2025R0`.

A list of document generation template tags.Processing tags for the file.


## Get list of all Box Doc Gen jobs for template

Lists the users jobs which use this template.

This operation is performed by calling function `GetDocgenTemplateJobByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-docgen-template-jobs-id/).

<!-- sample get_docgen_template_jobs_id_v2025.0 -->
```
await client.DocgenTemplate.GetDocgenTemplateJobByIdV2025R0Async(templateId: NullableUtils.Unwrap(fetchedDocgenTemplate.File).Id);
```

### Arguments

- templateId `string`
  - Id of template to fetch jobs for. Example: 123
- queryParams `GetDocgenTemplateJobByIdV2025R0QueryParams`
  - Query parameters of getDocgenTemplateJobByIdV2025R0 method
- headers `GetDocgenTemplateJobByIdV2025R0Headers`
  - Headers of getDocgenTemplateJobByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `DocGenJobsV2025R0`.

A single Box Doc Gen template.


