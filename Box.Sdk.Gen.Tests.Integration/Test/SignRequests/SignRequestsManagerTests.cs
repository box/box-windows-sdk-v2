using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class SignRequestsManagerTests {
        public BoxClient client { get; }

        public SignRequestsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateGetCancelAndListSignRequest() {
            string signerEmail = string.Concat(Utils.GetUUID(), "@box.com");
            FileFull fileToSign = await new CommonsManager().UploadNewFileAsync();
            FolderFull destinationFolder = await new CommonsManager().CreateNewFolderAsync();
            SignRequest createdSignRequest = await client.SignRequests.CreateSignRequestAsync(requestBody: new SignRequestCreateRequest(signers: Array.AsReadOnly(new [] {new SignRequestCreateSigner() { Email = signerEmail, SuppressNotifications = true, DeclinedRedirectUrl = "https://www.box.com", EmbedUrlExternalUserId = "123", IsInPerson = false, LoginRequired = false, Password = "password", Role = SignRequestCreateSignerRoleField.Signer }})) { AreRemindersEnabled = true, AreTextSignaturesEnabled = true, DaysValid = 30, DeclinedRedirectUrl = "https://www.box.com", EmailMessage = "Please sign this document", EmailSubject = "Sign this document", ExternalId = "123", ExternalSystemName = "BoxSignIntegration", IsDocumentPreparationNeeded = false, Name = "Sign Request", ParentFolder = new FolderMini(id: destinationFolder.Id), RedirectUrl = "https://www.box.com", PrefillTags = Array.AsReadOnly(new [] {new SignRequestPrefillTag() { DateValue = Utils.DateFromString(date: "2035-01-01"), DocumentTagId = "0" }}), SourceFiles = Array.AsReadOnly(new [] {new FileBase(id: fileToSign.Id)}) });
            Assert.IsTrue(createdSignRequest.AreRemindersEnabled == true);
            Assert.IsTrue(createdSignRequest.AreTextSignaturesEnabled == true);
            Assert.IsTrue(createdSignRequest.DaysValid == 30);
            Assert.IsTrue(createdSignRequest.DeclinedRedirectUrl == "https://www.box.com");
            Assert.IsTrue(createdSignRequest.EmailMessage == "Please sign this document");
            Assert.IsTrue(createdSignRequest.EmailSubject == "Sign this document");
            Assert.IsTrue(createdSignRequest.ExternalId == "123");
            Assert.IsTrue(createdSignRequest.ExternalSystemName == "BoxSignIntegration");
            Assert.IsTrue(createdSignRequest.IsDocumentPreparationNeeded == false);
            Assert.IsTrue(createdSignRequest.Name == "Sign Request.pdf");
            Assert.IsTrue(createdSignRequest.RedirectUrl == "https://www.box.com");
            Assert.IsTrue(NullableUtils.Unwrap(NullableUtils.Unwrap(createdSignRequest.SignFiles).Files)[0].Name == createdSignRequest.Name);
            Assert.IsTrue(NullableUtils.Unwrap(createdSignRequest.Signers)[1].Email == signerEmail);
            Assert.IsTrue(NullableUtils.Unwrap(createdSignRequest.Signers)[1].SuppressNotifications == true);
            Assert.IsTrue(NullableUtils.Unwrap(createdSignRequest.Signers)[1].DeclinedRedirectUrl == "https://www.box.com");
            Assert.IsTrue(NullableUtils.Unwrap(createdSignRequest.Signers)[1].EmbedUrlExternalUserId == "123");
            Assert.IsTrue(NullableUtils.Unwrap(createdSignRequest.Signers)[1].IsInPerson == false);
            Assert.IsTrue(NullableUtils.Unwrap(createdSignRequest.Signers)[1].LoginRequired == false);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(createdSignRequest.Signers)[1].Role) == "signer");
            Assert.IsTrue(NullableUtils.Unwrap(createdSignRequest.ParentFolder).Id == destinationFolder.Id);
            Assert.IsTrue(Utils.DateToString(date: NullableUtils.Unwrap(NullableUtils.Unwrap(createdSignRequest.PrefillTags)[0].DateValue)) == "2035-01-01");
            SignRequest newSignRequest = await client.SignRequests.GetSignRequestByIdAsync(signRequestId: NullableUtils.Unwrap(createdSignRequest.Id));
            Assert.IsTrue(NullableUtils.Unwrap(NullableUtils.Unwrap(newSignRequest.SignFiles).Files)[0].Name == createdSignRequest.Name);
            Assert.IsTrue(NullableUtils.Unwrap(newSignRequest.Signers)[1].Email == signerEmail);
            Assert.IsTrue(NullableUtils.Unwrap(newSignRequest.ParentFolder).Id == destinationFolder.Id);
            SignRequest cancelledSignRequest = await client.SignRequests.CancelSignRequestAsync(signRequestId: NullableUtils.Unwrap(createdSignRequest.Id));
            Assert.IsTrue(StringUtils.ToStringRepresentation(cancelledSignRequest.Status?.Value) == "cancelled");
            SignRequests signRequests = await client.SignRequests.GetSignRequestsAsync();
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(signRequests.Entries)[0].Type)) == "sign-request");
            await client.Folders.DeleteFolderByIdAsync(folderId: destinationFolder.Id, queryParams: new DeleteFolderByIdQueryParams() { Recursive = true });
            await client.Files.DeleteFileByIdAsync(fileId: fileToSign.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateSignRequestWithSignerGroupId() {
            string signer1Email = string.Concat(Utils.GetUUID(), "@box.com");
            string signer2Email = string.Concat(Utils.GetUUID(), "@box.com");
            FileFull fileToSign = await new CommonsManager().UploadNewFileAsync();
            FolderFull destinationFolder = await new CommonsManager().CreateNewFolderAsync();
            SignRequest createdSignRequest = await client.SignRequests.CreateSignRequestAsync(requestBody: new SignRequestCreateRequest(signers: Array.AsReadOnly(new [] {new SignRequestCreateSigner() { Email = signer1Email, SignerGroupId = "user" },new SignRequestCreateSigner() { Email = signer2Email, SignerGroupId = "user" }})) { ParentFolder = new FolderMini(id: destinationFolder.Id), SourceFiles = Array.AsReadOnly(new [] {new FileBase(id: fileToSign.Id)}) });
            Assert.IsTrue(NullableUtils.Unwrap(createdSignRequest.Signers).Count == 3);
            Assert.IsTrue(NullableUtils.Unwrap(createdSignRequest.Signers)[1].SignerGroupId != null);
            Assert.IsTrue(NullableUtils.Unwrap(createdSignRequest.Signers)[1].SignerGroupId == NullableUtils.Unwrap(createdSignRequest.Signers)[2].SignerGroupId);
            await client.Folders.DeleteFolderByIdAsync(folderId: destinationFolder.Id, queryParams: new DeleteFolderByIdQueryParams() { Recursive = true });
            await client.Files.DeleteFileByIdAsync(fileId: fileToSign.Id);
        }

    }
}