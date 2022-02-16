using System.Threading.Tasks;
using Box.V2.Test.Integration;
using Box.V2.Test.Integration.Configuration.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration.Configuration
{
    [TestClass]
    public abstract class TestInFolder : IntegrationTestBase
    {
        protected static string FolderId;

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static async Task FolderClassInitialize(TestContext context)
        {
            if (!ClassInit)
            {
                var folder = await CreateFolder("0", CommandScope.Class);
                FolderId = folder.Id;
                ClassInit = true;
            }
        }
    }
}
