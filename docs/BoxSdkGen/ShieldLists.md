# IShieldListsManager


- [Get all shield lists in enterprise](#get-all-shield-lists-in-enterprise)
- [Create shield list](#create-shield-list)
- [Get single shield list by shield list id](#get-single-shield-list-by-shield-list-id)
- [Delete single shield list by shield list id](#delete-single-shield-list-by-shield-list-id)
- [Update shield list](#update-shield-list)

## Get all shield lists in enterprise

Retrieves all shield lists in the enterprise.

This operation is performed by calling function `GetShieldListsV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-shield-lists/).

<!-- sample get_shield_lists_v2025.0 -->
```
await client.ShieldLists.GetShieldListsV2025R0Async();
```

### Arguments

- headers `GetShieldListsV2025R0Headers`
  - Headers of getShieldListsV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldListsV2025R0`.

Returns the list of shield list objects.


## Create shield list

Creates a shield list.

This operation is performed by calling function `CreateShieldListV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/post-shield-lists/).

<!-- sample post_shield_lists_v2025.0 -->
```
await client.ShieldLists.CreateShieldListV2025R0Async(requestBody: new ShieldListsCreateV2025R0(name: shieldListCountryName, content: new ShieldListContentCountryV2025R0(type: ShieldListContentCountryV2025R0TypeField.Country, countryCodes: Array.AsReadOnly(new [] {"US","PL"}))) { Description = "A list of things that are shielded" });
```

### Arguments

- requestBody `ShieldListsCreateV2025R0`
  - Request body of createShieldListV2025R0 method
- headers `CreateShieldListV2025R0Headers`
  - Headers of createShieldListV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldListV2025R0`.

Returns the shield list object.


## Get single shield list by shield list id

Retrieves a single shield list by its ID.

This operation is performed by calling function `GetShieldListByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-shield-lists-id/).

<!-- sample get_shield_lists_id_v2025.0 -->
```
await client.ShieldLists.GetShieldListByIdV2025R0Async(shieldListId: shieldListCountry.Id);
```

### Arguments

- shieldListId `string`
  - The unique identifier that represents a shield list. The ID for any Shield List can be determined by the response from the endpoint fetching all shield lists for the enterprise. Example: "90fb0e17-c332-40ed-b4f9-fa8908fbbb24 "
- headers `GetShieldListByIdV2025R0Headers`
  - Headers of getShieldListByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldListV2025R0`.

Returns the shield list object.


## Delete single shield list by shield list id

Delete a single shield list by its ID.

This operation is performed by calling function `DeleteShieldListByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/delete-shield-lists-id/).

<!-- sample delete_shield_lists_id_v2025.0 -->
```
await client.ShieldLists.DeleteShieldListByIdV2025R0Async(shieldListId: shieldListCountry.Id);
```

### Arguments

- shieldListId `string`
  - The unique identifier that represents a shield list. The ID for any Shield List can be determined by the response from the endpoint fetching all shield lists for the enterprise. Example: "90fb0e17-c332-40ed-b4f9-fa8908fbbb24 "
- headers `DeleteShieldListByIdV2025R0Headers`
  - Headers of deleteShieldListByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Shield List correctly removed. No content in response.


## Update shield list

Updates a shield list.

This operation is performed by calling function `UpdateShieldListByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/put-shield-lists-id/).

<!-- sample put_shield_lists_id_v2025.0 -->
```
await client.ShieldLists.UpdateShieldListByIdV2025R0Async(shieldListId: shieldListCountry.Id, requestBody: new ShieldListsUpdateV2025R0(name: shieldListCountryName, content: new ShieldListContentCountryV2025R0(type: ShieldListContentCountryV2025R0TypeField.Country, countryCodes: Array.AsReadOnly(new [] {"US"}))) { Description = "Updated description" });
```

### Arguments

- shieldListId `string`
  - The unique identifier that represents a shield list. The ID for any Shield List can be determined by the response from the endpoint fetching all shield lists for the enterprise. Example: "90fb0e17-c332-40ed-b4f9-fa8908fbbb24 "
- requestBody `ShieldListsUpdateV2025R0`
  - Request body of updateShieldListByIdV2025R0 method
- headers `UpdateShieldListByIdV2025R0Headers`
  - Headers of updateShieldListByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldListV2025R0`.

Returns the shield list object.


