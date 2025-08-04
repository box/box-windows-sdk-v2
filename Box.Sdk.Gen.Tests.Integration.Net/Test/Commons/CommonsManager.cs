using Box.Sdk.Gen.Internal;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen;

namespace Box.Sdk.Gen {
    public class CommonsManager : ICommonsManager {
        public BoxCcgAuth GetCcgAuth() {
            CcgConfig ccgConfig = new CcgConfig(clientId: Utils.GetEnvVar(name: "CLIENT_ID"), clientSecret: Utils.GetEnvVar(name: "CLIENT_SECRET")) { EnterpriseId = Utils.GetEnvVar(name: "ENTERPRISE_ID") };
            BoxCcgAuth auth = new BoxCcgAuth(config: ccgConfig);
            return auth;
        }

        public BoxJwtAuth GetJwtAuth() {
            JwtConfig jwtConfig = JwtConfig.FromConfigJsonString(configJsonString: Utils.DecodeBase64(value: Utils.GetEnvVar(name: "JWT_CONFIG_BASE_64")));
            BoxJwtAuth auth = new BoxJwtAuth(config: jwtConfig);
            return auth;
        }

        public BoxClient GetDefaultClientWithUserSubject(string userId) {
            if (Utils.IsBrowser()) {
                BoxCcgAuth ccgAuth = GetCcgAuth();
                BoxCcgAuth ccgAuthUser = ccgAuth.WithUserSubject(userId: userId);
                return new BoxClient(auth: ccgAuthUser);
            }
            BoxJwtAuth auth = GetJwtAuth();
            BoxJwtAuth authUser = auth.WithUserSubject(userId: userId);
            return new BoxClient(auth: authUser);
        }

        public BoxClient GetDefaultClient() {
            BoxClient client = new BoxClient(auth: Utils.IsBrowser() ? GetCcgAuth() : GetJwtAuth());
            return client;
        }

        public async System.Threading.Tasks.Task<FolderFull> CreateNewFolderAsync() {
            BoxClient client = new CommonsManager().GetDefaultClient();
            string newFolderName = Utils.GetUUID();
            return await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: newFolderName, parent: new CreateFolderRequestBodyParentField(id: "0")));
        }

        public async System.Threading.Tasks.Task<FileFull> UploadNewFileAsync() {
            BoxClient client = new CommonsManager().GetDefaultClient();
            string newFileName = string.Concat(Utils.GetUUID(), ".pdf");
            System.IO.Stream fileContentStream = Utils.GenerateByteStream(size: 1024 * 1024);
            Files uploadedFiles = await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: newFileName, parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: fileContentStream));
            return NullableUtils.Unwrap(uploadedFiles.Entries)[0];
        }

        public async System.Threading.Tasks.Task<TermsOfService> GetOrCreateTermsOfServicesAsync() {
            BoxClient client = new CommonsManager().GetDefaultClient();
            TermsOfServices tos = await client.TermsOfServices.GetTermsOfServiceAsync();
            int numberOfTos = NullableUtils.Unwrap(tos.Entries).Count;
            if (numberOfTos >= 1) {
                TermsOfService firstTos = NullableUtils.Unwrap(tos.Entries).ElementAt(0);
                if (StringUtils.ToStringRepresentation(firstTos.TosType) == "managed") {
                    return firstTos;
                }
            }
            if (numberOfTos >= 2) {
                TermsOfService secondTos = NullableUtils.Unwrap(tos.Entries).ElementAt(1);
                if (StringUtils.ToStringRepresentation(secondTos.TosType) == "managed") {
                    return secondTos;
                }
            }
            return await client.TermsOfServices.CreateTermsOfServiceAsync(requestBody: new CreateTermsOfServiceRequestBody(status: CreateTermsOfServiceRequestBodyStatusField.Disabled, text: "Test TOS") { TosType = CreateTermsOfServiceRequestBodyTosTypeField.Managed });
        }

        public async System.Threading.Tasks.Task<ClassificationTemplateFieldsOptionsField> GetOrCreateClassificationAsync(ClassificationTemplate classificationTemplate) {
            BoxClient client = new CommonsManager().GetDefaultClient();
            IReadOnlyList<ClassificationTemplateFieldsOptionsField> classifications = classificationTemplate.Fields[0].Options;
            int currentNumberOfClassifications = classifications.Count;
            if (currentNumberOfClassifications == 0) {
                ClassificationTemplate classificationTemplateWithNewClassification = await client.Classifications.AddClassificationAsync(requestBody: Array.AsReadOnly(new [] {new AddClassificationRequestBody(data: new AddClassificationRequestBodyDataField(key: Utils.GetUUID()) { StaticConfig = new AddClassificationRequestBodyDataStaticConfigField() { Classification = new AddClassificationRequestBodyDataStaticConfigClassificationField() { ColorId = 3, ClassificationDefinition = "Some description" } } })}));
                return classificationTemplateWithNewClassification.Fields[0].Options[0];
            }
            return classifications.ElementAt(0);
        }

        public async System.Threading.Tasks.Task<ClassificationTemplate> GetOrCreateClassificationTemplateAsync() {
            BoxClient client = new CommonsManager().GetDefaultClient();
            try {
                return await client.Classifications.GetClassificationTemplateAsync();
            } catch {
                return await client.Classifications.CreateClassificationTemplateAsync(requestBody: new CreateClassificationTemplateRequestBody(fields: Array.AsReadOnly(new [] {new CreateClassificationTemplateRequestBodyFieldsField(options: Enumerable.Empty<CreateClassificationTemplateRequestBodyFieldsOptionsField>().ToList())}))).ConfigureAwait(false);
            }
        }

        public async System.Threading.Tasks.Task<ShieldInformationBarrier> GetOrCreateShieldInformationBarrierAsync(BoxClient client, string enterpriseId) {
            ShieldInformationBarriers barriers = await client.ShieldInformationBarriers.GetShieldInformationBarriersAsync();
            int numberOfBarriers = NullableUtils.Unwrap(barriers.Entries).Count;
            if (numberOfBarriers == 0) {
                return await client.ShieldInformationBarriers.CreateShieldInformationBarrierAsync(requestBody: new CreateShieldInformationBarrierRequestBody(enterprise: new EnterpriseBase() { Id = enterpriseId }));
            }
            return NullableUtils.Unwrap(barriers.Entries).ElementAt(numberOfBarriers - 1);
        }

    }
}