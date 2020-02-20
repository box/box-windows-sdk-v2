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
            var config = new BoxConfig(ClientId, ClientSecret, EnterpriseId, privateKey, passphrase, publicKeyID);
            var session = new BoxJWTAuth(config);
            var adminToken = session.AdminToken();
            adminClient = session.AdminClient(adminToken);

            /*** Act ***/
            var queryParams = new Dictionary<string, object>();
            queryParams.Add("arg", "Templ Name");
            //List <BoxMetadataQueryOrderBy> orderByList = new List<BoxMetadataQueryOrderBy>();
            //var orderBy = new BoxMetadataQueryOrderBy()
            //{
            //    FieldKey = "amount",
            //    Direction = BoxSortDirection.ASC
            //};
            //orderByList.Add(orderBy);
            string marker = "0!KUOigib6KwkKUPL8kWCIZF1PKD8yeJqD0ehjzMOlzvQwHwiEgfH5NOEiO4Thwnye6nlZpCTZjJJavfGQTZ405cR6qCHclXXsA81Dgbgo13S2WEHEColWvE4En-NrR7-xJljx02V-g3LstI_lYirVMY__uDJbMeG--JAMiFFwtUNppPh8DPPmrn8kxCrSCdQ8LW1ZEQc2XwKRprfDQOlvI2YAjTy9GJvG4GjHE1o5UwNcSrAQZAErUv1geExWJb7W2oIVByTm3LXnS_n6B0h0ZQkj8Gx0aK_teOwPsibDTVnu-J4uRhhgrGgUD0nzfAY3YS0nIjBbg3Jwyz1MNHNURBxKEO4LfoH4OpJWFz8RBpxRHX3z0a_2Zbo8mN2O11-lN20jAGdNOFxSyf7K-e2bmupWaB_0Cplo-YTLBLGBrVSzlnl8_7_C4saijSr0wxYva1oIvg..";
            BoxCollectionMarkerBased<BoxMetadataQueryItem> items = await adminClient.MetadataManager.ExecuteMetadataQueryAsync(from: "enterprise_22899933.relayWorkflowInformation", query: "templateName = :arg", queryParameters: queryParams, ancestorFolderId: "0", autoPaginate: false, limit: 1);
            /*** Assert ***/
            Debug.WriteLine(items.Entries.ToString());

        }
    }
}
