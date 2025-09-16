# IDevicePinnersManager


- [Get device pin](#get-device-pin)
- [Remove device pin](#remove-device-pin)
- [List enterprise device pins](#list-enterprise-device-pins)

## Get device pin

Retrieves information about an individual device pin.

This operation is performed by calling function `GetDevicePinnerById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-device-pinners-id/).

<!-- sample get_device_pinners_id -->
```
await client.DevicePinners.GetDevicePinnerByIdAsync(devicePinnerId: devicePinnerId);
```

### Arguments

- devicePinnerId `string`
  - The ID of the device pin. Example: "2324234"
- headers `GetDevicePinnerByIdHeaders`
  - Headers of getDevicePinnerById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `DevicePinner`.

Returns information about a single device pin.


## Remove device pin

Deletes an individual device pin.

This operation is performed by calling function `DeleteDevicePinnerById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-device-pinners-id/).

<!-- sample delete_device_pinners_id -->
```
await client.DevicePinners.DeleteDevicePinnerByIdAsync(devicePinnerId: devicePinnerId);
```

### Arguments

- devicePinnerId `string`
  - The ID of the device pin. Example: "2324234"
- headers `DeleteDevicePinnerByIdHeaders`
  - Headers of deleteDevicePinnerById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the pin has been deleted.


## List enterprise device pins

Retrieves all the device pins within an enterprise.

The user must have admin privileges, and the application
needs the "manage enterprise" scope to make this call.

This operation is performed by calling function `GetEnterpriseDevicePinners`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-enterprises-id-device-pinners/).

<!-- sample get_enterprises_id_device_pinners -->
```
await client.DevicePinners.GetEnterpriseDevicePinnersAsync(enterpriseId: enterpriseId);
```

### Arguments

- enterpriseId `string`
  - The ID of the enterprise. Example: "3442311"
- queryParams `GetEnterpriseDevicePinnersQueryParams`
  - Query parameters of getEnterpriseDevicePinners method
- headers `GetEnterpriseDevicePinnersHeaders`
  - Headers of getEnterpriseDevicePinners method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `DevicePinners`.

Returns a list of device pins for a given enterprise.


