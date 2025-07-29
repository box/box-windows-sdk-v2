using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class ShieldListsManagerTests {
        public string userId { get; }

        public BoxClient client { get; }

        public ShieldListsManagerTests() {
            userId = Utils.GetEnvVar(name: "USER_ID");
            client = new CommonsManager().GetDefaultClientWithUserSubject(userId: userId);
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateGetUpdateDeleteShieldList() {
            string shieldListCountryName = string.Concat(Utils.GetUUID(), "shieldListCountry");
            ShieldListV2025R0 shieldListCountry = await client.ShieldLists.CreateShieldListV2025R0Async(requestBody: new ShieldListsCreateV2025R0(name: shieldListCountryName, content: new ShieldListContentCountryV2025R0(type: ShieldListContentCountryV2025R0TypeField.Country, countryCodes: Array.AsReadOnly(new [] {"US","PL"}))) { Description = "A list of things that are shielded" });
            string shieldListContentDomainName = string.Concat(Utils.GetUUID(), "shieldListContentDomain");
            ShieldListV2025R0 shieldListContentDomain = await client.ShieldLists.CreateShieldListV2025R0Async(requestBody: new ShieldListsCreateV2025R0(name: shieldListContentDomainName, content: new ShieldListContentDomainV2025R0(type: ShieldListContentDomainV2025R0TypeField.Domain, domains: Array.AsReadOnly(new [] {"box.com","example.com"}))) { Description = "A list of things that are shielded" });
            string shieldListContentEmailName = string.Concat(Utils.GetUUID(), "shieldListContentEmail");
            ShieldListV2025R0 shieldListContentEmail = await client.ShieldLists.CreateShieldListV2025R0Async(requestBody: new ShieldListsCreateV2025R0(name: shieldListContentEmailName, content: new ShieldListContentEmailV2025R0(type: ShieldListContentEmailV2025R0TypeField.Email, emailAddresses: Array.AsReadOnly(new [] {"test@box.com","test@example.com"}))) { Description = "A list of things that are shielded" });
            string shieldListContentIpName = string.Concat(Utils.GetUUID(), "shieldListContentIp");
            ShieldListV2025R0 shieldListContentIp = await client.ShieldLists.CreateShieldListV2025R0Async(requestBody: new ShieldListsCreateV2025R0(name: shieldListContentIpName, content: new ShieldListContentIpV2025R0(type: ShieldListContentIpV2025R0TypeField.Ip, ipAddresses: Array.AsReadOnly(new [] {"127.0.0.1","80.12.12.12/24"}))) { Description = "A list of things that are shielded" });
            ShieldListsV2025R0 shieldLists = await client.ShieldLists.GetShieldListsV2025R0Async();
            Assert.IsTrue(NullableUtils.Unwrap(shieldLists.Entries).Count > 0);
            ShieldListV2025R0 getShieldListCountry = await client.ShieldLists.GetShieldListByIdV2025R0Async(shieldListId: shieldListCountry.Id);
            Assert.IsTrue(getShieldListCountry.Name == shieldListCountryName);
            Assert.IsTrue(getShieldListCountry.Description == "A list of things that are shielded");
            await client.ShieldLists.UpdateShieldListByIdV2025R0Async(shieldListId: shieldListCountry.Id, requestBody: new ShieldListsUpdateV2025R0(name: shieldListCountryName, content: new ShieldListContentCountryV2025R0(type: ShieldListContentCountryV2025R0TypeField.Country, countryCodes: Array.AsReadOnly(new [] {"US"}))) { Description = "Updated description" });
            ShieldListV2025R0 getShieldListCountryUpdated = await client.ShieldLists.GetShieldListByIdV2025R0Async(shieldListId: shieldListCountry.Id);
            Assert.IsTrue(getShieldListCountryUpdated.Description == "Updated description");
            await client.ShieldLists.DeleteShieldListByIdV2025R0Async(shieldListId: shieldListCountry.Id);
            await client.ShieldLists.DeleteShieldListByIdV2025R0Async(shieldListId: shieldListContentDomain.Id);
            await client.ShieldLists.DeleteShieldListByIdV2025R0Async(shieldListId: shieldListContentEmail.Id);
            await client.ShieldLists.DeleteShieldListByIdV2025R0Async(shieldListId: shieldListContentIp.Id);
        }

    }
}