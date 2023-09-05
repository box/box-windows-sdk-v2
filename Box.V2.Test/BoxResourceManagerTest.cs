using System;
using System.IO;
using System.Text;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Request;
using Box.V2.Services;
using Box.V2.Test.Helpers;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Box.V2.Test
{
    public abstract class BoxResourceManagerTest
    {
        protected IBoxConverter Converter;
        protected Mock<IRequestHandler> Handler;
        protected IBoxService Service;
        protected Mock<IBoxConfig> Config;
        protected AuthRepository AuthRepository;

        protected Uri FoldersUri = new Uri(Constants.FoldersEndpointString);
        protected Uri FilesUploadUri = new Uri(Constants.FilesUploadEndpointString);
        protected Uri FilesUri = new Uri(Constants.FilesEndpointString);
        protected Uri MetadataQueryUri = new Uri(Constants.MetadataQueryEndpointString);
        protected Uri UserUri = new Uri(Constants.UserEndpointString);
        protected Uri InviteUri = new Uri(Constants.BoxApiUriString + Constants.InviteString);
        protected Uri FolderLocksUri = new Uri(Constants.FolderLocksEndpointString);
        protected Uri SignRequestUri = new Uri(Constants.SignRequestsEndpointString);
        protected Uri SignRequestWithPathUri = new Uri(Constants.SignRequestsWithPathEndpointString);
        protected Uri FileRequestsWithPathUri = new Uri(Constants.FileRequestsWithPathEndpointString);

        protected BoxResourceManagerTest()
        {
            // Initial Setup
            Converter = new BoxJsonConverter();
            Handler = new Mock<IRequestHandler>();
            Service = new BoxService(Handler.Object);
            Config = new Mock<IBoxConfig>();
            Config.SetupGet(x => x.CollaborationsEndpointUri).Returns(new Uri(Constants.CollaborationsEndpointString));
            Config.SetupGet(x => x.FoldersEndpointUri).Returns(FoldersUri);
            Config.SetupGet(x => x.FilesEndpointUri).Returns(FilesUri);
            Config.SetupGet(x => x.FilesUploadEndpointUri).Returns(FilesUploadUri);
            Config.SetupGet(x => x.MetadataQueryUri).Returns(MetadataQueryUri);
            Config.SetupGet(x => x.UserEndpointUri).Returns(UserUri);
            Config.SetupGet(x => x.InviteEndpointUri).Returns(InviteUri);
            Config.SetupGet(x => x.FolderLocksEndpointUri).Returns(FolderLocksUri);
            Config.SetupGet(x => x.SignRequestsEndpointUri).Returns(SignRequestUri);
            Config.SetupGet(x => x.SignRequestsEndpointWithPathUri).Returns(SignRequestWithPathUri);
            Config.SetupGet(x => x.SignTemplatesEndpointUri).Returns(new Uri(Constants.SignTemplatesEndpointString));
            Config.SetupGet(x => x.SignTemplatesEndpointWithPathUri).Returns(new Uri(Constants.SignTemplatesWithPathEndpointString));
            Config.SetupGet(x => x.FileRequestsEndpointWithPathUri).Returns(FileRequestsWithPathUri);
            Config.SetupGet(x => x.RetryStrategy).Returns(new InstantRetryStrategy());

            AuthRepository = new AuthRepository(Config.Object, Service, Converter, new OAuthSession("fakeAccessToken", "fakeRefreshToken", 3600, "bearer"));
        }

        public static bool AreJsonStringsEqual(string sourceJsonString, string targetJsonString)
        {
            JObject sourceJObject = JsonConvert.DeserializeObject<JObject>(sourceJsonString);
            JObject targetJObject = JsonConvert.DeserializeObject<JObject>(targetJsonString);

            return JToken.DeepEquals(sourceJObject, targetJObject);
        }

        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        public string LoadFixtureFromJson(string path)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            return File.ReadAllText(filePath);
        }
    }
}
