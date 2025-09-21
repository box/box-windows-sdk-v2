# ISignTemplatesManager


- [List Box Sign templates](#list-box-sign-templates)
- [Get Box Sign template by ID](#get-box-sign-template-by-id)

## List Box Sign templates

Gets Box Sign templates created by a user.

This operation is performed by calling function `GetSignTemplates`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-sign-templates/).

<!-- sample get_sign_templates -->
```
await client.SignTemplates.GetSignTemplatesAsync(queryParams: new GetSignTemplatesQueryParams() { Limit = 2 });
```

### Arguments

- queryParams `GetSignTemplatesQueryParams`
  - Query parameters of getSignTemplates method
- headers `GetSignTemplatesHeaders`
  - Headers of getSignTemplates method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `SignTemplates`.

Returns a collection of templates.


## Get Box Sign template by ID

Fetches details of a specific Box Sign template.

This operation is performed by calling function `GetSignTemplateById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-sign-templates-id/).

<!-- sample get_sign_templates_id -->
```
await client.SignTemplates.GetSignTemplateByIdAsync(templateId: NullableUtils.Unwrap(NullableUtils.Unwrap(signTemplates.Entries)[0].Id));
```

### Arguments

- templateId `string`
  - The ID of a Box Sign template. Example: "123075213-7d117509-8f05-42e4-a5ef-5190a319d41d"
- headers `GetSignTemplateByIdHeaders`
  - Headers of getSignTemplateById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `SignTemplate`.

Returns details of a template.


