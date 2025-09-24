using Box.V2.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test
{
    [TestClass]
    public class ContentTypeMapperTest : BoxResourceManagerTest
    {
        [TestMethod]
        [DataRow("avatar.png", "image/png")]
        [DataRow("avatar.jpg", "image/jpeg")]
        [DataRow("avatar.jpeg", "image/jpeg")]
        public void ContentTypeMapper_ForGivenFilename_ShouldMapToTheCorrectMimeType(string fileName, string mimeType)
        {
            Assert.AreEqual(ContentTypeMapper.GetContentTypeFromFilename(fileName), mimeType);
        }
    }
}
