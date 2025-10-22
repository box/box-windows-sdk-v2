# IEnterpriseConfigurationsManager


- [Get enterprise configuration](#get-enterprise-configuration)

## Get enterprise configuration

Retrieves the configuration for an enterprise.

This operation is performed by calling function `GetEnterpriseConfigurationByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-enterprise-configurations-id/).

*Currently we don't have an example for calling `GetEnterpriseConfigurationByIdV2025R0` in integration tests*

### Arguments

- enterpriseId `string`
  - The ID of the enterprise. Example: "3442311"
- queryParams `GetEnterpriseConfigurationByIdV2025R0QueryParams`
  - Query parameters of getEnterpriseConfigurationByIdV2025R0 method
- headers `GetEnterpriseConfigurationByIdV2025R0Headers`
  - Headers of getEnterpriseConfigurationByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `EnterpriseConfigurationV2025R0`.

Returns the enterprise configuration.


