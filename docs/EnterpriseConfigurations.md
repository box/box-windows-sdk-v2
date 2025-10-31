# IEnterpriseConfigurationsManager


- [Get enterprise configuration](#get-enterprise-configuration)

## Get enterprise configuration

Retrieves the configuration for an enterprise.

This operation is performed by calling function `GetEnterpriseConfigurationByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-enterprise-configurations-id/).

<!-- sample get_enterprise_configurations_id_v2025.0 -->
```
await adminClient.EnterpriseConfigurations.GetEnterpriseConfigurationByIdV2025R0Async(enterpriseId: enterpriseId, queryParams: new GetEnterpriseConfigurationByIdV2025R0QueryParams(categories: Array.AsReadOnly(new [] {"user_settings","content_and_sharing","security","shield"})));
```

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


