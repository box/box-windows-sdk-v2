# ISkillsManager


- [List Box Skill cards on file](#list-box-skill-cards-on-file)
- [Create Box Skill cards on file](#create-box-skill-cards-on-file)
- [Update Box Skill cards on file](#update-box-skill-cards-on-file)
- [Remove Box Skill cards from file](#remove-box-skill-cards-from-file)
- [Update all Box Skill cards on file](#update-all-box-skill-cards-on-file)

## List Box Skill cards on file

List the Box Skills metadata cards that are attached to a file.

This operation is performed by calling function `GetBoxSkillCardsOnFile`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-files-id-metadata-global-boxSkillsCards/).

*Currently we don't have an example for calling `GetBoxSkillCardsOnFile` in integration tests*

### Arguments

- fileId `string`
  - The unique identifier that represents a file.  The ID for any file can be determined by visiting a file in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/files/123` the `file_id` is `123`. Example: "12345"
- headers `GetBoxSkillCardsOnFileHeaders`
  - Headers of getBoxSkillCardsOnFile method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `SkillCardsMetadata`.

Returns all the metadata associated with a file.

This API does not support pagination and will therefore always return
all of the metadata associated to the file.


## Create Box Skill cards on file

Applies one or more Box Skills metadata cards to a file.

This operation is performed by calling function `CreateBoxSkillCardsOnFile`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-files-id-metadata-global-boxSkillsCards/).

*Currently we don't have an example for calling `CreateBoxSkillCardsOnFile` in integration tests*

### Arguments

- fileId `string`
  - The unique identifier that represents a file.  The ID for any file can be determined by visiting a file in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/files/123` the `file_id` is `123`. Example: "12345"
- requestBody `CreateBoxSkillCardsOnFileRequestBody`
  - Request body of createBoxSkillCardsOnFile method
- headers `CreateBoxSkillCardsOnFileHeaders`
  - Headers of createBoxSkillCardsOnFile method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `SkillCardsMetadata`.

Returns the instance of the template that was applied to the file,
including the data that was applied to the template.


## Update Box Skill cards on file

Updates one or more Box Skills metadata cards to a file.

This operation is performed by calling function `UpdateBoxSkillCardsOnFile`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-files-id-metadata-global-boxSkillsCards/).

*Currently we don't have an example for calling `UpdateBoxSkillCardsOnFile` in integration tests*

### Arguments

- fileId `string`
  - The unique identifier that represents a file.  The ID for any file can be determined by visiting a file in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/files/123` the `file_id` is `123`. Example: "12345"
- requestBody `IReadOnlyList<UpdateBoxSkillCardsOnFileRequestBody>`
  - Request body of updateBoxSkillCardsOnFile method
- headers `UpdateBoxSkillCardsOnFileHeaders`
  - Headers of updateBoxSkillCardsOnFile method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `SkillCardsMetadata`.

Returns the updated metadata template, with the
custom template data included.


## Remove Box Skill cards from file

Removes any Box Skills cards metadata from a file.

This operation is performed by calling function `DeleteBoxSkillCardsFromFile`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-files-id-metadata-global-boxSkillsCards/).

*Currently we don't have an example for calling `DeleteBoxSkillCardsFromFile` in integration tests*

### Arguments

- fileId `string`
  - The unique identifier that represents a file.  The ID for any file can be determined by visiting a file in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/files/123` the `file_id` is `123`. Example: "12345"
- headers `DeleteBoxSkillCardsFromFileHeaders`
  - Headers of deleteBoxSkillCardsFromFile method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the cards are
successfully deleted.


## Update all Box Skill cards on file

An alternative method that can be used to overwrite and update all Box Skill
metadata cards on a file.

This operation is performed by calling function `UpdateAllSkillCardsOnFile`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-skill-invocations-id/).

*Currently we don't have an example for calling `UpdateAllSkillCardsOnFile` in integration tests*

### Arguments

- skillId `string`
  - The ID of the skill to apply this metadata for. Example: "33243242"
- requestBody `UpdateAllSkillCardsOnFileRequestBody`
  - Request body of updateAllSkillCardsOnFile method
- headers `UpdateAllSkillCardsOnFileHeaders`
  - Headers of updateAllSkillCardsOnFile method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the card has been successfully updated.


