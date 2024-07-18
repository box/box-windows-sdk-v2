using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxAIManagerTest : BoxResourceManagerTest
    {
        private readonly BoxAIManager _aiManager;

        public BoxAIManagerTest()
        {
            _aiManager = new BoxAIManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task SendAiQuestionAsync_Success()
        {
            /** Arrange **/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxAIResponse>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxAIResponse>>(new BoxResponse<BoxAIResponse>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxAI/SendAiQuestion200.json")
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            var requestBody = new BoxAIAskRequest()
            {
                Mode = AiAskMode.single_item_qa,
                Prompt = "What is the value provided by public APIs based on this document?",
                Items = new System.Collections.Generic.List<BoxAIAskItem>()
                {
                    new BoxAIAskItem() { Id = "9842787262" }
                }
            };

            /*** Act ***/
            BoxAIResponse response = await _aiManager.SendAIQuestionAsync(requestBody);

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/ai/ask"), boxRequest.AbsoluteUri);

            // Response check
            Assert.AreEqual("Public APIs are important because of key and important reasons.", response.Answer);
            Assert.AreEqual("done", response.CompletionReason);
            Assert.AreEqual(DateTimeOffset.Parse("2012-12-12T10:53:43-08:00"), response.CreatedAt);
        }


        [TestMethod]
        public async Task SendAiGenerateTextRequestAsync_Success()
        {
            /** Arrange **/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxAIResponse>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxAIResponse>>(new BoxResponse<BoxAIResponse>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxAI/SendAITextGenRequestSuccess200.json")
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            var requestBody = new BoxAITextGenRequest()
            {
                Prompt = "Write an email to a client about the importance of public APIs",
                Items = new List<BoxAITextGenItem>()
                {
                    new BoxAITextGenItem() { Id = "12345678", Content = "More information about public APIs" }
                },
                DialogueHistory = new List<BoxAIDialogueHistory>()
                {
                    new BoxAIDialogueHistory()
                    {
                        Prompt = "Make my email about public APIs sound more professional",
                        Answer = "Here is the first draft of your professional email about public APIs",
                        CreatedAt = DateTimeOffset.Parse("2013-12-12T10:53:43-08:00")
                    },
                    new BoxAIDialogueHistory()
                    {
                        Prompt = "Can you add some more information?",
                        Answer = "Public API schemas provide necessary information to integrate with APIs...",
                        CreatedAt = DateTimeOffset.Parse("2013-12-12T11:20:43-08:00")
                    }
                }
            };

            /*** Act ***/
            BoxAIResponse response = await _aiManager.SendAITextGenRequestAsync(requestBody);

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/ai/text_gen"), boxRequest.AbsoluteUri);

            // Response check
            Assert.AreEqual("Public APIs are important because of key and important reasons.", response.Answer);
            Assert.AreEqual("done", response.CompletionReason);
            Assert.AreEqual(DateTimeOffset.Parse("2012-12-12T10:53:43-08:00"), response.CreatedAt);
        }
    }
}
