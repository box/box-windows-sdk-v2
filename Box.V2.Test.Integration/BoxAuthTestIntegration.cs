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
        public const string ClientId = "5t1jh03gpivv76p0ue2cro332uxui2t6";
        public const string ClientSecret = "cbXjQbG0Oy4WQZAUVp1L2zgVGI0po5aZ";
        public const string EnterpriseId = "243888861";
        public const string publicKeyID = "tx8wwo9i";
        public const string privateKey = "-----BEGIN ENCRYPTED PRIVATE KEY-----\nMIIFDjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQI+ppv12JKNGgCAggA\nMBQGCCqGSIb3DQMHBAh9ltiv2KkbowSCBMjmrEQoCz7xVoN+cBea5wzsX/GgK2NF\nua/8h/KBGPuo8rurd+Y0LLU/I7JdlfZLgpnWe56Ip1Mav4ajOZhg/loMGi0ilTCe\n6gPGXqTXuvcW754BqGDPpt0odgZ5lHYOU00BQz6erafr0EnPDdmqfyoeLjBun3fO\nSSVACR0dVI82wfOrTMNcTSMp6jTw4Vhwxrf4VY3MGW3DrN7XK8CXIsJmWCnmNFmq\n8+xVbghuDYdareXdNT1I9E+doJq+7qc0p0IdELtx8rwpoPh7tQPms6eRcAhPwF16\nQp8D/8+5xFS/6tyyQszWy0fqtfPaIgeEPmHVR0FWJtp8pTx1MyXhFiYMnIm1/95U\nIgPaoozUJHA4u0DuhtCSH/SVwjWcqcYzGOz3qqDoAdNfS0O1QbVhJ+tBfyrNkhmW\nseZev+MxQMs1RwAgkqYszFhr181o82f4V6VZf5N88oM6aSN6L9X1fIT8KBh6ZoBR\n4Z526M6kM4rxdAdF+Nh+QFX/LY8ygJCAGH3wclDFGRC8HuuoTg69reBpFwDH6H72\nwnzPplH26CxTtFHs6WQxXBy9R31btFLT7W8W97C5CzChy4q3UtwdD3USeRtitr7f\nhKq97FYlpj8O3oEIFqt2ebg3/6UN/6W4N3XS2RTOqU8uDXtd3AJWKuM6hJRV2MhP\nltxtWfzZETlntp+yqc1/7q/3lyBVYwA8zs3wyntDSkNxVnflCXd2MKe0DYF0cgCu\nasahCb80Bv/DoLhwd9Ukm6E3NhKTvBsnQyd5TlJpS4buz01gmTRlWU5Ck1QEgl1G\ncN9lz81LBjNZ3QsUeX+a3EKK+KL3c6buTV+0ISezl0MX2ESuuL3x8HXEHAeiVJT9\n+eLwcsKwALfRiSe9nHHuFMCu62dgowaP/gImqKkJBsAFHjpDNQ4005mPCawHVjkz\ny6vdYISW0j/tHAh6wz6k1cLiDH8t+eY4O9bVOJaOVQaausXH1fXC0tEG5+FosrMv\nPtBdLIYjjKks7HbwBabwKkKp0qXC4bNnrK86ZPIx5PbWr5YpKrfPUvH7pn+SZB1Y\nzz0wXLFw1aE+5fnqHbJIWTeT5sdlnRU7ywX+elCkOEzV2+Hr8MbNPVVsOiVN5Dlg\nlh8OTs7EFFqJWBeGMKAajXjd292vU2wog5mrw5Z0XymBrC+9kXfe8+xxCoIa84Eo\nayThu7bTUU4vFFO6cztesh3oED/gqiBpn0E0v6U0WsggQmQ6479sMwPEdSQ4+ldi\nW7CHMjIeFvQy+96oQdbtwjWHDg+B+AQBbS5mwLtj0iyAJps090dS8HxmTEJCf+QM\ni7aPuZqltQN5IctTl1dQi+ESF/zsHJLrYNX8/tV0rmVjuRRlAJZO8Mh4GswF/gpE\nnCsVn8blgoIem/Y6VmSIEB3GAy4YX4redREzSG6SQNy1TsI2jrhijoZhMicGpoUV\n9oiUSlVf9fdPOuIgaa1IHEVDZ1yLOCut3r+S/af3jqlA7kHIzwuHEQV2EB2cflLu\nerILNBFJPFqutLEHnCUvNPcUiQJxUrw5Lzg0rr8lexc0Hx0rKR6n3Dj8WSSEcqgz\nUfaHMECh+r09GDWnC6attgtEJvRsmy/nLwT8aLAu9iAVgtSSant+f3xCQgSuTXzl\n9JQ=\n-----END ENCRYPTED PRIVATE KEY-----\n";
        public const string passphrase = "c5ac6970728dfb85b7de66eaf00d9c2f";

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
            queryParams.Add("arg", 5);
            //List <BoxMetadataQueryOrderBy> orderByList = new List<BoxMetadataQueryOrderBy>();
            //var orderBy = new BoxMetadataQueryOrderBy()
            //{
            //    FieldKey = "amount",
            //    Direction = BoxSortDirection.ASC
            //};
            //orderByList.Add(orderBy);
            //string marker = "0!KUOigib6KwkKUPL8kWCIZF1PKD8yeJqD0ehjzMOlzvQwHwiEgfH5NOEiO4Thwnye6nlZpCTZjJJavfGQTZ405cR6qCHclXXsA81Dgbgo13S2WEHEColWvE4En-NrR7-xJljx02V-g3LstI_lYirVMY__uDJbMeG--JAMiFFwtUNppPh8DPPmrn8kxCrSCdQ8LW1ZEQc2XwKRprfDQOlvI2YAjTy9GJvG4GjHE1o5UwNcSrAQZAErUv1geExWJb7W2oIVByTm3LXnS_n6B0h0ZQkj8Gx0aK_teOwPsibDTVnu-J4uRhhgrGgUD0nzfAY3YS0nIjBbg3Jwyz1MNHNURBxKEO4LfoH4OpJWFz8RBpxRHX3z0a_2Zbo8mN2O11-lN20jAGdNOFxSyf7K-e2bmupWaB_0Cplo-YTLBLGBrVSzlnl8_7_C4saijSr0wxYva1oIvg..";
            BoxCollectionMarkerBased<BoxMetadataQueryItem> items = await adminClient.MetadataManager.ExecuteMetadataQueryAsync(from: "enterprise_243888861.test", query: "numberfield >= 5", ancestorFolderId: "0", autoPaginate: false, limit: 1);
            /*** Assert ***/
            string test = items.Entries[0].Item.Description;
            Console.WriteLine(items.Entries[0].Item.Description);
        }
    }
}
