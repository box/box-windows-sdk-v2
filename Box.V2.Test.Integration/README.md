Integration tests
==================

## Setup

To run the integration tests locally, you need an existing Box app. Create a box app in https://cloud.app.box.com/developers/console. Select the JWT authentication method. You must enable all possible scopes and authorize your app to access the Enterprise if you want to run all the tests. Some tests requires enterprise access and different scopes. When your application is ready download App Settings as and replace the existing `config.json` file in this directory with your own. 

There is an optional `userId` field that you can fill in with your existing `userId`, so this user will be used in the tests and no new user will be created.

You can run the test using Visual Studio test explorer or from the command line by calling the following command 

```
dotnet test .\Box.V2.Test.Integration
```

By default, integration tests will run for .net framework and .net core. You can run tests against either of these by passing the -f flag as follows.

```
dotnet test .\Box.V2.Test.Integration -f netcoreapp2.0
```

## Command approach for test setup

Typically, integration tests require pre-existing resources (such as files) to operate on them. Such resources should also be removed from the account when the test ends or when an exception is thrown. To eliminate the need to duplicate such logic, the command approach was introduced.

An example `IDisposableCommand` implementation looks like this 

```c#
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateFolderCommand : CommandBase, IDisposableCommand
    {
        private readonly string _folderName;
        private readonly string _parentId;

        public string FolderId;
        public BoxFolder Folder;

        public CreateFolderCommand(string folderName, string parentId = "0", CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User) : base(scope, accessLevel)
        {
            _folderName = folderName;
            _parentId = parentId;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            var createFolderRequest = new BoxFolderRequest
            {
                Name = _folderName,
                Parent = new BoxRequestEntity
                {
                    Id = _parentId
                }
            };
            Folder = await client.FoldersManager.CreateAsync(createFolderRequest);
            FolderId = Folder.Id;
            return FolderId;
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.FoldersManager.DeleteAsync(FolderId);

            await client.FoldersManager.PurgeTrashedFolderAsync(FolderId);
        }
    }
}
```

The Execute command will be called when the command is executed, and Dispose when the test is terminated or an exception is thrown. As you can see, dispose or execute command could consist of multiple API calls. 

`CommandAccessLevel` indicates which BoxClient should be used for the test (managed or service account). Depends on what you may need and the enterprise access (e.g., to create a MetadataTemplate).

`CommandScope` indicates when dispose will be called (test, class, assembly). You can usually keep this as `CommandScope.Test` for single test setup commands.

Commands are usually wrapped in helper methods that look like this

```c#
public static async Task<BoxFolder> CreateFolder(string parentId = "0", CommandScope commandScope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User)
{
    var createFolderCommand = new CreateFolderCommand(GetUniqueName("folder"), parentId, commandScope, accessLevel);
    await ExecuteCommand(createFolderCommand);
    return createFolderCommand.Folder;
}
```

You can then simply call this command in your test as follows

```c#
[TestMethod]
public async Task GetWatermarkAsync_ForExistingFolder_ShouldCorrectlyApplyWatermarkOnFolder()
{
    var folder = await CreateFolder(FolderId);
    await UserClient.FoldersManager.ApplyWatermarkAsync(folder.Id);

    var watermark = await UserClient.FoldersManager.GetWatermarkAsync(folder.Id);

    Assert.IsNotNull(watermark);
}
```

As you can see, since this test does not test folder creation, so command logic is used. Try using or adding commands like this to simplify and reuse the test setup. This makes the tests more readable, easier to maintain, and ensures that the test account is in the same state after and before running all the tests.