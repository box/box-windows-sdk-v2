# IMetadataTaxonomiesManager


- [Create metadata taxonomy](#create-metadata-taxonomy)
- [Get metadata taxonomies for namespace](#get-metadata-taxonomies-for-namespace)
- [Get metadata taxonomy by taxonomy key](#get-metadata-taxonomy-by-taxonomy-key)
- [Update metadata taxonomy](#update-metadata-taxonomy)
- [Remove metadata taxonomy](#remove-metadata-taxonomy)
- [Create metadata taxonomy levels](#create-metadata-taxonomy-levels)
- [Update metadata taxonomy level](#update-metadata-taxonomy-level)
- [Add metadata taxonomy level](#add-metadata-taxonomy-level)
- [Delete metadata taxonomy level](#delete-metadata-taxonomy-level)
- [List metadata taxonomy nodes](#list-metadata-taxonomy-nodes)
- [Create metadata taxonomy node](#create-metadata-taxonomy-node)
- [Get metadata taxonomy node by ID](#get-metadata-taxonomy-node-by-id)
- [Update metadata taxonomy node](#update-metadata-taxonomy-node)
- [Remove metadata taxonomy node](#remove-metadata-taxonomy-node)
- [List metadata template's options for taxonomy field](#list-metadata-templates-options-for-taxonomy-field)

## Create metadata taxonomy

Creates a new metadata taxonomy that can be used in
metadata templates.

This operation is performed by calling function `CreateMetadataTaxonomy`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-metadata-taxonomies/).

*Currently we don't have an example for calling `CreateMetadataTaxonomy` in integration tests*

### Arguments

- requestBody `CreateMetadataTaxonomyRequestBody`
  - Request body of createMetadataTaxonomy method
- headers `CreateMetadataTaxonomyHeaders`
  - Headers of createMetadataTaxonomy method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomy`.

The schema representing the metadata taxonomy created.


## Get metadata taxonomies for namespace

Used to retrieve all metadata taxonomies in a namespace.

This operation is performed by calling function `GetMetadataTaxonomies`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-metadata-taxonomies-id/).

*Currently we don't have an example for calling `GetMetadataTaxonomies` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- queryParams `GetMetadataTaxonomiesQueryParams`
  - Query parameters of getMetadataTaxonomies method
- headers `GetMetadataTaxonomiesHeaders`
  - Headers of getMetadataTaxonomies method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomies`.

Returns all of the metadata taxonomies within a namespace
and their corresponding schema.


## Get metadata taxonomy by taxonomy key

Used to retrieve a metadata taxonomy by taxonomy key.

This operation is performed by calling function `GetMetadataTaxonomyByKey`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-metadata-taxonomies-id-id/).

*Currently we don't have an example for calling `GetMetadataTaxonomyByKey` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- headers `GetMetadataTaxonomyByKeyHeaders`
  - Headers of getMetadataTaxonomyByKey method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomy`.

Returns the metadata taxonomy identified by the taxonomy key.


## Update metadata taxonomy

Updates an existing metadata taxonomy.

This operation is performed by calling function `UpdateMetadataTaxonomy`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/patch-metadata-taxonomies-id-id/).

*Currently we don't have an example for calling `UpdateMetadataTaxonomy` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- requestBody `UpdateMetadataTaxonomyRequestBody`
  - Request body of updateMetadataTaxonomy method
- headers `UpdateMetadataTaxonomyHeaders`
  - Headers of updateMetadataTaxonomy method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomy`.

The schema representing the updated metadata taxonomy.


## Remove metadata taxonomy

Delete a metadata taxonomy.
This deletion is permanent and cannot be reverted.

This operation is performed by calling function `DeleteMetadataTaxonomy`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-metadata-taxonomies-id-id/).

*Currently we don't have an example for calling `DeleteMetadataTaxonomy` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- headers `DeleteMetadataTaxonomyHeaders`
  - Headers of deleteMetadataTaxonomy method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the metadata taxonomy is successfully deleted.


## Create metadata taxonomy levels

Creates new metadata taxonomy levels.

This operation is performed by calling function `CreateMetadataTaxonomyLevel`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-metadata-taxonomies-id-id-levels/).

*Currently we don't have an example for calling `CreateMetadataTaxonomyLevel` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- requestBody `IReadOnlyList<MetadataTaxonomyLevel>`
  - Request body of createMetadataTaxonomyLevel method
- headers `CreateMetadataTaxonomyLevelHeaders`
  - Headers of createMetadataTaxonomyLevel method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomyLevels`.

Returns an array of all taxonomy levels.


## Update metadata taxonomy level

Updates an existing metadata taxonomy level.

This operation is performed by calling function `PatchMetadataTaxonomiesIdIdLevelsId`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/patch-metadata-taxonomies-id-id-levels-id/).

*Currently we don't have an example for calling `PatchMetadataTaxonomiesIdIdLevelsId` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- levelIndex `long`
  - The index of the metadata taxonomy level. Example: 1
- requestBody `PatchMetadataTaxonomiesIdIdLevelsIdRequestBody`
  - Request body of patchMetadataTaxonomiesIdIdLevelsId method
- headers `PatchMetadataTaxonomiesIdIdLevelsIdHeaders`
  - Headers of patchMetadataTaxonomiesIdIdLevelsId method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomyLevel`.

The updated taxonomy level.


## Add metadata taxonomy level

Creates a new metadata taxonomy level and appends it to the existing levels.
If there are no levels defined yet, this will create the first level.

This operation is performed by calling function `AddMetadataTaxonomyLevel`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-metadata-taxonomies-id-id-levels:append/).

*Currently we don't have an example for calling `AddMetadataTaxonomyLevel` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- requestBody `AddMetadataTaxonomyLevelRequestBody`
  - Request body of addMetadataTaxonomyLevel method
- headers `AddMetadataTaxonomyLevelHeaders`
  - Headers of addMetadataTaxonomyLevel method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomyLevels`.

Returns an array of all taxonomy levels.


## Delete metadata taxonomy level

Deletes the last level of the metadata taxonomy.

This operation is performed by calling function `DeleteMetadataTaxonomyLevel`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-metadata-taxonomies-id-id-levels:trim/).

*Currently we don't have an example for calling `DeleteMetadataTaxonomyLevel` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- headers `DeleteMetadataTaxonomyLevelHeaders`
  - Headers of deleteMetadataTaxonomyLevel method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomyLevels`.

Returns an array of all taxonomy levels.


## List metadata taxonomy nodes

Used to retrieve metadata taxonomy nodes based on the parameters specified. 
Results are sorted in lexicographic order unless a `query` parameter is passed. 
With a `query` parameter specified, results are sorted in order of relevance.

This operation is performed by calling function `GetMetadataTaxonomyNodes`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-metadata-taxonomies-id-id-nodes/).

*Currently we don't have an example for calling `GetMetadataTaxonomyNodes` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- queryParams `GetMetadataTaxonomyNodesQueryParams`
  - Query parameters of getMetadataTaxonomyNodes method
- headers `GetMetadataTaxonomyNodesHeaders`
  - Headers of getMetadataTaxonomyNodes method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomyNodes`.

Returns a list of the taxonomy nodes that match the specified parameters.


## Create metadata taxonomy node

Creates a new metadata taxonomy node.

This operation is performed by calling function `CreateMetadataTaxonomyNode`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-metadata-taxonomies-id-id-nodes/).

*Currently we don't have an example for calling `CreateMetadataTaxonomyNode` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- requestBody `CreateMetadataTaxonomyNodeRequestBody`
  - Request body of createMetadataTaxonomyNode method
- headers `CreateMetadataTaxonomyNodeHeaders`
  - Headers of createMetadataTaxonomyNode method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomyNode`.

The schema representing the taxonomy node created.


## Get metadata taxonomy node by ID

Retrieves a metadata taxonomy node by its identifier.

This operation is performed by calling function `GetMetadataTaxonomyNodeById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-metadata-taxonomies-id-id-nodes-id/).

*Currently we don't have an example for calling `GetMetadataTaxonomyNodeById` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- nodeId `string`
  - The identifier of the metadata taxonomy node. Example: "14d3d433-c77f-49c5-b146-9dea370f6e32"
- headers `GetMetadataTaxonomyNodeByIdHeaders`
  - Headers of getMetadataTaxonomyNodeById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomyNode`.

Returns the metadata taxonomy node that matches the identifier.


## Update metadata taxonomy node

Updates an existing metadata taxonomy node.

This operation is performed by calling function `UpdateMetadataTaxonomyNode`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/patch-metadata-taxonomies-id-id-nodes-id/).

*Currently we don't have an example for calling `UpdateMetadataTaxonomyNode` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- nodeId `string`
  - The identifier of the metadata taxonomy node. Example: "14d3d433-c77f-49c5-b146-9dea370f6e32"
- requestBody `UpdateMetadataTaxonomyNodeRequestBody`
  - Request body of updateMetadataTaxonomyNode method
- headers `UpdateMetadataTaxonomyNodeHeaders`
  - Headers of updateMetadataTaxonomyNode method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomyNode`.

The schema representing the updated taxonomy node.


## Remove metadata taxonomy node

Delete a metadata taxonomy node.
This deletion is permanent and cannot be reverted.
Only metadata taxonomy nodes without any children can be deleted.

This operation is performed by calling function `DeleteMetadataTaxonomyNode`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-metadata-taxonomies-id-id-nodes-id/).

*Currently we don't have an example for calling `DeleteMetadataTaxonomyNode` in integration tests*

### Arguments

- namespaceParam `string`
  - The namespace of the metadata taxonomy. Example: "enterprise_123456"
- taxonomyKey `string`
  - The key of the metadata taxonomy. Example: "geography"
- nodeId `string`
  - The identifier of the metadata taxonomy node. Example: "14d3d433-c77f-49c5-b146-9dea370f6e32"
- headers `DeleteMetadataTaxonomyNodeHeaders`
  - Headers of deleteMetadataTaxonomyNode method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the metadata taxonomy node is successfully deleted.


## List metadata template's options for taxonomy field

Used to retrieve metadata taxonomy nodes which are available for the taxonomy field based 
on its configuration and the parameters specified. 
Results are sorted in lexicographic order unless a `query` parameter is passed. 
With a `query` parameter specified, results are sorted in order of relevance.

This operation is performed by calling function `GetMetadataTemplateFieldOptions`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-metadata-templates-id-id-fields-id-options/).

*Currently we don't have an example for calling `GetMetadataTemplateFieldOptions` in integration tests*

### Arguments

- scope `GetMetadataTemplateFieldOptionsScope`
  - The scope of the metadata template. Example: "global"
- templateKey `string`
  - The name of the metadata template. Example: "properties"
- fieldKey `string`
  - The key of the metadata taxonomy field in the template. Example: "geography"
- queryParams `GetMetadataTemplateFieldOptionsQueryParams`
  - Query parameters of getMetadataTemplateFieldOptions method
- headers `GetMetadataTemplateFieldOptionsHeaders`
  - Headers of getMetadataTemplateFieldOptions method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataTaxonomyNodes`.

Returns a list of the taxonomy nodes that match the specified parameters.


