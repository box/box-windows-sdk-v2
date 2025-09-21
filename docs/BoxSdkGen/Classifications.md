# IClassificationsManager


- [List all classifications](#list-all-classifications)
- [Add classification](#add-classification)
- [Update classification](#update-classification)
- [Add initial classifications](#add-initial-classifications)

## List all classifications

Retrieves the classification metadata template and lists all the
classifications available to this enterprise.

This API can also be called by including the enterprise ID in the
URL explicitly, for example
`/metadata_templates/enterprise_12345/securityClassification-6VMVochwUWo/schema`.

This operation is performed by calling function `GetClassificationTemplate`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-metadata-templates-enterprise-securityClassification-6VMVochwUWo-schema/).

<!-- sample get_metadata_templates_enterprise_securityClassification-6VMVochwUWo_schema -->
```
await client.Classifications.GetClassificationTemplateAsync();
```

### Arguments

- headers `GetClassificationTemplateHeaders`
  - Headers of getClassificationTemplate method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ClassificationTemplate`.

Returns the `securityClassification` metadata template, which contains
a `Box__Security__Classification__Key` field that lists all the
classifications available to this enterprise.


## Add classification

Adds one or more new classifications to the list of classifications
available to the enterprise.

This API can also be called by including the enterprise ID in the
URL explicitly, for example
`/metadata_templates/enterprise_12345/securityClassification-6VMVochwUWo/schema`.

This operation is performed by calling function `AddClassification`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-metadata-templates-enterprise-securityClassification-6VMVochwUWo-schema--add/).

<!-- sample put_metadata_templates_enterprise_securityClassification-6VMVochwUWo_schema#add -->
```
await client.Classifications.AddClassificationAsync(requestBody: Array.AsReadOnly(new [] {new AddClassificationRequestBody(data: new AddClassificationRequestBodyDataField(key: Utils.GetUUID()) { StaticConfig = new AddClassificationRequestBodyDataStaticConfigField() { Classification = new AddClassificationRequestBodyDataStaticConfigClassificationField() { ColorId = 4, ClassificationDefinition = "Other description" } } })}));
```

### Arguments

- requestBody `IReadOnlyList<AddClassificationRequestBody>`
  - Request body of addClassification method
- headers `AddClassificationHeaders`
  - Headers of addClassification method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ClassificationTemplate`.

Returns the updated `securityClassification` metadata template, which
contains a `Box__Security__Classification__Key` field that lists all
the classifications available to this enterprise.


## Update classification

Updates the labels and descriptions of one or more classifications
available to the enterprise.

This API can also be called by including the enterprise ID in the
URL explicitly, for example
`/metadata_templates/enterprise_12345/securityClassification-6VMVochwUWo/schema`.

This operation is performed by calling function `UpdateClassification`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-metadata-templates-enterprise-securityClassification-6VMVochwUWo-schema--update/).

<!-- sample put_metadata_templates_enterprise_securityClassification-6VMVochwUWo_schema#update -->
```
await client.Classifications.UpdateClassificationAsync(requestBody: Array.AsReadOnly(new [] {new UpdateClassificationRequestBody(enumOptionKey: classification.Key, data: new UpdateClassificationRequestBodyDataField(key: updatedClassificationName) { StaticConfig = new UpdateClassificationRequestBodyDataStaticConfigField() { Classification = new UpdateClassificationRequestBodyDataStaticConfigClassificationField() { ColorId = 2, ClassificationDefinition = updatedClassificationDescription } } })}));
```

### Arguments

- requestBody `IReadOnlyList<UpdateClassificationRequestBody>`
  - Request body of updateClassification method
- headers `UpdateClassificationHeaders`
  - Headers of updateClassification method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ClassificationTemplate`.

Returns the updated `securityClassification` metadata template, which
contains a `Box__Security__Classification__Key` field that lists all
the classifications available to this enterprise.


## Add initial classifications

When an enterprise does not yet have any classifications, this API call
initializes the classification template with an initial set of
classifications.

If an enterprise already has a classification, the template will already
exist and instead an API call should be made to add additional
classifications.

This operation is performed by calling function `CreateClassificationTemplate`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-metadata-templates-schema--classifications/).

*Currently we don't have an example for calling `CreateClassificationTemplate` in integration tests*

### Arguments

- requestBody `CreateClassificationTemplateRequestBody`
  - Request body of createClassificationTemplate method
- headers `CreateClassificationTemplateHeaders`
  - Headers of createClassificationTemplate method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ClassificationTemplate`.

Returns a new `securityClassification` metadata template, which
contains a `Box__Security__Classification__Key` field that lists all
the classifications available to this enterprise.


