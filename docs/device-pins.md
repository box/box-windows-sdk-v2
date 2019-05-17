Device Pins
===========

Device pins allow enterprises to control what devices can use native Box applications. To learn more about device
pinning, please see the
[Device Pinning documentation](https://community.box.com/t5/For-Admins/Device-Pinning-Overview-And-FAQs/ta-p/172).

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Get Enterprise Device Pins](#get-enterprise-device-pins)
- [Get Device Pin](#get-device-pin)
- [Delete Device Pin](#delete-device-pin)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Get Enterprise Device Pins
--------------------------

Get all device pins records for an enterprise by calling
`DevicePinManager.GetEnterpriseDevicePinsAsync(string enterpriseId, string marker = null, int limit = 100, BoxSortDirection direction = BoxSortDirection.ASC, bool autoPaginate = false)`.

<!-- sample get_enterprises_id_device_pinners -->
```c#
BoxCollectionMarkerBased<BoxDevicePin> pins = await client.DevicePinManager
    .GetEnterpriseDevicePinsAsync(enterpriseId: "12345");
```

Get Device Pin
--------------

To get information about a specific device pin, call `DevicePinManager.GetDevicePin(string id)`
with the ID of the device pin object.

<!-- sample get_device_pinners_id -->
```c#
BoxDevicePin pin = await client.DevicePinManager.GetDevicePin(id: "11111");
```

Delete Device Pin
-----------------

To remove a specific device pin, call `DevicePinManager.DeleteDevicePin(string id)` with the ID of the device
pin to delete.

<!-- sample delete_device_pinners_id -->
```c#
await client.DevicePinManager.DeleteDevicePin(id: "11111");
```