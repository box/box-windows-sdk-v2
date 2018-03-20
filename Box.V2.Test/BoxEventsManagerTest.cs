using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxEventsManagerTest : BoxResourceManagerTest
    {
        private readonly BoxEventsManager _eventsManager;

        public BoxEventsManagerTest()
        {
            _eventsManager = new BoxEventsManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task GetGroupEvents_ValidResponse()
        {
            string responseString = "{\"chunk_size\": 1, \"next_stream_position\": 123, \"entries\": [{\"source\":{\"group_id\":\"942617509\",\"group_name\":\"Groupies\"},\"created_by\":{\"type\":\"user\",\"id\":\"275035869\",\"name\":\"MattWiller\",\"login\":\"mwiller + appusers@box.com\"},\"created_at\":\"2018 - 03 - 16T15: 12:52 - 07:00\",\"event_id\":\"85c57bf3 - bc15 - 4d24 - 93bc - 955c796217c8\",\"event_type\":\"GROUP_EDITED\",\"ip_address\":\"UnknownIP\",\"type\":\"event\",\"session_id\":null,\"additional_details\":null}]}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxEventCollection<BoxEnterpriseEvent>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>>>(new BoxResponse<BoxEventCollection<BoxEnterpriseEvent>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var groupEvents = await _eventsManager.EnterpriseEventsAsync();
        }
    }
}