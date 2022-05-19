using System;
using Box.V2.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test
{
    [TestClass]
    public class RetryStrategyTest : BoxResourceManagerTest
    {
        [TestMethod]
        public void ExponentialBackoff_ForDifferentRetryNumber_ReturnCorrectRetryTime()
        {
            var exponentialBackoff = new ExponentialBackoff();

            var firstRetry = exponentialBackoff.GetRetryTimeout(1);
            Assert.IsTrue(firstRetry <= TimeSpan.FromSeconds(3));
            Assert.IsTrue(firstRetry >= TimeSpan.FromSeconds(1));

            var secondRetry = exponentialBackoff.GetRetryTimeout(2);
            Assert.IsTrue(secondRetry <= TimeSpan.FromSeconds(6));
            Assert.IsTrue(secondRetry >= TimeSpan.FromSeconds(2));

            var thirdRetry = exponentialBackoff.GetRetryTimeout(3);
            Assert.IsTrue(thirdRetry <= TimeSpan.FromSeconds(12));
            Assert.IsTrue(thirdRetry >= TimeSpan.FromSeconds(4));

            var fourthRetry = exponentialBackoff.GetRetryTimeout(4);
            Assert.IsTrue(fourthRetry <= TimeSpan.FromSeconds(24));
            Assert.IsTrue(fourthRetry >= TimeSpan.FromSeconds(8));

            var fifthRetry = exponentialBackoff.GetRetryTimeout(5);
            Assert.IsTrue(fifthRetry <= TimeSpan.FromSeconds(48));
            Assert.IsTrue(fifthRetry >= TimeSpan.FromSeconds(16));
        }
    }
}
