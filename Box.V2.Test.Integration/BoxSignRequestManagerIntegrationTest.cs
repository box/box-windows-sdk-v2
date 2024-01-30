using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Test.Integration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxSignRequestManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        public async Task CreateSignRequestAsync_ForCorrectSignRequestCreateRequest_ShouldCreateNewSignRequest()
        {
            var fileToSign = await CreateSmallFile(FolderId);
            var fileToSign2 = await CreateSmallFile(FolderId);
            var signRequestCreateRequest = new BoxSignRequestCreateRequest()
            {
                SourceFiles = new List<BoxSignRequestCreateSourceFile>()
                {
                    new BoxSignRequestCreateSourceFile()
                    {
                        Id = fileToSign.Id
                    },
                    new BoxSignRequestCreateSourceFile()
                    {
                        Id = fileToSign2.Id
                    }
                },
                Signers = new List<BoxSignRequestSignerCreate>()
                {
                    new BoxSignRequestSignerCreate()
                    {
                        Email = "sdk_integration_test@boxdemo.com",
                        RedirectUrl = new Uri("https://www.box.com/redirect_url_signer_1"),
                        DeclinedRedirectUrl = new Uri("https://www.box.com/declined_redirect_url_singer_1"),
                        EmbedUrlExternalUserId = UserId,
                        SignerGroupId = "SignerGroup",
                        Password = "password",
                        LoginRequired = false,
                    },
                    new BoxSignRequestSignerCreate()
                    {
                        Email = "sdk_integration_test_2@boxdemo.com",
                        RedirectUrl = new Uri("https://www.box.com/redirect_url_signer_2"),
                        DeclinedRedirectUrl = new Uri("https://www.box.com/declined_redirect_url_singer_2"),
                        SignerGroupId = "SignerGroup",
                        Password = "password",
                        LoginRequired = false,
                    }
                },
                ParentFolder = new BoxRequestEntity()
                {
                    Id = FolderId,
                    Type = BoxType.folder
                },
                RedirectUrl = new Uri("https://www.box.com/redirect_url"),
                DeclinedRedirectUrl = new Uri("https://www.box.com/declined_redirect_url")
            };
            BoxSignRequest signRequest = await UserClient.SignRequestsManager.CreateSignRequestAsync(signRequestCreateRequest);
            Assert.IsNotNull(signRequest.Id);
            Assert.AreEqual(signRequestCreateRequest.SourceFiles[0].Id, signRequest.SourceFiles[0].Id);
            Assert.AreEqual(signRequestCreateRequest.SourceFiles[1].Id, signRequest.SourceFiles[1].Id);
            Assert.AreEqual(signRequestCreateRequest.RedirectUrl.ToString(), signRequest.RedirectUrl.ToString());
            Assert.AreEqual(signRequestCreateRequest.DeclinedRedirectUrl.ToString(), signRequest.DeclinedRedirectUrl.ToString());
            Assert.AreEqual(signRequestCreateRequest.ParentFolder.Id, signRequest.ParentFolder.Id);

            // first signer is the sender with role final_copy_reader, second and third is the recipient with role signer
            Assert.AreEqual(3, signRequest.Signers.Count);
            Assert.IsNotNull(signRequest.Signers[1].IframeableEmbedUrl);

            var signerGroupId = "";
            foreach (var signer in signRequest.Signers)
            {
                if (signer.Role == BoxSignRequestSignerRole.signer)
                {
                    if (string.IsNullOrEmpty(signerGroupId))
                    {
                        signerGroupId = signer.SignerGroupId;
                    }
                    Assert.AreEqual(signerGroupId, signer.SignerGroupId);
                }
            }

            await UserClient.SignRequestsManager.CancelSignRequestAsync(signRequest.Id);

            signRequest = await UserClient.SignRequestsManager.GetSignRequestByIdAsync(signRequest.Id);
            Assert.AreEqual(signRequest.Status, BoxSignRequestStatus.cancelled);
        }

        [TestMethod]
        public async Task GetSignRequestAsync_ForExistingSignRequest_ShouldReturnSignRequest()
        {
            var signRequest = await CreateSignRequest("sdk_integration_test@boxdemo.com", FolderId);
            var fetchedSignRequest = await UserClient.SignRequestsManager.GetSignRequestByIdAsync(signRequest.Id);

            Assert.AreEqual(signRequest.Id, fetchedSignRequest.Id);
        }
    }
}
