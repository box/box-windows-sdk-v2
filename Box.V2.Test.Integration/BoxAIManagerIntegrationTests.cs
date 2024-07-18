using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Test.Integration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxAIManagerIntegrationTests : TestInFolder
    {
        [TestMethod]
        public async Task SendAIQuestionAsync_ForSingleItem_ReturnsValidResponse()
        {
            var fileName = "[Single Item AI] Test File.txt";
            var fileContent = "Test file";
            var uploadedFile = await CreateSmallFromMemoryStream(FolderId, fileName, fileContent);

            var request = new BoxAIAskRequest
            {
                Prompt = "What is the name of the file?",
                Items = new List<BoxAIAskItem>() { new BoxAIAskItem() { Id = uploadedFile.Id } },
                Mode = AiAskMode.single_item_qa
            };

            await Retry(async () =>
            {
                var response = await UserClient.BoxAIManager.SendAIQuestionAsync(request);

                Assert.IsTrue(response.Answer.Contains(fileContent));
                Assert.IsTrue(response.CreatedAt < DateTimeOffset.Now);
                Assert.AreEqual(response.CompletionReason, "done");
            });
        }

        [TestMethod]
        public async Task SendAIQuestionAsync_ForMultipleItems_ReturnsValidResponse()
        {
            var fileContent = "Test file";

            var fileName1 = "[Multi Item AI] First Test File.txt";
            var uploadedFile1 = await CreateSmallFromMemoryStream(FolderId, fileName1, fileContent);

            var fileName2 = "[Multi Item AI] Second test file.txt";
            var uploadedFile2 = await CreateSmallFromMemoryStream(FolderId, fileName2, fileContent);

            var request = new BoxAIAskRequest
            {
                Prompt = "What is the content of these files?",
                Items = new List<BoxAIAskItem>()
                {
                    new BoxAIAskItem() { Id = uploadedFile1.Id },
                    new BoxAIAskItem() { Id = uploadedFile2.Id }
                },
                Mode = AiAskMode.multiple_item_qa
            };

            await Retry(async () =>
            {
                var response = await UserClient.BoxAIManager.SendAIQuestionAsync(request);

                Assert.IsTrue(response.Answer.Contains(fileContent));
                Assert.IsTrue(response.CreatedAt < DateTimeOffset.Now);
                Assert.AreEqual(response.CompletionReason, "done");
            });
        }

        [TestMethod]
        public async Task SendTextGenRequestAsync_ForValidPayload_ReturnsValidResponse()
        {
            var fileName = "[AI Text Gen] Test File.txt";
            var fileContent = "Test File";
            var uploadedFile = await CreateSmallFromMemoryStream(FolderId, fileName, fileContent);
            var date1 = DateTimeOffset.Parse("2013-05-16T15:27:57-07:00");
            var date2 = DateTimeOffset.Parse("2013-05-16T15:26:57-07:00");

            var request = new BoxAITextGenRequest
            {
                Prompt = "What is the name of the file?",
                Items = new List<BoxAITextGenItem>() { new BoxAITextGenItem() { Id = uploadedFile.Id } },
                DialogueHistory = new List<BoxAIDialogueHistory>()
                {
                    new BoxAIDialogueHistory() { Prompt = "What is the name of the file?", Answer = fileContent, CreatedAt = date1 },
                    new BoxAIDialogueHistory() { Prompt = "What is the size of the file?", Answer = "10kb", CreatedAt = date2 }
                }
            };

            await Retry(async () =>
            {
                var response = await UserClient.BoxAIManager.SendAITextGenRequestAsync(request);

                Assert.IsTrue(response.Answer.Contains(fileContent));
                Assert.IsTrue(response.CreatedAt < DateTimeOffset.Now);
                Assert.AreEqual(response.CompletionReason, "done");
            });
        }
    }
}
