using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Box.V2.Models;
using Box.V2.Test.Integration.Configuration.Commands;
using Box.V2.Test.Integration.Configuration.Commands.CleanupCommands;
using Box.V2.Test.Integration.Configuration.Commands.DisposableCommands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public abstract class IntegrationTestBase
    {
        protected static IBoxClient UserClient;
        protected static IBoxClient AdminClient;
        protected static string UserId;
        protected static bool ClassInit = false;
        protected static bool UserCreated = false;
        protected static string EnterpriseId;

        protected static Stack<IDisposableCommand> ClassCommands;
        protected static Stack<IDisposableCommand> TestCommands;

        [AssemblyInitialize]
        public static async Task AssemblyInitialize(TestContext testContext)
        {
            var jsonConfig = Environment.GetEnvironmentVariable("INTEGRATION_TESTING_CONFIG");

            if (string.IsNullOrEmpty(jsonConfig))
            {
                Debug.WriteLine("No json config found in environment variables Reading from config.json.");
                jsonConfig = ReadFromJson("config.json");
            }

            var config = BoxConfigBuilder.CreateFromJsonString(jsonConfig)
                .Build();
            var session = new BoxJWTAuth(config);

            var json = JObject.Parse(jsonConfig);
            var adminToken = await session.AdminTokenAsync();
            AdminClient = session.AdminClient(adminToken);

            if (json["userID"] != null && json["userID"].ToString().Length != 0)
            {
                UserId = json["userID"].ToString();
            }
            else
            {
                var user = await CreateNewUser(AdminClient);

                UserId = user.Id;
            }
            var userToken = await session.UserTokenAsync(UserId);
            UserClient = session.UserClient(userToken, UserId);

            EnterpriseId = config.EnterpriseId;
        }

        [AssemblyCleanup]
        public static async Task AssemblyCleanup()
        {
            if (UserCreated)
            {
                try
                {
                    await AdminClient.UsersManager.DeleteEnterpriseUserAsync(UserId, false, true);

                }
                catch (Exception exp)
                {
                    // Delete will fail if there are content in the user
                    Console.WriteLine(exp.StackTrace);
                }
            }
        }

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void ClassInitialize(TestContext context)
        {
            if (!ClassInit)
            {
                ClassCommands = new Stack<IDisposableCommand>();
            }
        }

        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        public static async Task ClassCleanup()
        {
            await DisposeCommands(ClassCommands);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestCommands = new Stack<IDisposableCommand>();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await DisposeCommands(TestCommands);
        }

        private static async Task DisposeCommands(Stack<IDisposableCommand> commands)
        {
            while (commands.Count > 0)
            {
                var command = commands.Pop();
                IBoxClient client = GetClient(command);
                await command.Dispose(client);
            }
        }

        protected static Task<BoxUser> CreateNewUser(IBoxClient client)
        {
            var userRequest = new BoxUserRequest
            {
                Name = "IT App User - " + Guid.NewGuid().ToString(),
                IsPlatformAccessOnly = true
            };
            var user = client.UsersManager.CreateEnterpriseUserAsync(userRequest);
            UserCreated = true;
            return user;
        }

        protected static string GetUniqueName(TestContext testContext)
        {
            return string.Format("{0} - {1}", testContext.TestName, Guid.NewGuid().ToString());
        }

        protected static string GetUniqueName(string resourceName, bool includeSpecialCharacters = true)
        {
            var uniqueName = string.Format("{0} - {1}", resourceName, Guid.NewGuid().ToString());
            if (!includeSpecialCharacters)
            {
                var rgx = new Regex("[^a-zA-Z0-9]");
                uniqueName = rgx.Replace(uniqueName, "");
            }

            return uniqueName;
        }

        protected static string GetShortUniqueName(string resourceName)
        {
            return GetUniqueName(resourceName, false).Substring(0, 20);
        }

        public static async Task ExecuteCommand(ICleanupCommand command)
        {
            IBoxClient client = GetClient(command);
            await command.Execute(client);
        }

        public static async Task ExecuteCommand(INonDisposableCommand command)
        {
            IBoxClient client = GetClient(command);
            await command.Execute(client);
        }

        public static async Task<string> ExecuteCommand(IDisposableCommand command)
        {
            IBoxClient client = GetClient(command);

            var resourceId = await command.Execute(client);
            if (command.Scope == CommandScope.Test)
            {
                TestCommands.Push(command);
            }
            else
            {
                ClassCommands.Push(command);
            }
            return resourceId;
        }

        private static IBoxClient GetClient(ICommand command)
        {
            switch (command.AccessLevel)
            {
                case CommandAccessLevel.Admin:
                    return AdminClient;
                case CommandAccessLevel.User:
                    return UserClient;
                default:
                    return UserClient;
            }
        }

        public static string GetSmallFilePath()
        {
            return string.Format(AppDomain.CurrentDomain.BaseDirectory + "/TestData/smalltest.pdf");
        }

        public static string GetSmallFileV2Path()
        {
            return string.Format(AppDomain.CurrentDomain.BaseDirectory + "/TestData/smalltestV2.pdf");
        }

        public static string GetSmallPicturePath()
        {
            return string.Format(AppDomain.CurrentDomain.BaseDirectory + "/TestData/smallpic.png");
        }

        public static string ReadFromJson(string path)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            return File.ReadAllText(filePath);
        }

        public static async Task<BoxFile> CreateSmallFile(string parentId = "0", CommandScope commandScope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User)
        {
            var path = GetSmallFilePath();
            var ext = "";
            var spPath = path.Split('.');
            if (path.Length > 0)
                ext = "." + spPath[spPath.Length - 1];
            var createFileCommand = new CreateFileCommand(GetUniqueName("file") + ext, GetSmallFilePath(), parentId, commandScope, accessLevel);
            await ExecuteCommand(createFileCommand);
            return createFileCommand.File;
        }

        public static async Task<BoxFile> CreateSmallFileAsAdmin(string parentId)
        {
            return await CreateSmallFile(parentId, CommandScope.Test, CommandAccessLevel.Admin);
        }

        public static async Task DeleteFile(string fileId)
        {
            await ExecuteCommand(new DeleteFileCommand(fileId));
        }

        public static async Task DeleteFolder(string folderId)
        {
            await ExecuteCommand(new DeleteFolderCommand(folderId));
        }

        public static async Task<BoxFolder> CreateFolder(string parentId = "0", CommandScope commandScope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User)
        {
            var createFolderCommand = new CreateFolderCommand(GetUniqueName("folder"), parentId, commandScope, accessLevel);
            await ExecuteCommand(createFolderCommand);
            return createFolderCommand.Folder;
        }

        public static async Task<BoxFolder> CreateFolderAsAdmin(string parentId = "0")
        {
            return await CreateFolder(parentId, CommandScope.Test, CommandAccessLevel.Admin);
        }

        public static MemoryStream CreateFileInMemoryStream(long fileSize)
        {
            var dataArray = new byte[fileSize];
            new Random().NextBytes(dataArray);
            var memoryStream = new MemoryStream(dataArray);
            return memoryStream;
        }

        public static MemoryStream CreateBigFileInMemoryStream()
        {
            return CreateFileInMemoryStream(50000000);
        }

        public static async Task<BoxRetentionPolicy> CreateRetentionPolicy(string folderId = null, CommandScope commandScope = CommandScope.Test)
        {
            var createRetentionPolicyCommand = new CreateRetentionPolicyCommand(folderId, GetUniqueName("policy"), commandScope);
            await ExecuteCommand(createRetentionPolicyCommand);
            return createRetentionPolicyCommand.Policy;
        }

        public static async Task<BoxMetadataTemplate> CreateMetadataTemplate(List<BoxMetadataTemplateField> fields = null,
                CommandScope commandScope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.Admin)
        {
            var createMetadataTemplateCommand = new CreateMetadataTemplateCommand(GetUniqueName("template_key", false), fields, commandScope, accessLevel);
            await ExecuteCommand(createMetadataTemplateCommand);

            return createMetadataTemplateCommand.MetadataTemplate;
        }

        public static async Task<Tuple<BoxFile, string>> CreateSmallFileWithMetadata
            (string parentId = "0", Dictionary<string, object> metadata = null,
            CommandScope commandScope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.Admin)
        {
            var createFileCommand = new CreateFileCommand(GetUniqueName("file"), GetSmallFilePath(), parentId, commandScope, accessLevel);
            await ExecuteCommand(createFileCommand);

            var createMetadataTemplateCommand = new CreateMetadataTemplateCommand(GetUniqueName("template_key", false), ToStringMetadataFields(metadata), commandScope, accessLevel);
            await ExecuteCommand(createMetadataTemplateCommand);

            var applyMetadataCommand = new ApplyMetadataCommand(createMetadataTemplateCommand.TemplateKey, createFileCommand.FileId, metadata, commandScope, accessLevel);
            await ExecuteCommand(applyMetadataCommand);

            return Tuple.Create(createFileCommand.File, createMetadataTemplateCommand.TemplateKey);
        }

        private static List<BoxMetadataTemplateField> ToStringMetadataFields(Dictionary<string, object> metadataFields)
        {
            var mappedFields = new List<BoxMetadataTemplateField>();

            if (metadataFields != null)
            {
                foreach (var field in metadataFields)
                {
                    mappedFields.Add(new BoxMetadataTemplateField()
                    {
                        Type = "string",
                        Key = field.Key,
                        DisplayName = field.Key
                    });
                }
            }

            return mappedFields;
        }

        public static async Task<bool> DoesFileExistInFolder(IBoxClient client, string folderId, string fileName)
        {
            // TODO: Paging
            BoxCollection<BoxItem> boxCollection = await client.FoldersManager.GetFolderItemsAsync(folderId, 1000);
            return boxCollection.Entries.Any(item => item.Name == fileName);
        }

        public static async Task<BoxUser> CreateEnterpriseUser()
        {
            return await CreateEnterpriseUser(GetUniqueName("user"), GetUniqueName("user-id"));
        }

        public static async Task<BoxUser> CreateEnterpriseUser(string name, string externalUserAppId)
        {
            var createEnterpriseUserCommand = new CreateEnterpriseUserCommand(name, externalUserAppId);
            await ExecuteCommand(createEnterpriseUserCommand);
            return createEnterpriseUserCommand.BoxUser;
        }

        public static async Task<BoxWebLink> CreateWebLink(string name, string parentFolderId)
        {
            var createWebLinkCommand = new CreateWebLinkCommand(name, parentFolderId);
            await ExecuteCommand(createWebLinkCommand);
            return createWebLinkCommand.WebLink;
        }

        public static async Task<BoxCollaborationWhitelistTargetEntry> AddCollaborationExempt(string userId)
        {
            var addCollaborationExemptCommand = new AddCollaborationExemptCommand(userId);
            await ExecuteCommand(addCollaborationExemptCommand);
            return addCollaborationExemptCommand.WhitelistTargetEntry;
        }

        public static async Task<BoxFile> CreateNewFileVersion(string fileId)
        {
            var createNewFileVersionCommand = new CreateNewFileVersion(GetUniqueName("file"), GetSmallFilePath(), fileId);
            await ExecuteCommand(createNewFileVersionCommand);
            return createNewFileVersionCommand.File;
        }

        public static async Task<BoxUploadAvatarResponse> CreateUserAvatar(string userId)
        {
            var createAvatarCommand = new CreateUserAvatarCommand(userId, GetSmallPicturePath());
            await ExecuteCommand(createAvatarCommand);
            return createAvatarCommand.Response;
        }

        public static async Task Retry(Func<Task> action, int retries = 5, int sleep = 5000)
        {
            var retryCount = 0;
            while (retryCount < retries)
            {
                try
                {
                    await action();
                    break;
                }
                catch (Exception) { }

                Thread.Sleep(sleep);
                retryCount++;
            }

            if (retryCount >= retries)
            {
                Assert.Fail("Retries limit exceeded");
            }
        }

        public static async Task<BoxSignRequest> CreateSignRequest(string signerEmail = "sdk_integration_test@boxdemo.com", string folderId = "0")
        {
            var file = await CreateSmallFile();
            var createSignRequestCommand = new CreateSignRequestCommand(signerEmail, file.Id, folderId);
            await ExecuteCommand(createSignRequestCommand);
            return createSignRequestCommand.SignRequest;
        }

        public static async Task<BoxWebhook> CreateWebhook(string targetId = null, BoxType targetType = BoxType.folder,
            string address = "https://example.com", List<string> triggers = null)
        {
            if (targetId == null)
            {
                var folder = await CreateFolder();
                targetId = folder.Id;
            }
            if (triggers == null)
            {
                triggers = new List<string>()
                {
                    "FILE.UPLOADED"
                };
            }
            var createWebhookCommand = new CreateWebhookCommand(targetId, targetType, address, triggers);
            await ExecuteCommand(createWebhookCommand);
            return createWebhookCommand.Webhook;
        }
    }
}
