# ITermsOfServicesManager


- [List terms of services](#list-terms-of-services)
- [Create terms of service](#create-terms-of-service)
- [Get terms of service](#get-terms-of-service)
- [Update terms of service](#update-terms-of-service)

## List terms of services

Returns the current terms of service text and settings
for the enterprise.

This operation is performed by calling function `GetTermsOfService`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-terms-of-services/).

<!-- sample get_terms_of_services -->
```
await client.TermsOfServices.GetTermsOfServiceAsync();
```

### Arguments

- queryParams `GetTermsOfServiceQueryParams`
  - Query parameters of getTermsOfService method
- headers `GetTermsOfServiceHeaders`
  - Headers of getTermsOfService method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `TermsOfServices`.

Returns a collection of terms of service text and settings for the
enterprise.


## Create terms of service

Creates a terms of service for a given enterprise
and type of user.

This operation is performed by calling function `CreateTermsOfService`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-terms-of-services/).

<!-- sample post_terms_of_services -->
```
await client.TermsOfServices.CreateTermsOfServiceAsync(requestBody: new CreateTermsOfServiceRequestBody(status: CreateTermsOfServiceRequestBodyStatusField.Disabled, text: "Test TOS") { TosType = CreateTermsOfServiceRequestBodyTosTypeField.Managed });
```

### Arguments

- requestBody `CreateTermsOfServiceRequestBody`
  - Request body of createTermsOfService method
- headers `CreateTermsOfServiceHeaders`
  - Headers of createTermsOfService method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `TermsOfService`.

Returns a new task object.


## Get terms of service

Fetches a specific terms of service.

This operation is performed by calling function `GetTermsOfServiceById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-terms-of-services-id/).

*Currently we don't have an example for calling `GetTermsOfServiceById` in integration tests*

### Arguments

- termsOfServiceId `string`
  - The ID of the terms of service. Example: "324234"
- headers `GetTermsOfServiceByIdHeaders`
  - Headers of getTermsOfServiceById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `TermsOfService`.

Returns a terms of service object.


## Update terms of service

Updates a specific terms of service.

This operation is performed by calling function `UpdateTermsOfServiceById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-terms-of-services-id/).

<!-- sample put_terms_of_services_id -->
```
await client.TermsOfServices.UpdateTermsOfServiceByIdAsync(termsOfServiceId: tos.Id, requestBody: new UpdateTermsOfServiceByIdRequestBody(status: UpdateTermsOfServiceByIdRequestBodyStatusField.Disabled, text: "TOS"));
```

### Arguments

- termsOfServiceId `string`
  - The ID of the terms of service. Example: "324234"
- requestBody `UpdateTermsOfServiceByIdRequestBody`
  - Request body of updateTermsOfServiceById method
- headers `UpdateTermsOfServiceByIdHeaders`
  - Headers of updateTermsOfServiceById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `TermsOfService`.

Returns an updated terms of service object.


