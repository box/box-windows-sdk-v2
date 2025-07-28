# Handling null values in Box Dotnet SDK Gen

While using Box Dotnet SDK it's important to understand how null values behave. This document provides a general overview of null value behaviour in Box Dotnet SDK to help developers manage data consistently and predictably.

## Understanding null behaviour

The Box Dotnet SDK follows a consistent pattern when handling null values in update operations. This behaviour applies to most endpoints that modify resources such as users, files, folders and metadata. The updating field behaves differently depending on weather you omit it, set it to null, or provide a value:

- Omitting the field: The field won't be included in request and the value will remain unchanged
- Setting it to null: Setting a field to null, will cause sending HTTP request with field value set to null, what will result in removing its current value or disassociates it from the resource.
- Providing a value: Providing a non-null value assigns or updates the field to that value.

## Example Usage

The client.Files.UpdateFileByIdAsync() method demonstrates null handling when modifying the lock field while updating the file:

```c#
    public async Task CreateUpdateFileAsync(BoxClient client)
    {
      var fileId = '12345';
      var fileWithLockRequestBody = new UpdateFileByIdRequestBody()
      {
        Lock = new UpdateFileByIdRequestBodyLockField() { Access = UpdateFileByIdRequestBodyLockAccessField.Lock }
      };
      var fileWithLockQueryParams = new UpdateFileByIdQueryParams() { Fields = new List<string>() { { "lock" } } };
      // locking the file
      var fileWithLock = await client.Files.UpdateFileByIdAsync(fileId, fileWithLockRequestBody, fileWithLockQueryParams);

      Console.WriteLine(fileWithLock.Lock?.Id);

      var fileWithoutLockRequestBody = new UpdateFileByIdRequestBody()
      {
        Lock = null
      };
      var fileWithoutLockQueryParams = new UpdateFileByIdQueryParams() { Fields = new List<string>() { { "lock" } } };
      // unlocking the file using lock value as null
      var fileWithoutLock = await client.Files.UpdateFileByIdAsync(fileId, fileWithoutLockRequestBody, fileWithoutLockQueryParams);

      Console.WriteLine(fileWithoutLock.Lock?.Id);
    }
```

## Summary

To summarize, if you omit the field, the field remains unchanged. If you set it to null, it clears/removes the value. If you provide a value to that field, the field gets updated to that specified value.
