# ISignRequestsManager


- [Cancel Box Sign request](#cancel-box-sign-request)
- [Resend Box Sign request](#resend-box-sign-request)
- [Get Box Sign request by ID](#get-box-sign-request-by-id)
- [List Box Sign requests](#list-box-sign-requests)
- [Create Box Sign request](#create-box-sign-request)

## Cancel Box Sign request

Cancels a sign request.

This operation is performed by calling function `CancelSignRequest`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-sign-requests-id-cancel/).

<!-- sample post_sign_requests_id_cancel -->
```
await client.SignRequests.CancelSignRequestAsync(signRequestId: NullableUtils.Unwrap(createdSignRequest.Id));
```

### Arguments

- signRequestId `string`
  - The ID of the signature request. Example: "33243242"
- headers `CancelSignRequestHeaders`
  - Headers of cancelSignRequest method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `SignRequest`.

Returns a Sign Request object.


## Resend Box Sign request

Resends a signature request email to all outstanding signers.

This operation is performed by calling function `ResendSignRequest`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-sign-requests-id-resend/).

*Currently we don't have an example for calling `ResendSignRequest` in integration tests*

### Arguments

- signRequestId `string`
  - The ID of the signature request. Example: "33243242"
- headers `ResendSignRequestHeaders`
  - Headers of resendSignRequest method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the API call was successful.
The email notifications will be sent asynchronously.


## Get Box Sign request by ID

Gets a sign request by ID.

This operation is performed by calling function `GetSignRequestById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-sign-requests-id/).

<!-- sample get_sign_requests_id -->
```
await client.SignRequests.GetSignRequestByIdAsync(signRequestId: NullableUtils.Unwrap(createdSignRequest.Id));
```

### Arguments

- signRequestId `string`
  - The ID of the signature request. Example: "33243242"
- headers `GetSignRequestByIdHeaders`
  - Headers of getSignRequestById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `SignRequest`.

Returns a signature request.


## List Box Sign requests

Gets signature requests created by a user. If the `sign_files` and/or
`parent_folder` are deleted, the signature request will not return in the list.

This operation is performed by calling function `GetSignRequests`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-sign-requests/).

<!-- sample get_sign_requests -->
```
await client.SignRequests.GetSignRequestsAsync();
```

### Arguments

- queryParams `GetSignRequestsQueryParams`
  - Query parameters of getSignRequests method
- headers `GetSignRequestsHeaders`
  - Headers of getSignRequests method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `SignRequests`.

Returns a collection of sign requests.


## Create Box Sign request

Creates a signature request. This involves preparing a document for signing and
sending the signature request to signers.

This operation is performed by calling function `CreateSignRequest`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-sign-requests/).

<!-- sample post_sign_requests -->
```
await client.SignRequests.CreateSignRequestAsync(requestBody: new SignRequestCreateRequest(signers: Array.AsReadOnly(new [] {new SignRequestCreateSigner() { Email = signerEmail, SuppressNotifications = true, DeclinedRedirectUrl = "https://www.box.com", EmbedUrlExternalUserId = "123", IsInPerson = false, LoginRequired = false, Password = "password", Role = SignRequestCreateSignerRoleField.Signer }}), areRemindersEnabled: true, areTextSignaturesEnabled: true, daysValid: 30, declinedRedirectUrl: "https://www.box.com", emailMessage: "Please sign this document", emailSubject: "Sign this document", externalId: "123", externalSystemName: "BoxSignIntegration", isDocumentPreparationNeeded: false, name: "Sign Request", parentFolder: new FolderMini(id: destinationFolder.Id), redirectUrl: "https://www.box.com", prefillTags: Array.AsReadOnly(new [] {new SignRequestPrefillTag() { DateValue = Utils.DateFromString(date: "2035-01-01"), DocumentTagId = "0" }}), sourceFiles: Array.AsReadOnly(new [] {new FileBase(id: fileToSign.Id)})));
```

### Arguments

- requestBody `SignRequestCreateRequest`
  - Request body of createSignRequest method
- headers `CreateSignRequestHeaders`
  - Headers of createSignRequest method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `SignRequest`.

Returns a Box Sign request object.


