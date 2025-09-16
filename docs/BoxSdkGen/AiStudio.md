# IAiStudioManager


- [List AI agents](#list-ai-agents)
- [Create AI agent](#create-ai-agent)
- [Update AI agent](#update-ai-agent)
- [Get AI agent by agent ID](#get-ai-agent-by-agent-id)
- [Delete AI agent](#delete-ai-agent)

## List AI agents

Lists AI agents based on the provided parameters.

This operation is performed by calling function `GetAiAgents`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-ai-agents/).

<!-- sample get_ai_agents -->
```
await client.AiStudio.GetAiAgentsAsync();
```

### Arguments

- queryParams `GetAiAgentsQueryParams`
  - Query parameters of getAiAgents method
- headers `GetAiAgentsHeaders`
  - Headers of getAiAgents method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AiMultipleAgentResponse`.

A successful response including the agents list.


## Create AI agent

Creates an AI agent. At least one of the following capabilities must be provided: `ask`, `text_gen`, `extract`.

This operation is performed by calling function `CreateAiAgent`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-ai-agents/).

<!-- sample post_ai_agents -->
```
await client.AiStudio.CreateAiAgentAsync(requestBody: new CreateAiAgent(name: agentName, accessState: "enabled") { Ask = new AiStudioAgentAsk(accessState: "enabled", description: "desc1") });
```

### Arguments

- requestBody `CreateAiAgent`
  - Request body of createAiAgent method
- headers `CreateAiAgentHeaders`
  - Headers of createAiAgent method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AiSingleAgentResponseFull`.

Definition of created AI agent.


## Update AI agent

Updates an AI agent.

This operation is performed by calling function `UpdateAiAgentById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-ai-agents-id/).

<!-- sample put_ai_agents_id -->
```
await client.AiStudio.UpdateAiAgentByIdAsync(agentId: createdAgent.Id, requestBody: new CreateAiAgent(name: agentName, accessState: "enabled") { Ask = new AiStudioAgentAsk(accessState: "disabled", description: "desc2") });
```

### Arguments

- agentId `string`
  - The ID of the agent to update. Example: "1234"
- requestBody `CreateAiAgent`
  - Request body of updateAiAgentById method
- headers `UpdateAiAgentByIdHeaders`
  - Headers of updateAiAgentById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AiSingleAgentResponseFull`.

Definition of created AI agent.


## Get AI agent by agent ID

Gets an AI Agent using the `agent_id` parameter.

This operation is performed by calling function `GetAiAgentById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-ai-agents-id/).

<!-- sample get_ai_agents_id -->
```
await client.AiStudio.GetAiAgentByIdAsync(agentId: createdAgent.Id, queryParams: new GetAiAgentByIdQueryParams() { Fields = Array.AsReadOnly(new [] {"ask"}) });
```

### Arguments

- agentId `string`
  - The agent id to get. Example: "1234"
- queryParams `GetAiAgentByIdQueryParams`
  - Query parameters of getAiAgentById method
- headers `GetAiAgentByIdHeaders`
  - Headers of getAiAgentById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AiSingleAgentResponseFull`.

A successful response including the agent.


## Delete AI agent

Deletes an AI agent using the provided parameters.

This operation is performed by calling function `DeleteAiAgentById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-ai-agents-id/).

<!-- sample delete_ai_agents_id -->
```
await client.AiStudio.DeleteAiAgentByIdAsync(agentId: createdAgent.Id);
```

### Arguments

- agentId `string`
  - The ID of the agent to delete. Example: "1234"
- headers `DeleteAiAgentByIdHeaders`
  - Headers of deleteAiAgentById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

A successful response with no content.


