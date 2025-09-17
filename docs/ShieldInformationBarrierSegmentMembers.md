# IShieldInformationBarrierSegmentMembersManager


- [Get shield information barrier segment member by ID](#get-shield-information-barrier-segment-member-by-id)
- [Delete shield information barrier segment member by ID](#delete-shield-information-barrier-segment-member-by-id)
- [List shield information barrier segment members](#list-shield-information-barrier-segment-members)
- [Create shield information barrier segment member](#create-shield-information-barrier-segment-member)

## Get shield information barrier segment member by ID

Retrieves a shield information barrier
segment member by its ID.

This operation is performed by calling function `GetShieldInformationBarrierSegmentMemberById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-shield-information-barrier-segment-members-id/).

<!-- sample get_shield_information_barrier_segment_members_id -->
```
await client.ShieldInformationBarrierSegmentMembers.GetShieldInformationBarrierSegmentMemberByIdAsync(shieldInformationBarrierSegmentMemberId: NullableUtils.Unwrap(segmentMember.Id));
```

### Arguments

- shieldInformationBarrierSegmentMemberId `string`
  - The ID of the shield information barrier segment Member. Example: "7815"
- headers `GetShieldInformationBarrierSegmentMemberByIdHeaders`
  - Headers of getShieldInformationBarrierSegmentMemberById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldInformationBarrierSegmentMember`.

Returns the shield information barrier segment member object.


## Delete shield information barrier segment member by ID

Deletes a shield information barrier
segment member based on provided ID.

This operation is performed by calling function `DeleteShieldInformationBarrierSegmentMemberById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-shield-information-barrier-segment-members-id/).

<!-- sample delete_shield_information_barrier_segment_members_id -->
```
await client.ShieldInformationBarrierSegmentMembers.DeleteShieldInformationBarrierSegmentMemberByIdAsync(shieldInformationBarrierSegmentMemberId: NullableUtils.Unwrap(segmentMember.Id));
```

### Arguments

- shieldInformationBarrierSegmentMemberId `string`
  - The ID of the shield information barrier segment Member. Example: "7815"
- headers `DeleteShieldInformationBarrierSegmentMemberByIdHeaders`
  - Headers of deleteShieldInformationBarrierSegmentMemberById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response if the
segment member was deleted successfully.


## List shield information barrier segment members

Lists shield information barrier segment members
based on provided segment IDs.

This operation is performed by calling function `GetShieldInformationBarrierSegmentMembers`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-shield-information-barrier-segment-members/).

<!-- sample get_shield_information_barrier_segment_members -->
```
await client.ShieldInformationBarrierSegmentMembers.GetShieldInformationBarrierSegmentMembersAsync(queryParams: new GetShieldInformationBarrierSegmentMembersQueryParams(shieldInformationBarrierSegmentId: NullableUtils.Unwrap(segment.Id)));
```

### Arguments

- queryParams `GetShieldInformationBarrierSegmentMembersQueryParams`
  - Query parameters of getShieldInformationBarrierSegmentMembers method
- headers `GetShieldInformationBarrierSegmentMembersHeaders`
  - Headers of getShieldInformationBarrierSegmentMembers method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldInformationBarrierSegmentMembers`.

Returns a paginated list of
shield information barrier segment member objects.


## Create shield information barrier segment member

Creates a new shield information barrier segment member.

This operation is performed by calling function `CreateShieldInformationBarrierSegmentMember`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-shield-information-barrier-segment-members/).

<!-- sample post_shield_information_barrier_segment_members -->
```
await client.ShieldInformationBarrierSegmentMembers.CreateShieldInformationBarrierSegmentMemberAsync(requestBody: new CreateShieldInformationBarrierSegmentMemberRequestBody(shieldInformationBarrierSegment: new CreateShieldInformationBarrierSegmentMemberRequestBodyShieldInformationBarrierSegmentField() { Id = NullableUtils.Unwrap(segment.Id), Type = CreateShieldInformationBarrierSegmentMemberRequestBodyShieldInformationBarrierSegmentTypeField.ShieldInformationBarrierSegment }, user: new UserBase(id: Utils.GetEnvVar(name: "USER_ID"))));
```

### Arguments

- requestBody `CreateShieldInformationBarrierSegmentMemberRequestBody`
  - Request body of createShieldInformationBarrierSegmentMember method
- headers `CreateShieldInformationBarrierSegmentMemberHeaders`
  - Headers of createShieldInformationBarrierSegmentMember method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ShieldInformationBarrierSegmentMember`.

Returns a new shield information barrier segment member object.


