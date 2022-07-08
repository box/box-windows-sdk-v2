Constructing API Calls Manually
===========
The SDK also exposes low-level request methods for constructing your own API calls. These can be useful for adding your own API calls that aren't yet explicitly supported by the SDK.

To make a custom API call you need to provide implementation for `BoxResourceManager`.

```c#
public class BoxFolderHintsManager : BoxResourceManager
{
    public BoxFolderHintsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null) : base(config, service, converter, auth, asUser, suppressNotifications) { }

    public async Task<MyCustomReturnObject> GetFolderItemsWithHintsAsync(string folderId, string xRepHints)
    {
        BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.ItemsPathString, folderId))
            .Method(RequestMethod.Get)
            .Param(ParamFields, "representations")
            .Header(Constants.RequestParameters.XRepHints, xRepHints);

            return await ToResponseAsync<MyCustomReturnObject>(request).ConfigureAwait(false);
        }
    }
```

You need to first register your custom `BoxResourceManager`, then you can use it as any other SDK manager.

```c#
var client = boxJWT.UserClient(userToken, userId);
client.AddResourcePlugin<BoxFolderHintsManager>();

string folderId = "11111";
string xRepHints = "[jpg?dimensions=32x32][jpg?dimensions=94x94]";

var boxFolderHintsManager = client.ResourcePlugins.Get<BoxFolderHintsManager>();
var response = await boxFolderHintsManager.GetFolderItemsWithHintsAsync(folderId, xRepHints);
```