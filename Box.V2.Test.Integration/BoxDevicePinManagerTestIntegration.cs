using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxDevicePinManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task EnterpriseDevicePins_LiveSession()
        {
            const string EnterpriseId = "440385";

            //get enterprise device pins
            var pins = await Client.DevicePinManager.GetEnterpriseDevicePinsAsync(EnterpriseId, autoPaginate: true);
            Assert.IsTrue(pins.Entries.Count == 1, "Failed to get enterprise device pins.");

            //get device pin by id
            var devicePin = await Client.DevicePinManager.GetDevicePin(pins.Entries[0].Id);
            Assert.AreEqual(pins.Entries[0].Id, devicePin.Id, "Failed to get device pin by id.");

            // This test code is disabled because the device pins need to be created manually and we don't want to delete our test device pin.
            //delete device pin
            //var result = await _client.DevicePinManager.DeleteDevicePin(devicePin.Id);
            //Assert.IsTrue(result, "Failed to delete device pin");
        }
    }
}
