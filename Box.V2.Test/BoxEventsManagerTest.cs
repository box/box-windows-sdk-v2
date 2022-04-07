using System;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
        public async Task EnterpriseEventsAsync_DateEncodingUTC_ValidResponse()
        {
            var responseString = "{\"chunk_size\": 1, \"next_stream_position\": 123, \"entries\": [{\"source\":{\"group_id\":\"942617509\",\"group_name\":\"Groupies\"},\"created_by\":{\"type\":\"user\",\"id\":\"11111\",\"name\":\"Test User\",\"login\":\"test@user.com\"},\"action_by\":{\"type\":\"user\",\"id\":\"12345\",\"name\":\"Test User\",\"login\":\"test@user.com\"},\"created_at\":\"2018-03-16T15:12:52-07:00\",\"event_id\":\"85c57bf3-bc15-4d24-93bc-955c796217c8\",\"event_type\":\"GROUP_EDITED\",\"ip_address\":\"UnknownIP\",\"type\":\"event\",\"session_id\":null,\"additional_details\":null}]}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxEventCollection<BoxEnterpriseEvent>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>>>(new BoxResponse<BoxEventCollection<BoxEnterpriseEvent>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var events = await _eventsManager.EnterpriseEventsAsync(createdAfter: new DateTimeOffset(1988, 11, 18, 9, 30, 0, TimeSpan.Zero), createdBefore: new DateTimeOffset(2018, 11, 18, 9, 30, 0, TimeSpan.Zero));
            var firstEvent = events.Entries.First<BoxEnterpriseEvent>();

            /*** Assert ***/
            Assert.AreEqual("stream_type=admin_logs&limit=500&created_after=1988-11-18T09%3A30%3A00%2B00%3A00&created_before=2018-11-18T09%3A30%3A00%2B00%3A00", boxRequest.GetQueryString());
            Assert.AreEqual("12345", firstEvent.ActionBy.Id);
            Assert.AreEqual("user", firstEvent.ActionBy.Type);
            Assert.AreEqual("Test User", firstEvent.ActionBy.Name);
            Assert.AreEqual("test@user.com", firstEvent.ActionBy.Login);
            Assert.AreEqual("942617509", firstEvent.Source.Id);
            Assert.AreEqual("85c57bf3-bc15-4d24-93bc-955c796217c8", firstEvent.EventId);
        }

        [TestMethod]
        public async Task GetGroupEvents_ValidResponse()
        {
            var responseString = "{\"chunk_size\": 1, \"next_stream_position\": 123, \"entries\": [{\"source\":{\"group_id\":\"942617509\",\"group_name\":\"Groupies\"},\"created_by\":{\"type\":\"user\",\"id\":\"11111\",\"name\":\"Test User\",\"login\":\"test@user.com\"},\"created_at\":\"2018-03-16T15:12:52-07:00\",\"event_id\":\"85c57bf3-bc15-4d24-93bc-955c796217c8\",\"event_type\":\"GROUP_EDITED\",\"ip_address\":\"UnknownIP\",\"type\":\"event\",\"session_id\":null,\"additional_details\":null}]}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxEventCollection<BoxEnterpriseEvent>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>>>(new BoxResponse<BoxEventCollection<BoxEnterpriseEvent>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var groupEvents = await _eventsManager.EnterpriseEventsAsync();

            var groupEventSource = groupEvents.Entries[0].Source as BoxGroupEventSource;

            Assert.AreEqual(groupEvents.Entries[0].EventType, "GROUP_EDITED");
            Assert.AreEqual(groupEvents.Entries[0].Source.GetType(), typeof(BoxGroupEventSource));
            Assert.AreEqual(groupEventSource.Id, "942617509");
            Assert.AreEqual(groupEventSource.Name, "Groupies");
        }

        [TestMethod]
        public async Task GetUserEventsFile_ValidResponse()
        {
            var responseString = "{\"chunk_size\": 1, \"next_stream_position\": 123, \"entries\": [{\"source\":{\"file_id\":\"283257336425\",\"file_name\":\"ScreenShot2018-03-12at5.44.00PM.png\",\"user_id\":\"285663442\",\"user_name\":\"foo\",\"parent\":{\"type\":\"folder\",\"name\":\"AllFiles\",\"id\":\"0\"}},\"created_by\":{\"type\":\"user\",\"id\":\"11111\",\"name\":\"Test User\",\"login\":\"test@user.com\"},\"created_at\":\"2018-03-16T15:12:52-07:00\",\"event_id\":\"85c57bf3-bc15-4d24-93bc-955c796217c8\",\"event_type\":\"COLLABORATION_INVITE\",\"ip_address\":\"UnknownIP\",\"type\":\"event\",\"session_id\":null,\"additional_details\":null}]}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxEventCollection<BoxEnterpriseEvent>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>>>(new BoxResponse<BoxEventCollection<BoxEnterpriseEvent>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var userFileEvents = await _eventsManager.EnterpriseEventsAsync();

            var userFileEventSource = userFileEvents.Entries[0].Source as BoxUserFileCollaborationEventSource;

            Assert.AreEqual(userFileEvents.Entries[0].EventType, "COLLABORATION_INVITE");
            Assert.AreEqual(userFileEvents.Entries[0].Source.GetType(), typeof(BoxUserFileCollaborationEventSource));
            Assert.AreEqual(userFileEventSource.Id, "283257336425");
            Assert.AreEqual(userFileEventSource.Name, "ScreenShot2018-03-12at5.44.00PM.png");
        }

        [TestMethod]
        public async Task GetUserEventsFolder_ValidResponse()
        {
            var responseString = "{\"chunk_size\": 1, \"next_stream_position\": 123, \"entries\": [{\"source\":{\"folder_id\":\"47846340014\",\"folder_name\":\"SharedWithServiceAccount\",\"user_id\":\"182069272\",\"user_name\":\"MattWiller\",\"parent\":{\"type\":\"folder\",\"name\":\"AllFiles\",\"id\":\"0\"}},\"created_by\":{\"type\":\"user\",\"id\":\"11111\",\"name\":\"Test User\",\"login\":\"test@user.com\"},\"created_at\":\"2018-03-16T15:12:52-07:00\",\"event_id\":\"85c57bf3-bc15-4d24-93bc-955c796217c8\",\"event_type\":\"COLLABORATION_INVITE\",\"ip_address\":\"UnknownIP\",\"type\":\"event\",\"session_id\":null,\"additional_details\":null}]}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxEventCollection<BoxEnterpriseEvent>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>>>(new BoxResponse<BoxEventCollection<BoxEnterpriseEvent>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var userFolderEvents = await _eventsManager.EnterpriseEventsAsync();

            var userFolderEventSource = userFolderEvents.Entries[0].Source as BoxUserFolderCollaborationEventSource;

            Assert.AreEqual(userFolderEvents.Entries[0].EventType, "COLLABORATION_INVITE");
            Assert.AreEqual(userFolderEvents.Entries[0].Source.GetType(), typeof(BoxUserFolderCollaborationEventSource));
            Assert.AreEqual(userFolderEventSource.Id, "47846340014");
            Assert.AreEqual(userFolderEventSource.Name, "SharedWithServiceAccount");
        }

        [TestMethod]
        public async Task GetGroupEventsFolder_ValidResponse()
        {
            var responseString = "{\"chunk_size\": 1, \"next_stream_position\": 123, \"entries\": [{\"source\":{\"folder_id\":\"47846340014\",\"folder_name\":\"SharedWithServiceAccount\",\"group_id\":\"182069272\",\"group_name\":\"TestGroup\",\"parent\":{\"type\":\"folder\",\"name\":\"AllFiles\",\"id\":\"0\"}},\"created_by\":{\"type\":\"user\",\"id\":\"275035869\",\"name\":\"MattWiller\",\"login\":\"mwiller + appusers@box.com\"},\"created_at\":\"2018-03-16T15:12:52-07:00\",\"event_id\":\"85c57bf3-bc15-4d24-93bc-955c796217c8\",\"event_type\":\"COLLABORATION_INVITE\",\"ip_address\":\"UnknownIP\",\"type\":\"event\",\"session_id\":null,\"additional_details\":null}]}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxEventCollection<BoxEnterpriseEvent>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>>>(new BoxResponse<BoxEventCollection<BoxEnterpriseEvent>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var groupFolderEvents = await _eventsManager.EnterpriseEventsAsync();

            var groupFolderEventSource = groupFolderEvents.Entries[0].Source as BoxGroupFolderCollaborationEventSource;

            Assert.AreEqual(groupFolderEvents.Entries[0].EventType, "COLLABORATION_INVITE");
            Assert.AreEqual(groupFolderEvents.Entries[0].Source.GetType(), typeof(BoxGroupFolderCollaborationEventSource));
            Assert.AreEqual(groupFolderEventSource.Id, "47846340014");
            Assert.AreEqual(groupFolderEventSource.GroupName, "TestGroup");
        }

        [TestMethod]
        public async Task GetGroupEventsFile_ValidResponse()
        {
            var responseString = "{\"chunk_size\": 1, \"next_stream_position\": 123, \"entries\": [{\"source\":{\"file_id\":\"47846340014\",\"file_name\":\"test-picture.jpg\",\"group_id\":\"182069272\",\"group_name\":\"TestGroup\"},\"created_by\":{\"type\":\"user\",\"id\":\"11111\",\"name\":\"Test User\",\"login\":\"test@user.com\"},\"created_at\":\"2018-03-16T15:12:52-07:00\",\"event_id\":\"85c57bf3-bc15-4d24-93bc-955c796217c8\",\"event_type\":\"COLLABORATION_INVITE\",\"ip_address\":\"UnknownIP\",\"type\":\"event\",\"session_id\":null,\"additional_details\":null}]}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxEventCollection<BoxEnterpriseEvent>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>>>(new BoxResponse<BoxEventCollection<BoxEnterpriseEvent>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var groupFileEvents = await _eventsManager.EnterpriseEventsAsync();

            var groupFileEventSource = groupFileEvents.Entries[0].Source as BoxGroupFileCollaborationEventSource;

            Assert.AreEqual(groupFileEvents.Entries[0].EventType, "COLLABORATION_INVITE");
            Assert.AreEqual(groupFileEvents.Entries[0].Source.GetType(), typeof(BoxGroupFileCollaborationEventSource));
            Assert.AreEqual(groupFileEventSource.Id, "47846340014");
            Assert.AreEqual(groupFileEventSource.Name, "test-picture.jpg");
        }

        [TestMethod]
        public async Task GetApplications_ValidResponse()
        {
            var responseString = "{\"chunk_size\": 1, \"next_stream_position\": 123, \"entries\": [{\"source\":{\"type\":\"application\",\"name\":\"AppUsersSample\",\"api_key\":\"9aktq31ca981fk2wc1dml6y1douxscx9\"},\"created_at\":\"2018-03-16T15:12:52-07:00\",\"event_id\":\"85c57bf3-bc15-4d24-93bc-955c796217c8\",\"event_type\":\"COLLABORATION_INVITE\",\"ip_address\":\"UnknownIP\",\"type\":\"event\",\"session_id\":null,\"additional_details\":null}]}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxEventCollection<BoxEnterpriseEvent>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>>>(new BoxResponse<BoxEventCollection<BoxEnterpriseEvent>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var applicationEvents = await _eventsManager.EnterpriseEventsAsync();

            var applicationEventSource = applicationEvents.Entries[0].Source as BoxApplication;

            Assert.AreEqual(applicationEventSource.Name, "AppUsersSample");
            Assert.AreEqual(applicationEventSource.Type, "application");
            Assert.AreEqual(applicationEventSource.ApiKey, "9aktq31ca981fk2wc1dml6y1douxscx9");
        }

        [TestMethod]
        public async Task GetWebLinkEvents_ValidResponse()
        {
            var responseString = "{\"chunk_size\": 1, \"next_stream_position\": 123, \"entries\": [{\"source\":{\"item_type\":\"web_link\",\"item_id\":\"77777\",\"item_name\":\"My Bookmark\",\"parent\":{\"type\":\"folder\",\"name\":\"Web Sites I Like\",\"id\":\"22222\"}},\"created_by\":{\"type\":\"user\",\"id\":\"33333\",\"name\":\"Test User\",\"login\":\"testuser@example.com\"},\"created_at\":\"2018-03-16T15:12:52-07:00\",\"event_id\":\"85c57bf3-bc15-4d24-93bc-955c796217c8\",\"event_type\":\"SHARE\",\"ip_address\":\"UnknownIP\",\"type\":\"event\",\"session_id\":null,\"additional_details\":null}]}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxEventCollection<BoxEnterpriseEvent>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>>>(new BoxResponse<BoxEventCollection<BoxEnterpriseEvent>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var webLinkEvents = await _eventsManager.EnterpriseEventsAsync();

            var webLinkEventSource = webLinkEvents.Entries[0].Source as BoxWebLinkEventSource;

            Assert.AreEqual(webLinkEvents.Entries[0].EventType, "SHARE");
            Assert.AreEqual(webLinkEvents.Entries[0].Source.GetType(), typeof(BoxWebLinkEventSource));
            Assert.AreEqual(webLinkEventSource.Id, "77777");
            Assert.AreEqual(webLinkEventSource.Name, "My Bookmark");
            Assert.AreEqual(webLinkEventSource.Type, "web_link");
            Assert.AreEqual(webLinkEventSource.Parent.Id, "22222");
        }

        [TestMethod]
        public async Task EnterpriseEventsStreamingAsync_ValidResponse()
        {
            var responseString = "{\"chunk_size\": 1, \"next_stream_position\": 123, \"entries\": [{\"source\":{\"group_id\":\"942617509\",\"group_name\":\"Groupies\"},\"created_by\":{\"type\":\"user\",\"id\":\"11111\",\"name\":\"Test User\",\"login\":\"test@user.com\"},\"action_by\":{\"type\":\"user\",\"id\":\"12345\",\"name\":\"Test User\",\"login\":\"test@user.com\"},\"created_at\":\"2018-03-16T15:12:52-07:00\",\"event_id\":\"85c57bf3-bc15-4d24-93bc-955c796217c8\",\"event_type\":\"GROUP_EDITED\",\"ip_address\":\"UnknownIP\",\"type\":\"event\",\"session_id\":null,\"additional_details\":null}]}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxEventCollection<BoxEnterpriseEvent>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>>>(new BoxResponse<BoxEventCollection<BoxEnterpriseEvent>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var events = await _eventsManager.EnterpriseEventsStreamingAsync();
            var firstEvent = events.Entries.First<BoxEnterpriseEvent>();

            /*** Assert ***/
            Assert.AreEqual("stream_type=admin_logs_streaming&limit=500", boxRequest.GetQueryString());
            Assert.AreEqual("12345", firstEvent.ActionBy.Id);
            Assert.AreEqual("user", firstEvent.ActionBy.Type);
            Assert.AreEqual("Test User", firstEvent.ActionBy.Name);
            Assert.AreEqual("test@user.com", firstEvent.ActionBy.Login);
            Assert.AreEqual("942617509", firstEvent.Source.Id);
            Assert.AreEqual("85c57bf3-bc15-4d24-93bc-955c796217c8", firstEvent.EventId);
        }
    }
}
