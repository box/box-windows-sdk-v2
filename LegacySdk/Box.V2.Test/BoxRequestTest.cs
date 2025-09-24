using System;
using Box.V2.Exceptions;
using Box.V2.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxRequestTest
    {
        [TestMethod]
        public void ValidParameters_ValidRequest()
        {
            var baseUri = new Uri("http://api.box.com/v2");
            IBoxRequest request = new BoxRequest(baseUri, "auth/oauth2");
            request.Parameters.Add("test", "test2");

            Assert.AreEqual(request.Method, RequestMethod.Get);
            Assert.AreEqual(baseUri, request.Host);
            Assert.IsNotNull(request.Parameters);
        }

        [TestMethod]
        public void InvalidParameters_InvalidRequest()
        {
            var baseUri = new Uri("http://api.box.com/v2");
            try
            {
                IBoxRequest request = new BoxRequest(baseUri, "auth/../oauth2/../");
                Assert.Fail(); // raises AssertionException
            }
            catch (BoxException) { }

            try
            {
                IBoxRequest request = new BoxRequest(baseUri, "auth/../../oauth2/../");
                Assert.Fail(); // raises AssertionException
            }
            catch (BoxException) { }
        }

        [TestMethod]
        public void ExponentialBackoff_ValidResponse()
        {
            var retryCount = 1;
            double[] lowerBound = { 1, 2, 4, 8, 16, 32 };
            double[] upperBound = { 3, 6, 12, 24, 48, 96 };

            for (var i = 0; i < 6; i++)
            {
                var expBackoff = new ExponentialBackoff();
                var backoffDelay = expBackoff.GetRetryTimeout(retryCount);
                Assert.IsTrue(lowerBound[i] <= backoffDelay.TotalSeconds, "Backoff Delay is not in the correct range.");
                Assert.IsTrue(backoffDelay.TotalSeconds <= upperBound[i], "Backoff Delay is not in the correct range.");
                retryCount++;
            }
        }
    }
}
