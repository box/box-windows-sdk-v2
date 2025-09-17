# IShieldInformationBarriersManager


- [Get shield information barrier with specified ID](#get-shield-information-barrier-with-specified-id)
- [Add changed status of shield information barrier with specified ID](#add-changed-status-of-shield-information-barrier-with-specified-id)
- [List shield information barriers](#list-shield-information-barriers)
- [Create shield information barrier](#create-shield-information-barrier)

## Get shield information barrier with specified ID

Get shield information barrier based on provided ID.

This operation is performed by calling function `GetShieldInformationBarrierById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-shield-information-barriers-id/).

<!-- sample get_shield_information_barriers_id -->
```
await client.ShieldInformationBarriers.GetShieldInformationBarrierByIdAsync(shieldInformationBarrierId: barrierId);
```

### Arguments

- shieldInformationBarrierId `string`
  - The ID of the shield information barrier. Example: "1910967"
- headers `GetShieldInformationBarrierByIdHeaders`
  - Headers of getShieldInformationBarrierById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldInformationBarrier`.

Returns the shield information barrier object.


## Add changed status of shield information barrier with specified ID

Change status of shield information barrier with the specified ID.

This operation is performed by calling function `UpdateShieldInformationBarrierStatus`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-shield-information-barriers-change-status/).

<!-- sample post_shield_information_barriers_change_status -->
```
await client.ShieldInformationBarriers.UpdateShieldInformationBarrierStatusAsync(requestBody: new UpdateShieldInformationBarrierStatusRequestBody(id: barrierId, status: UpdateShieldInformationBarrierStatusRequestBodyStatusField.Disabled));
```

### Arguments

- requestBody `UpdateShieldInformationBarrierStatusRequestBody`
  - Request body of updateShieldInformationBarrierStatus method
- headers `UpdateShieldInformationBarrierStatusHeaders`
  - Headers of updateShieldInformationBarrierStatus method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldInformationBarrier`.

Returns the updated shield information barrier object.


## List shield information barriers

Retrieves a list of shield information barrier objects
for the enterprise of JWT.

This operation is performed by calling function `GetShieldInformationBarriers`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-shield-information-barriers/).

<!-- sample get_shield_information_barriers -->
```
await client.ShieldInformationBarriers.GetShieldInformationBarriersAsync();
```

### Arguments

- queryParams `GetShieldInformationBarriersQueryParams`
  - Query parameters of getShieldInformationBarriers method
- headers `GetShieldInformationBarriersHeaders`
  - Headers of getShieldInformationBarriers method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldInformationBarriers`.

Returns a paginated list of
shield information barrier objects,
empty list if currently no barrier.


## Create shield information barrier

Creates a shield information barrier to
separate individuals/groups within the same
firm and prevents confidential information passing between them.

This operation is performed by calling function `CreateShieldInformationBarrier`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-shield-information-barriers/).

<!-- sample post_shield_information_barriers -->
```
await client.ShieldInformationBarriers.CreateShieldInformationBarrierAsync(requestBody: new CreateShieldInformationBarrierRequestBody(enterprise: new EnterpriseBase() { Id = enterpriseId }));
```

### Arguments

- requestBody `CreateShieldInformationBarrierRequestBody`
  - Request body of createShieldInformationBarrier method
- headers `CreateShieldInformationBarrierHeaders`
  - Headers of createShieldInformationBarrier method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldInformationBarrier`.

Returns a new shield information barrier object.


