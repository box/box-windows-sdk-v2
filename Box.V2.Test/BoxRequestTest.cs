using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Utility;
using Box.V2.Exceptions;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxRequestTest
    {
        [TestMethod]
        public void ValidParameters_ValidRequest()
        {
            Uri baseUri = new Uri("http://api.box.com/v2");
            IBoxRequest request = new BoxRequest(baseUri, "auth/oauth2");
            request.Parameters.Add("test", "test2");

            Assert.AreEqual(request.Method, RequestMethod.Get);
            Assert.AreEqual(baseUri, request.Host);
            Assert.IsNotNull(request.Parameters);
        }

        [TestMethod]
        public void InvalidParameters_InvalidRequest()
        {
            Uri baseUri = new Uri("http://api.box.com/v2");
            try
            {
                IBoxRequest request = new BoxRequest(baseUri, "auth/../oauth2/../");
                Assert.Fail(); // raises AssertionException
            }
            catch (BoxException) {}

            try
            {
                IBoxRequest request = new BoxRequest(baseUri, "auth/../../oauth2/../");
                Assert.Fail(); // raises AssertionException
            }
            catch (BoxException) {}
        }

        [TestMethod]
        public void ExponentialBackoff_ValidResponse()
        {
            int retryCount = 1;
            double[] lowerBound = { 1, 2, 4, 8, 16, 32};
            double[] upperBound = { 3, 6, 12, 24, 48, 96};

            for (int i = 0; i < 6; i++)
            {
                ExponentialBackoff expBackoff = new ExponentialBackoff();
                var backoffDelay = expBackoff.GetRetryTimeout(retryCount);
                Assert.IsTrue(lowerBound[i] <= backoffDelay.TotalSeconds, "Backoff Delay is not in the correct range.");
                Assert.IsTrue(backoffDelay.TotalSeconds <= upperBound[i], "Backoff Delay is not in the correct range.");
                retryCount++;
            }
        }
    }
}
