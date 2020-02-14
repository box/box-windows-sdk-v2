using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Request;
using Box.V2.Services;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Reflection;

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
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
        public static T CreateInstanceNonPublicConstructor<T>()
        {
            Type[] pTypes = new Type[0];

            ConstructorInfo[] c = typeof(T).GetConstructors
                (BindingFlags.NonPublic | BindingFlags.Instance
                );

            T inst =
                (T)c[0].Invoke(BindingFlags.NonPublic,
                               null,
                               null,
                               System.Threading.Thread.CurrentThread.CurrentCulture);
            return inst;
        }
    }
}
