using Box.V2.Converter;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test
{
    [TestClass]
    public class SingleOrCollectionConverterTest : BoxResourceManagerTest
    {
        private readonly IBoxConverter _converter;

        public SingleOrCollectionConverterTest()
        {
            _converter = new BoxJsonConverter();
        }

        [TestMethod]
        public void SingleObject()
        {
            var json = LoadFixtureFromJson("Fixtures/Converters/SingleOrCollectionConverter/SingleObject.json");
            var error = _converter.Parse<BoxConflictErrorContextInfo<BoxFolder>>(json);
            Assert.AreEqual(error.Conflicts[0].Name, "Test Folder");
        }

        [TestMethod]
        public void Array()
        {
            var json = LoadFixtureFromJson("Fixtures/Converters/SingleOrCollectionConverter/Array.json");
            var error = _converter.Parse<BoxConflictErrorContextInfo<BoxFolder>>(json);
            Assert.AreEqual(error.Conflicts[0].Name, "Test Folder");
            Assert.AreEqual(error.Conflicts[1].Name, "Test Folder 2");
        }

        [TestMethod]
        public void EmptyArray()
        {
            var json = LoadFixtureFromJson("Fixtures/Converters/SingleOrCollectionConverter/EmptyArray.json");
            var error = _converter.Parse<BoxConflictErrorContextInfo<BoxFolder>>(json);
            Assert.AreEqual(error.Conflicts.Count, 0);
        }

        [TestMethod]
        public void Empty()
        {
            var json = LoadFixtureFromJson("Fixtures/Converters/SingleOrCollectionConverter/Empty.json");
            var error = _converter.Parse<BoxConflictErrorContextInfo<BoxFolder>>(json);
            Assert.IsNull(error.Conflicts);
        }
    }
}
