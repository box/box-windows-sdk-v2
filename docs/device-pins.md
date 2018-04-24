Device Pins
===========

Device pins allow enterprises to control what devices can use native Box applications. To learn more about device
pinning, please see the
[Device Pinning documentation](https://community.box.com/t5/For-Admins/Device-Pinning-Overview-And-FAQs/ta-p/172).

Get Enterprise Device Pins
--------------------------

Get all device pins records for an enterprise by calling
`DevicePinManager.GetEnterpriseDevicePinsAsync(string enterpriseId, string marker = null, int limit = 100, BoxSortDirection direction = BoxSortDirection.ASC, bool autoPaginate = false)`.

```c#
BoxCollectionMarkerBased<BoxDevicePin> pins = await client.DevicePinManager
    .GetEnterpriseDevicePinsAsync(enterpriseId: "12345");
```

Get Device Pin
--------------

To get information about a specific device pin, call `DevicePinManager.GetDevicePin(string id)`
with the ID of the device pin object.

```c#
BoxDevicePin pin = await client.DevicePinManager.GetDevicePin(id: "11111");
```

Delete Device Pin
-----------------

To remove a specific device pin, call `DevicePinManager.DeleteDevicePin(string id)` with the ID of the device
pin to delete.

```c#
await client.DevicePinManager.DeleteDevicePin(id: "11111");
```