using Box.V2.Converter;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxJsonConverterTest : BoxResourceManagerTest
    {
        private readonly IBoxConverter _converter;

        public BoxJsonConverterTest()
        {
            _converter = new BoxJsonConverter();
        }

        [TestMethod]
        public void BoxUserFileCollaborationEventSource_ValidateConversion()
        {
            var sourceString = "{\"file_id\":\"283257336425\",\"file_name\":\"ScreenShot2018-03-12at5.44.00PM.png\",\"user_id\":\"285663442\",\"user_name\":\"foo\",\"parent\":{\"type\":\"folder\",\"name\":\"AllFiles\",\"id\":\"0\"},\"owned_by\":{\"type\":\"user\",\"id\":\"11111\",\"name\":\"Test User\",\"login\":\"test@user.com\"}}";
            var sourceObject = _converter.Parse<BoxUserFileCollaborationEventSource>(sourceString);

            var serializedObjectString = _converter.Serialize(sourceObject);

            var boxUserFileCollaborationEventSource = _converter.Parse<BoxUserFileCollaborationEventSource>(serializedObjectString);

            Assert.AreEqual(boxUserFileCollaborationEventSource.GetType(), typeof(BoxUserFileCollaborationEventSource));
            Assert.AreEqual(boxUserFileCollaborationEventSource.Id, "283257336425");
            Assert.AreEqual(boxUserFileCollaborationEventSource.Name, "ScreenShot2018-03-12at5.44.00PM.png");
            Assert.AreEqual(boxUserFileCollaborationEventSource.UserName, "foo");
            Assert.AreEqual(boxUserFileCollaborationEventSource.OwnedBy.Id, "11111");
            Assert.AreEqual(boxUserFileCollaborationEventSource.Parent.Id, "0");
        }

        [TestMethod]
        public void BoxUserFolderCollaborationEventSource_ValidateConversion()
        {
            var sourceString = "{\"folder_id\":\"47846340014\",\"folder_name\":\"SharedWithServiceAccount\",\"user_id\":\"182069272\",\"user_name\":\"MattWiller\",\"parent\":{\"type\":\"folder\",\"name\":\"AllFiles\",\"id\":\"0\"},\"owned_by\":{\"type\":\"user\",\"id\":\"11111\",\"name\":\"Test User\",\"login\":\"test@user.com\"}}";
            var sourceObject = _converter.Parse<BoxUserFolderCollaborationEventSource>(sourceString);

            var serializedObjectString = _converter.Serialize(sourceObject);

            var boxUserFolderCollaborationEventSource = _converter.Parse<BoxUserFolderCollaborationEventSource>(serializedObjectString);

            Assert.AreEqual(boxUserFolderCollaborationEventSource.GetType(), typeof(BoxUserFolderCollaborationEventSource));
            Assert.AreEqual(boxUserFolderCollaborationEventSource.Id, "47846340014");
            Assert.AreEqual(boxUserFolderCollaborationEventSource.UserId, "182069272");
            Assert.AreEqual(boxUserFolderCollaborationEventSource.UserName, "MattWiller");
            Assert.AreEqual(boxUserFolderCollaborationEventSource.Name, "SharedWithServiceAccount");
            Assert.AreEqual(boxUserFolderCollaborationEventSource.OwnedBy.Id, "11111");
            Assert.AreEqual(boxUserFolderCollaborationEventSource.Parent.Id, "0");
        }

        [TestMethod]
        public void BoxGroupFolderCollaborationEventSource_ValidateConversion()
        {
            var sourceString = "{\"folder_id\":\"47846340014\",\"folder_name\":\"SharedWithServiceAccount\",\"group_id\":\"182069272\",\"group_name\":\"TestGroup\",\"parent\":{\"type\":\"folder\",\"name\":\"AllFiles\",\"id\":\"0\"},\"owned_by\":{\"type\":\"user\",\"id\":\"275035869\",\"name\":\"MattWiller\",\"login\":\"mwiller + appusers@box.com\"}}";
            var sourceObject = _converter.Parse<BoxGroupFolderCollaborationEventSource>(sourceString);

            var serializedObjectString = _converter.Serialize(sourceObject);

            var boxGroupFolderCollaborationEventSource = _converter.Parse<BoxGroupFolderCollaborationEventSource>(serializedObjectString);

            Assert.AreEqual(boxGroupFolderCollaborationEventSource.GetType(), typeof(BoxGroupFolderCollaborationEventSource));
            Assert.AreEqual(boxGroupFolderCollaborationEventSource.Id, "47846340014");
            Assert.AreEqual(boxGroupFolderCollaborationEventSource.Name, "SharedWithServiceAccount");
            Assert.AreEqual(boxGroupFolderCollaborationEventSource.GroupId, "182069272");
            Assert.AreEqual(boxGroupFolderCollaborationEventSource.GroupName, "TestGroup");
            Assert.AreEqual(boxGroupFolderCollaborationEventSource.Parent.Id, "0");
            Assert.AreEqual(boxGroupFolderCollaborationEventSource.OwnedBy.Id, "275035869");
        }

        [TestMethod]
        public void BoxGroupFileCollaborationEventSource_ValidateConversion()
        {
            var sourceString = "{\"file_id\":\"47846340014\",\"file_name\":\"test-picture.jpg\",\"group_id\":\"182069272\",\"group_name\":\"TestGroup\",\"parent\":{\"type\":\"folder\",\"name\":\"AllFiles\",\"id\":\"0\"},\"owned_by\":{\"type\":\"user\",\"id\":\"11111\",\"name\":\"Test User\",\"login\":\"test@user.com\"}}";
            var sourceObject = _converter.Parse<BoxGroupFileCollaborationEventSource>(sourceString);

            var serializedObjectString = _converter.Serialize(sourceObject);

            var boxGroupFileCollaborationEventSource = _converter.Parse<BoxGroupFileCollaborationEventSource>(serializedObjectString);

            Assert.AreEqual(boxGroupFileCollaborationEventSource.GetType(), typeof(BoxGroupFileCollaborationEventSource));
            Assert.AreEqual(boxGroupFileCollaborationEventSource.Id, "47846340014");
            Assert.AreEqual(boxGroupFileCollaborationEventSource.Name, "test-picture.jpg");
            Assert.AreEqual(boxGroupFileCollaborationEventSource.GroupId, "182069272");
            Assert.AreEqual(boxGroupFileCollaborationEventSource.GroupName, "TestGroup");
            Assert.AreEqual(boxGroupFileCollaborationEventSource.Parent.Id, "0");
            Assert.AreEqual(boxGroupFileCollaborationEventSource.OwnedBy.Id, "11111");
        }

        [TestMethod]
        public void BoxGroupEventSource_ValidateConversion()
        {
            var sourceString = "{\"group_id\": \"182069272\",\"group_name\": \"TestGroup\"}";
            var sourceObject = _converter.Parse<BoxGroupEventSource>(sourceString);

            var serializedObjectString = _converter.Serialize(sourceObject);

            var boxGroupEventSource = _converter.Parse<BoxGroupEventSource>(serializedObjectString);

            Assert.AreEqual(boxGroupEventSource.GetType(), typeof(BoxGroupEventSource));
            Assert.AreEqual(boxGroupEventSource.Id, "182069272");
            Assert.AreEqual(boxGroupEventSource.Name, "TestGroup");
        }
    }
}
