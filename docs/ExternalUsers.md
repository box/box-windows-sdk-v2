# IExternalUsersManager


- [Submit job to delete external users](#submit-job-to-delete-external-users)

## Submit job to delete external users

Delete external users from current user enterprise. This will remove each
external user from all invited collaborations within the current enterprise.

This operation is performed by calling function `SubmitJobToDeleteExternalUsersV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/post-external-users-submit-delete-job/).

<!-- sample post_external_users_submit_delete_job_v2025.0 -->
```
await client.ExternalUsers.SubmitJobToDeleteExternalUsersV2025R0Async(requestBody: new ExternalUsersSubmitDeleteJobRequestV2025R0(externalUsers: Array.AsReadOnly(new [] {new UserReferenceV2025R0(id: Utils.GetEnvVar(name: "BOX_EXTERNAL_USER_ID"))})));
```

### Arguments

- requestBody `ExternalUsersSubmitDeleteJobRequestV2025R0`
  - Request body of submitJobToDeleteExternalUsersV2025R0 method
- headers `SubmitJobToDeleteExternalUsersV2025R0Headers`
  - Headers of submitJobToDeleteExternalUsersV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `ExternalUsersSubmitDeleteJobResponseV2025R0`.




