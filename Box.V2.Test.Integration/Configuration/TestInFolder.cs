using System.Threading.Tasks;
using Box.V2.Test.Integration.Configuration.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration.Configuration
{
    /// <summary>
    /// Base class for most tests. When inherited, tests in the class will be executed in a separate folder
    /// </summary>
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
