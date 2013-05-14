using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Web;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxRequestTest
    {
        [TestMethod]
        public void ParamTest()
        {
            Uri baseUri = new Uri("http://api.box.com/v2");
            IBoxRequest qb = new BoxRequest(RequestMethod.GET, baseUri, "auth/oauth2");
            qb.Param("test", "test2");

            Assert.AreEqual(baseUri, qb.Host);
            Assert.IsNotNull(qb.Parameters);
        }
    }
}
