# IAiSessionsManager


- [List AI sessions](#list-ai-sessions)
- [Delete AI session](#delete-ai-session)

## List AI sessions

Returns a marker-paginated list of the AI sessions that the authenticated
user can read and that were created through the Box public API. Entries are
ordered by `sort` and `direction`, defaulting to `updated_at` descending so
the most recently active session appears first.

The access token must have both the `ai.readwrite` and `root_readwrite`
scopes.

A page may contain fewer than `limit` entries while further pages remain.
Treat a non-null `next_marker` as the only signal that more results exist.

This operation is performed by calling function `GetAiSessionsV2026R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2026.0/get-ai-sessions/).

<!-- sample get_ai_sessions_v2026.0 -->
```
await client.AiSessions.GetAiSessionsV2026R0Async(queryParams: new GetAiSessionsV2026R0QueryParams(source: GetAiSessionsV2026R0QueryParamsSourceField.Api));
```

### Arguments

- queryParams `GetAiSessionsV2026R0QueryParams`
  - Query parameters of getAiSessionsV2026R0 method
- headers `GetAiSessionsV2026R0Headers`
  - Headers of getAiSessionsV2026R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AiSessionsV2026R0`.

Returns a paginated list of AI sessions.


## Delete AI session

Deletes an AI session created through the Box public API, together with its
agent tasks. The session's agent-generated files and uploaded files are
moved to the trash. Files attached as sources from elsewhere in Box are not
affected. Deleting a session does not stop a running task or notify the
agent.

The access token must have both the `ai.readwrite` and `root_readwrite`
scopes.

This operation is performed by calling function `DeleteAiSessionByIdV2026R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2026.0/delete-ai-sessions-id/).

<!-- sample delete_ai_sessions_id_v2026.0 -->
```
await client.AiSessions.DeleteAiSessionByIdV2026R0Async(aiSessionId: aiSessionId);
```

### Arguments

- aiSessionId `string`
  - The unique identifier of the AI session. This is the same value returned as `id` by the list endpoint and used as the A2A `contextId`. Example: "23489723"
- headers `DeleteAiSessionByIdV2026R0Headers`
  - Headers of deleteAiSessionByIdV2026R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the AI session was successfully deleted.


