using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;
using Box.V2.Auth;
using System.Diagnostics;
using Box.V2.Config;
using Box.V2.JWTAuth;
using System.Collections.Generic;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxAuthTestIntegration : BoxResourceManagerTestIntegration
    {
        public const string ClientId = "YOUR_CLIENT_ID";
        public const string ClientSecret = "YOUR_CLIENT_SECRET";
        public const string EnterpriseId = "YOUR_ENTERPRISE_ID";
        public const string publicKeyID = "YOUR_PUBLIC_KEY_ID";
        public const string privateKey = "-----BEGIN ENCRYPTED PRIVATE KEY-----\nYOUR_PRIVATE_KEY\n-----END ENCRYPTED PRIVATE KEY-----\n";
        public const string passphrase = "YOUR_PASSPHRASE";

        [TestMethod]
        public void retriesWithNewJWTAssertionOnErrorResponseAndSucceeds()
        {
            var config = new BoxConfig(ClientId, ClientSecret, EnterpriseId, privateKey, passphrase, publicKeyID);
            var session = new BoxJWTAuth(config);
            var adminToken = session.AdminToken();
            adminClient = session.AdminClient(adminToken);
        }

        [TestMethod]
        public async Task metadataTestAsync()
        {
            /*** Act ***/
            var queryParams = new Dictionary<string, object>();
            queryParams.Add("arg", 5);
            List <BoxMetadataQueryOrderBy> orderByList = new List<BoxMetadataQueryOrderBy>();
            var orderBy = new BoxMetadataQueryOrderBy()
            {
                FieldKey = "numberfield",
                Direction = BoxSortDirection.DESC
            };
            orderByList.Add(orderBy);
            string marker = "0!w7PKdy8e2Bz_P0A1MCWpAzvvmL3Zuz9xnjyEVuTMB90C6kYwNuIN7S8GPI5yAgPyh31cHRkrNZuKew7eVzdGxKojsFZzETPBYXBJd1D-pWJf2nftYOMpvjyACe9C3IEMBGoq81CdREqjTunVg99XMSl4OaHDx7iEDrnEPmEvTa07NzSG8utSDmcNUP4uQMSIckVBiQ3y_neH3i5KEBK-hu2viHlojSUutHrwoHCaO9s-eLHZU4VEb7ooZk2Vc-0dm5KJj6y1jRIlG87VC7TzBDWbo5uh5D_nsvecJJA0Yj54JM3LLkv9TIWz2Uq8ApSMuozXA-ngxuNKHXy9UeUGQhmakipJX4jcEFvm9FufJplYQuA8zUNkujdMfE1BGgmXXmKVQ4tdkV4jw0KO59IwwIVsTEfyqICu6x09OXcNOA..";
            BoxCollectionMarkerBased<BoxMetadataQueryItem> items = await _client.MetadataManager.ExecuteMetadataQueryAsync(from: "enterprise_243888861.test", query: "numberfield >= :arg", queryParameters: queryParams, orderBy: orderByList, ancestorFolderId: "0", autoPaginate: true, marker: marker, limit: 1);
            /*** Assert ***/
            string test = items.Entries[0].Item.Description;
            Console.WriteLine(items.Entries[0].Item.Description);
        }
    }
}
