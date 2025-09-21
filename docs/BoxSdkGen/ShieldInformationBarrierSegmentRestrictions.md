# IShieldInformationBarrierSegmentRestrictionsManager


- [Get shield information barrier segment restriction by ID](#get-shield-information-barrier-segment-restriction-by-id)
- [Delete shield information barrier segment restriction by ID](#delete-shield-information-barrier-segment-restriction-by-id)
- [List shield information barrier segment restrictions](#list-shield-information-barrier-segment-restrictions)
- [Create shield information barrier segment restriction](#create-shield-information-barrier-segment-restriction)

## Get shield information barrier segment restriction by ID

Retrieves a shield information barrier segment
restriction based on provided ID.

This operation is performed by calling function `GetShieldInformationBarrierSegmentRestrictionById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-shield-information-barrier-segment-restrictions-id/).

<!-- sample get_shield_information_barrier_segment_restrictions_id -->
```
await client.ShieldInformationBarrierSegmentRestrictions.GetShieldInformationBarrierSegmentRestrictionByIdAsync(shieldInformationBarrierSegmentRestrictionId: segmentRestrictionId);
```

### Arguments

- shieldInformationBarrierSegmentRestrictionId `string`
  - The ID of the shield information barrier segment Restriction. Example: "4563"
- headers `GetShieldInformationBarrierSegmentRestrictionByIdHeaders`
  - Headers of getShieldInformationBarrierSegmentRestrictionById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldInformationBarrierSegmentRestriction`.

Returns the shield information barrier segment
restriction object.


## Delete shield information barrier segment restriction by ID

Delete shield information barrier segment restriction
based on provided ID.

This operation is performed by calling function `DeleteShieldInformationBarrierSegmentRestrictionById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-shield-information-barrier-segment-restrictions-id/).

<!-- sample delete_shield_information_barrier_segment_restrictions_id -->
```
await client.ShieldInformationBarrierSegmentRestrictions.DeleteShieldInformationBarrierSegmentRestrictionByIdAsync(shieldInformationBarrierSegmentRestrictionId: segmentRestrictionId);
```

### Arguments

- shieldInformationBarrierSegmentRestrictionId `string`
  - The ID of the shield information barrier segment Restriction. Example: "4563"
- headers `DeleteShieldInformationBarrierSegmentRestrictionByIdHeaders`
  - Headers of deleteShieldInformationBarrierSegmentRestrictionById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Empty body in response.


## List shield information barrier segment restrictions

Lists shield information barrier segment restrictions
based on provided segment ID.

This operation is performed by calling function `GetShieldInformationBarrierSegmentRestrictions`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-shield-information-barrier-segment-restrictions/).

<!-- sample get_shield_information_barrier_segment_restrictions -->
```
await client.ShieldInformationBarrierSegmentRestrictions.GetShieldInformationBarrierSegmentRestrictionsAsync(queryParams: new GetShieldInformationBarrierSegmentRestrictionsQueryParams(shieldInformationBarrierSegmentId: segmentId));
```

### Arguments

- queryParams `GetShieldInformationBarrierSegmentRestrictionsQueryParams`
  - Query parameters of getShieldInformationBarrierSegmentRestrictions method
- headers `GetShieldInformationBarrierSegmentRestrictionsHeaders`
  - Headers of getShieldInformationBarrierSegmentRestrictions method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldInformationBarrierSegmentRestrictions`.

Returns a paginated list of
shield information barrier segment restriction objects.


## Create shield information barrier segment restriction

Creates a shield information barrier
segment restriction object.

This operation is performed by calling function `CreateShieldInformationBarrierSegmentRestriction`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-shield-information-barrier-segment-restrictions/).

<!-- sample post_shield_information_barrier_segment_restrictions -->
```
await client.ShieldInformationBarrierSegmentRestrictions.CreateShieldInformationBarrierSegmentRestrictionAsync(requestBody: new CreateShieldInformationBarrierSegmentRestrictionRequestBody(restrictedSegment: new CreateShieldInformationBarrierSegmentRestrictionRequestBodyRestrictedSegmentField() { Id = segmentToRestrictId, Type = CreateShieldInformationBarrierSegmentRestrictionRequestBodyRestrictedSegmentTypeField.ShieldInformationBarrierSegment }, shieldInformationBarrierSegment: new CreateShieldInformationBarrierSegmentRestrictionRequestBodyShieldInformationBarrierSegmentField() { Id = segmentId, Type = CreateShieldInformationBarrierSegmentRestrictionRequestBodyShieldInformationBarrierSegmentTypeField.ShieldInformationBarrierSegment }, type: CreateShieldInformationBarrierSegmentRestrictionRequestBodyTypeField.ShieldInformationBarrierSegmentRestriction));
```

### Arguments

- requestBody `CreateShieldInformationBarrierSegmentRestrictionRequestBody`
  - Request body of createShieldInformationBarrierSegmentRestriction method
- headers `CreateShieldInformationBarrierSegmentRestrictionHeaders`
  - Headers of createShieldInformationBarrierSegmentRestriction method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldInformationBarrierSegmentRestriction`.

Returns the newly created Shield
Information Barrier Segment Restriction object.


