# IAiManager


- [Ask question](#ask-question)
- [Generate text](#generate-text)
- [Get AI agent default configuration](#get-ai-agent-default-configuration)
- [Extract metadata (freeform)](#extract-metadata-freeform)
- [Extract metadata (structured)](#extract-metadata-structured)

## Ask question

Sends an AI request to supported LLMs and returns an answer specifically focused on the user's question given the provided context.

This operation is performed by calling function `CreateAiAsk`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-ai-ask/).

<!-- sample post_ai_ask -->
```
await client.Ai.CreateAiAskAsync(requestBody: new AiAsk(mode: AiAskModeField.SingleItemQa, prompt: "which direction sun rises", items: Array.AsReadOnly(new [] {new AiItemAsk(id: fileToAsk.Id, type: AiItemAskTypeField.File) { Content = "Sun rises in the East" }})));
```

### Arguments

- requestBody `AiAsk`
  - Request body of createAiAsk method
- headers `CreateAiAskHeaders`
  - Headers of createAiAsk method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AiResponseFull?`.

A successful response including the answer from the LLM.No content is available to answer the question. This is returned when the request item is a hub, but content in the hubs is not indexed. To ensure content in the hub is indexed, make sure Box AI for Hubs in the Admin Console was enabled before hub creation.


## Generate text

Sends an AI request to supported Large Language Models (LLMs) and returns generated text based on the provided prompt.

This operation is performed by calling function `CreateAiTextGen`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-ai-text-gen/).

<!-- sample post_ai_text_gen -->
```
await client.Ai.CreateAiTextGenAsync(requestBody: new AiTextGen(prompt: "Parapharse the document.s", items: Array.AsReadOnly(new [] {new AiTextGenItemsField(id: fileToAsk.Id, type: AiTextGenItemsTypeField.File) { Content = "The Earth goes around the sun. Sun rises in the East in the morning." }})) { DialogueHistory = Array.AsReadOnly(new [] {new AiDialogueHistory() { Prompt = "What does the earth go around?", Answer = "The sun", CreatedAt = Utils.DateTimeFromString(dateTime: "2021-01-01T00:00:00Z") },new AiDialogueHistory() { Prompt = "On Earth, where does the sun rise?", Answer = "East", CreatedAt = Utils.DateTimeFromString(dateTime: "2021-01-01T00:00:00Z") }}) });
```

### Arguments

- requestBody `AiTextGen`
  - Request body of createAiTextGen method
- headers `CreateAiTextGenHeaders`
  - Headers of createAiTextGen method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AiResponse`.

A successful response including the answer from the LLM.


## Get AI agent default configuration

Get the AI agent default config.

This operation is performed by calling function `GetAiAgentDefaultConfig`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-ai-agent-default/).

<!-- sample get_ai_agent_default -->
```
await client.Ai.GetAiAgentDefaultConfigAsync(queryParams: new GetAiAgentDefaultConfigQueryParams(mode: GetAiAgentDefaultConfigQueryParamsModeField.Ask) { Language = "en-US" });
```

### Arguments

- queryParams `GetAiAgentDefaultConfigQueryParams`
  - Query parameters of getAiAgentDefaultConfig method
- headers `GetAiAgentDefaultConfigHeaders`
  - Headers of getAiAgentDefaultConfig method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen`.

A successful response including the default agent configuration.
This response can be one of the following four objects:
* AI agent for questions
* AI agent for text generation
* AI agent for freeform metadata extraction
* AI agent for structured metadata extraction.
The response depends on the agent configuration requested in this endpoint.


## Extract metadata (freeform)

Sends an AI request to supported Large Language Models (LLMs) and extracts metadata in form of key-value pairs.
In this request, both the prompt and the output can be freeform.
Metadata template setup before sending the request is not required.

This operation is performed by calling function `CreateAiExtract`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-ai-extract/).

<!-- sample post_ai_extract -->
```
await client.Ai.CreateAiExtractAsync(requestBody: new AiExtract(prompt: "firstName, lastName, location, yearOfBirth, company", items: Array.AsReadOnly(new [] {new AiItemBase(id: file.Id)})));
```

### Arguments

- requestBody `AiExtract`
  - Request body of createAiExtract method
- headers `CreateAiExtractHeaders`
  - Headers of createAiExtract method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AiResponse`.

A response including the answer from the LLM.


## Extract metadata (structured)

Sends an AI request to supported Large Language Models (LLMs) and returns extracted metadata as a set of key-value pairs.
For this request, you either need a metadata template or a list of fields you want to extract.
Input is **either** a metadata template or a list of fields to ensure the structure.
To learn more about creating templates, see [Creating metadata templates in the Admin Console](https://support.box.com/hc/en-us/articles/360044194033-Customizing-Metadata-Templates)
or use the [metadata template API](g://metadata/templates/create).

This operation is performed by calling function `CreateAiExtractStructured`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-ai-extract-structured/).

<!-- sample post_ai_extract_structured -->
```
await client.Ai.CreateAiExtractStructuredAsync(requestBody: new AiExtractStructured(items: Array.AsReadOnly(new [] {new AiItemBase(id: file.Id)})) { Fields = Array.AsReadOnly(new [] {new AiExtractStructuredFieldsField(key: "firstName") { DisplayName = "First name", Description = "Person first name", Prompt = "What is the your first name?", Type = "string" },new AiExtractStructuredFieldsField(key: "lastName") { DisplayName = "Last name", Description = "Person last name", Prompt = "What is the your last name?", Type = "string" },new AiExtractStructuredFieldsField(key: "dateOfBirth") { DisplayName = "Birth date", Description = "Person date of birth", Prompt = "What is the date of your birth?", Type = "date" },new AiExtractStructuredFieldsField(key: "age") { DisplayName = "Age", Description = "Person age", Prompt = "How old are you?", Type = "float" },new AiExtractStructuredFieldsField(key: "hobby") { DisplayName = "Hobby", Description = "Person hobby", Prompt = "What is your hobby?", Type = "multiSelect", Options = Array.AsReadOnly(new [] {new AiExtractStructuredFieldsOptionsField(key: "guitar"),new AiExtractStructuredFieldsOptionsField(key: "books")}) }}) });
```

### Arguments

- requestBody `AiExtractStructured`
  - Request body of createAiExtractStructured method
- headers `CreateAiExtractStructuredHeaders`
  - Headers of createAiExtractStructured method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AiExtractStructuredResponse`.

A successful response including the answer from the LLM.


