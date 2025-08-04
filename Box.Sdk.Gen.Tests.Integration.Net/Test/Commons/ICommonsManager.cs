using Box.Sdk.Gen.Internal;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen;

namespace Box.Sdk.Gen {
public interface ICommonsManager {
    BoxCcgAuth GetCcgAuth();

    BoxJwtAuth GetJwtAuth();

    BoxClient GetDefaultClientWithUserSubject(string userId);

    BoxClient GetDefaultClient();

    System.Threading.Tasks.Task<FolderFull> CreateNewFolderAsync();

    System.Threading.Tasks.Task<FileFull> UploadNewFileAsync();

    System.Threading.Tasks.Task<TermsOfService> GetOrCreateTermsOfServicesAsync();

    System.Threading.Tasks.Task<ClassificationTemplateFieldsOptionsField> GetOrCreateClassificationAsync(ClassificationTemplate classificationTemplate);

    System.Threading.Tasks.Task<ClassificationTemplate> GetOrCreateClassificationTemplateAsync();

    System.Threading.Tasks.Task<ShieldInformationBarrier> GetOrCreateShieldInformationBarrierAsync(BoxClient client, string enterpriseId);

}
}