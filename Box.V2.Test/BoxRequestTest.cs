using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Utility;

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
        public void ExponentialBackoff_ValidResponse()
        {
            int retryCount = 1;
            int secondsDelay = 1;
            double[] lowerBound = { 1, 2, 4};
            double[] upperBound = { 3, 6, 12};

            for (int i = 0; i < 3; i++)
            {
                TimeSpan baseInterval = TimeSpan.FromSeconds(secondsDelay);
                ExponentialBackoff expBackoff = new ExponentialBackoff();
                var backoffDelay = expBackoff.GetRetryTimeout(retryCount, baseInterval);
                Assert.IsTrue(lowerBound[i] <= backoffDelay.TotalSeconds, "Backoff Delay is not in the correct range.");
                Assert.IsTrue(backoffDelay.TotalSeconds <= upperBound[i], "Backoff Delay is not in the correct range.");
                retryCount++;
                secondsDelay++;
            }
        }
    }
}
