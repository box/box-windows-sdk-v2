# IIntegrationMappingsManager


- [List Slack integration mappings](#list-slack-integration-mappings)
- [Create Slack integration mapping](#create-slack-integration-mapping)
- [Update Slack integration mapping](#update-slack-integration-mapping)
- [Delete Slack integration mapping](#delete-slack-integration-mapping)
- [List Teams integration mappings](#list-teams-integration-mappings)
- [Create Teams integration mapping](#create-teams-integration-mapping)
- [Update Teams integration mapping](#update-teams-integration-mapping)
- [Delete Teams integration mapping](#delete-teams-integration-mapping)

## List Slack integration mappings

Lists [Slack integration mappings](https://support.box.com/hc/en-us/articles/4415585987859-Box-as-the-Content-Layer-for-Slack) in a users' enterprise.

You need Admin or Co-Admin role to
use this endpoint.

This operation is performed by calling function `GetSlackIntegrationMapping`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-integration-mappings-slack/).

<!-- sample get_integration_mappings_slack -->
```
await userClient.IntegrationMappings.GetSlackIntegrationMappingAsync();
```

### Arguments

- queryParams `GetSlackIntegrationMappingQueryParams`
  - Query parameters of getSlackIntegrationMapping method
- headers `GetSlackIntegrationMappingHeaders`
  - Headers of getSlackIntegrationMapping method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `IntegrationMappings`.

Returns a collection of integration mappings.


## Create Slack integration mapping

Creates a [Slack integration mapping](https://support.box.com/hc/en-us/articles/4415585987859-Box-as-the-Content-Layer-for-Slack)
by mapping a Slack channel to a Box item.

You need Admin or Co-Admin role to
use this endpoint.

This operation is performed by calling function `CreateSlackIntegrationMapping`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-integration-mappings-slack/).

<!-- sample post_integration_mappings_slack -->
```
await userClient.IntegrationMappings.CreateSlackIntegrationMappingAsync(requestBody: new IntegrationMappingSlackCreateRequest(partnerItem: new IntegrationMappingPartnerItemSlack(id: slackPartnerItemId) { SlackOrgId = slackOrgId }, boxItem: new IntegrationMappingBoxItemSlack(id: folder.Id)));
```

### Arguments

- requestBody `IntegrationMappingSlackCreateRequest`
  - Request body of createSlackIntegrationMapping method
- headers `CreateSlackIntegrationMappingHeaders`
  - Headers of createSlackIntegrationMapping method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `IntegrationMapping`.

Returns the created integration mapping.


## Update Slack integration mapping

Updates a [Slack integration mapping](https://support.box.com/hc/en-us/articles/4415585987859-Box-as-the-Content-Layer-for-Slack).
Supports updating the Box folder ID and options.

You need Admin or Co-Admin role to
use this endpoint.

This operation is performed by calling function `UpdateSlackIntegrationMappingById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-integration-mappings-slack-id/).

<!-- sample put_integration_mappings_slack_id -->
```
await userClient.IntegrationMappings.UpdateSlackIntegrationMappingByIdAsync(integrationMappingId: slackIntegrationMapping.Id, requestBody: new UpdateSlackIntegrationMappingByIdRequestBody() { BoxItem = new IntegrationMappingBoxItemSlack(id: folder.Id) });
```

### Arguments

- integrationMappingId `string`
  - An ID of an integration mapping. Example: "11235432"
- requestBody `UpdateSlackIntegrationMappingByIdRequestBody`
  - Request body of updateSlackIntegrationMappingById method
- headers `UpdateSlackIntegrationMappingByIdHeaders`
  - Headers of updateSlackIntegrationMappingById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `IntegrationMapping`.

Returns the updated integration mapping object.


## Delete Slack integration mapping

Deletes a [Slack integration mapping](https://support.box.com/hc/en-us/articles/4415585987859-Box-as-the-Content-Layer-for-Slack).


You need Admin or Co-Admin role to
use this endpoint.

This operation is performed by calling function `DeleteSlackIntegrationMappingById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-integration-mappings-slack-id/).

<!-- sample delete_integration_mappings_slack_id -->
```
await userClient.IntegrationMappings.DeleteSlackIntegrationMappingByIdAsync(integrationMappingId: slackIntegrationMapping.Id);
```

### Arguments

- integrationMappingId `string`
  - An ID of an integration mapping. Example: "11235432"
- headers `DeleteSlackIntegrationMappingByIdHeaders`
  - Headers of deleteSlackIntegrationMappingById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Empty body in response.


## List Teams integration mappings

Lists [Teams integration mappings](https://support.box.com/hc/en-us/articles/360044681474-Using-Box-for-Teams) in a users' enterprise.
You need Admin or Co-Admin role to
use this endpoint.

This operation is performed by calling function `GetTeamsIntegrationMapping`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-integration-mappings-teams/).

<!-- sample get_integration_mappings_teams -->
```
await userClient.IntegrationMappings.GetTeamsIntegrationMappingAsync();
```

### Arguments

- queryParams `GetTeamsIntegrationMappingQueryParams`
  - Query parameters of getTeamsIntegrationMapping method
- headers `GetTeamsIntegrationMappingHeaders`
  - Headers of getTeamsIntegrationMapping method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `IntegrationMappingsTeams`.

Returns a collection of integration mappings.


## Create Teams integration mapping

Creates a [Teams integration mapping](https://support.box.com/hc/en-us/articles/360044681474-Using-Box-for-Teams)
by mapping a Teams channel to a Box item.
You need Admin or Co-Admin role to
use this endpoint.

This operation is performed by calling function `CreateTeamsIntegrationMapping`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-integration-mappings-teams/).

<!-- sample post_integration_mappings_teams -->
```
await userClient.IntegrationMappings.CreateTeamsIntegrationMappingAsync(requestBody: new IntegrationMappingTeamsCreateRequest(partnerItem: new IntegrationMappingPartnerItemTeamsCreateRequest(type: IntegrationMappingPartnerItemTeamsCreateRequestTypeField.Channel, id: partnerItemId, tenantId: tenantId, teamId: teamId), boxItem: new FolderReference(id: folder.Id)));
```

### Arguments

- requestBody `IntegrationMappingTeamsCreateRequest`
  - Request body of createTeamsIntegrationMapping method
- headers `CreateTeamsIntegrationMappingHeaders`
  - Headers of createTeamsIntegrationMapping method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `IntegrationMappingTeams`.

Returns the created integration mapping.


## Update Teams integration mapping

Updates a [Teams integration mapping](https://support.box.com/hc/en-us/articles/360044681474-Using-Box-for-Teams).
Supports updating the Box folder ID and options.
You need Admin or Co-Admin role to
use this endpoint.

This operation is performed by calling function `UpdateTeamsIntegrationMappingById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-integration-mappings-teams-id/).

<!-- sample put_integration_mappings_teams_id -->
```
await userClient.IntegrationMappings.UpdateTeamsIntegrationMappingByIdAsync(integrationMappingId: integrationMappingId, requestBody: new UpdateTeamsIntegrationMappingByIdRequestBody() { BoxItem = new FolderReference(id: "1234567") });
```

### Arguments

- integrationMappingId `string`
  - An ID of an integration mapping. Example: "11235432"
- requestBody `UpdateTeamsIntegrationMappingByIdRequestBody`
  - Request body of updateTeamsIntegrationMappingById method
- headers `UpdateTeamsIntegrationMappingByIdHeaders`
  - Headers of updateTeamsIntegrationMappingById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `IntegrationMappingTeams`.

Returns the updated integration mapping object.


## Delete Teams integration mapping

Deletes a [Teams integration mapping](https://support.box.com/hc/en-us/articles/360044681474-Using-Box-for-Teams).
You need Admin or Co-Admin role to
use this endpoint.

This operation is performed by calling function `DeleteTeamsIntegrationMappingById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-integration-mappings-teams-id/).

<!-- sample delete_integration_mappings_teams_id -->
```
await userClient.IntegrationMappings.DeleteTeamsIntegrationMappingByIdAsync(integrationMappingId: integrationMappingId);
```

### Arguments

- integrationMappingId `string`
  - An ID of an integration mapping. Example: "11235432"
- headers `DeleteTeamsIntegrationMappingByIdHeaders`
  - Headers of deleteTeamsIntegrationMappingById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Empty body in response.


