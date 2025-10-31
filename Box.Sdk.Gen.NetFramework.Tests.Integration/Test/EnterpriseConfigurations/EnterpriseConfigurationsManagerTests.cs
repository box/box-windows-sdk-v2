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
    public class EnterpriseConfigurationsManagerTests {
        public BoxClient adminClient { get; }

        public EnterpriseConfigurationsManagerTests() {
            adminClient = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetEnterpriseConfigurationById() {
            string enterpriseId = Utils.GetEnvVar(name: "ENTERPRISE_ID");
            EnterpriseConfigurationV2025R0 enterpriseConfiguration = await adminClient.EnterpriseConfigurations.GetEnterpriseConfigurationByIdV2025R0Async(enterpriseId: enterpriseId, queryParams: new GetEnterpriseConfigurationByIdV2025R0QueryParams(categories: Array.AsReadOnly(new [] {"user_settings","content_and_sharing","security","shield"})));
            Assert.IsTrue(StringUtils.ToStringRepresentation(enterpriseConfiguration.Type?.Value) == "enterprise_configuration");
            EnterpriseConfigurationUserSettingsV2025R0 userSettings = NullableUtils.Unwrap(NullableUtils.Unwrap(enterpriseConfiguration.UserSettings));
            Assert.IsTrue(NullableUtils.Unwrap(userSettings.IsEnterpriseSsoRequired).Value == false);
            Assert.IsTrue(NullableUtils.Unwrap(userSettings.NewUserDefaultLanguage).Value == "English (US)");
            Assert.IsTrue(NullableUtils.Unwrap(userSettings.NewUserDefaultStorageLimit).Value == -1);
            EnterpriseConfigurationContentAndSharingV2025R0 contentAndSharing = NullableUtils.Unwrap(NullableUtils.Unwrap(enterpriseConfiguration.ContentAndSharing));
            Assert.IsTrue(NullableUtils.Unwrap(NullableUtils.Unwrap(contentAndSharing.CollaborationPermissions).Value).IsEditorRoleEnabled == true);
            EnterpriseConfigurationSecurityV2025R0 security = NullableUtils.Unwrap(NullableUtils.Unwrap(enterpriseConfiguration.Security));
            Assert.IsTrue(NullableUtils.Unwrap(NullableUtils.Unwrap(security.IsManagedUserSignupEnabled).Value) == false);
            EnterpriseConfigurationShieldV2025R0 shield = NullableUtils.Unwrap(NullableUtils.Unwrap(enterpriseConfiguration.Shield));
            Assert.IsTrue(NullableUtils.Unwrap(shield.ShieldRules).Count == 0);
        }

    }
}