AI
==

AI allows to send an intelligence request to supported large language models and returns
an answer based on the provided prompt and items.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Send AI question](#send-ai-request)
- [Send AI text generation request](#send-ai-text-generation-request)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Send AI question
--------------------------

To send an AI request, use `BoxAIManager.SendAIQuestionAsync(BoxAIAskRequest aiAskRequest)` method
In the request you have to provide a prompt, a list of items that your prompt refers to and a mode of the request.
There are two modes available: `single_item_qa` and `multiple_item_qa`, which specifies if this request refers to
for a single or multiple items.

<!-- sample post_ai_ask -->
```c#
BoxAIResponse response = await client.BoxAIManager.SendAIQuestionAsync(
    new BoxAIAskRequest
    {
        Prompt = "What is the name of the file?",
        Items = new List<BoxAIAskItem>() { new BoxAIAskItem() { Id = "12345" } },
        Mode = AiAskMode.single_item_qa
    };
);
```

NOTE: The AI endpoint may return a 412 status code if you use for your request a file which has just been updated to the box.
It usually takes a few seconds for the file to be indexed and available for the AI endpoint.

Send AI text generation request
--------------

To send an AI request specifically focused on the creation of new text, call
`BoxAIManager.SendAITextGenRequestAsync(BoxAiTextGenRequest aiTextGenRequest)` method.
In the request you have to provide a prompt, a list of items that your prompt refers to and optionally a dialogue history,
which provides additional context to the LLM in generating the response.

<!-- sample post_ai_text_gen -->
```c#
BoxAIResponse response = await client.BoxAIManager.SendAITextGenRequestAsync(
    new BoxAITextGenRequest
    {
        Prompt = "What is the name of the file?",
        Items = new List<BoxAITextGenItem>() { new BoxAITextGenItem() { Id = "12345" } },
        DialogueHistory = new List<BoxAIDialogueHistory>()
        {
            new BoxAIDialogueHistory() { Prompt = "What is the name of the file?", Answer = "MyFile", CreatedAt = DateTimeOffset.Parse("2024-05-16T15:26:57-07:00") }
            new BoxAIDialogueHistory() { Prompt = "What is the size of the file?", Answer = "10kb", CreatedAt =  DateTimeOffset.Parse("2024-05-16T15:26:57-07:00") }
        }
    };
);
```