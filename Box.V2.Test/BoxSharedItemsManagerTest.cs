﻿using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Test
{

    [TestClass]
    public class BoxSharedItemsManagerTest : BoxResourceManagerTest
    {
        protected BoxSharedItemsManager _sharedItemsManager;

        public BoxSharedItemsManagerTest()
        {
            _sharedItemsManager = new BoxSharedItemsManager(_config.Object, _service, _converter, _authRepository);
        }
        [TestMethod]
        public async Task SharedItems_ValidResponse_ValidSharedLink()
        {
            /*** Arrange ***/
            string responseString = @"{
                    ""type"": ""folder"",
                    ""id"": ""11446498"",
                    ""sequence_id"": ""1"",
                    ""etag"": ""1"",
                    ""name"": ""Pictures"",
                    ""created_at"": ""2012-12-12T10:53:43-08:00"",
                    ""modified_at"": ""2012-12-12T11:15:04-08:00"",
                    ""description"": ""Some pictures I took"",
                    ""size"": 629644,
                    ""path_collection"": {
                        ""total_count"": 1,
                        ""entries"": [
                            {
                                ""type"": ""folder"",
                                ""id"": ""0"",
                                ""sequence_id"": null,
                                ""etag"": null,
                                ""name"": ""All Files""
                            }
                        ]
                    },
                    ""created_by"": {
                        ""type"": ""user"",
                        ""id"": ""17738362"",
                        ""name"": ""sean rose"",
                        ""login"": ""sean@box.com""
                    },
                    ""modified_by"": {
                        ""type"": ""user"",
                        ""id"": ""17738362"",
                        ""name"": ""sean rose"",
                        ""login"": ""sean@box.com""
                    },
                    ""owned_by"": {
                        ""type"": ""user"",
                        ""id"": ""17738362"",
                        ""name"": ""sean rose"",
                        ""login"": ""sean@box.com""
                    },
                    ""shared_link"": {
                        ""url"": ""https://www.box.com/s/vspke7y05sb214wjokpk"",
                        ""download_url"": null,
                        ""vanity_url"": null,
                        ""is_password_enabled"": false,
                        ""unshared_at"": null,
                        ""download_count"": 0,
                        ""preview_count"": 0,
                        ""access"": ""open"",
                        ""permissions"": {
                            ""can_download"": true,
                            ""can_preview"": true
                        }
                    },
                    ""folder_upload_email"": {
                        ""access"": ""open"",
                        ""email"": ""upload.Picture.k13sdz1@u.box.com""
                    },
                    ""parent"": {
                        ""type"": ""folder"",
                        ""id"": ""0"",
                        ""sequence_id"": null,
                        ""etag"": null,
                        ""name"": ""All Files""
                    },
                    ""item_status"": ""active"",
                    ""item_collection"": {
                        ""total_count"": 1,
                        ""entries"": [
                            {
                                ""type"": ""file"",
                                ""id"": ""5000948880"",
                                ""sequence_id"": ""3"",
                                ""etag"": ""3"",
                                ""sha1"": ""134b65991ed521fcfe4724b7d814ab8ded5185dc"",
                                ""name"": ""tigers.jpeg""
                            }
                        ],
                        ""offset"": 0,
                        ""limit"": 100
                    }
                }";
            _handler.Setup(h => h.ExecuteAsync<BoxItem>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxItem>>(new BoxResponse<BoxItem>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxItem result = await _sharedItemsManager.SharedItemsAsync("fakeSharedLink");

            /*** Assert ***/
            Assert.AreEqual("11446498", result.Id);
            Assert.AreEqual(1, result.PathCollection.TotalCount);
            Assert.AreEqual("https://www.box.com/s/vspke7y05sb214wjokpk", result.SharedLink.Url);
            Assert.AreEqual("17738362", result.CreatedBy.Id);
            
        }
    }
}