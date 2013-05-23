using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Box.V2.Services;
using Moq;
using Box.V2.Auth;
using Box.V2.Contracts;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxServiceTest
    {

        IBoxConverter _parser;
        Mock<IRequestHandler> _handler;
        IBoxService _service;
        Mock<IBoxConfig> _boxConfig;
        IAuthRepository _authRepository;

        public BoxServiceTest()
        {
            // Initial Setup
            _parser = new BoxJsonConverter();
            _handler = new Mock<IRequestHandler>();
            _service = new BoxService(_parser, _handler.Object);
            _boxConfig = new Mock<IBoxConfig>();

            OAuthSession session = new OAuthSession("fakeAccessToken", "fakeRefreshToken", 3600, "bearer");

            _authRepository = new AuthRepository(_boxConfig.Object, _service, session);
        }

        [TestMethod]
        public async Task QueueTask_MultipleThreads_OrderedResponse()
        {
            /*** Arrange ***/
            int numTasks = 1000;

            int count = 0;

            // Increments the access token each time a call is made to the API
            _handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<OAuthSession>>(new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"" + count + "\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                })).Callback(() => System.Threading.Interlocked.Increment(ref count));

            /*** Act ***/
            IBoxRequest request = new BoxRequest(new Uri("http://box.com"), "folders");

            List<Task<IBoxResponse<OAuthSession>>> tasks = new List<Task<IBoxResponse<OAuthSession>>>();
            for (int i = 0; i < numTasks; i++)
                tasks.Add(_service.EnqueueAsync<OAuthSession>(request));

            await Task.WhenAll(tasks);

            /*** Assert ***/
            for (int i = 0; i < numTasks; i++)
            {
                Assert.AreEqual(tasks[i].Result.ResponseObject.AccessToken, i.ToString());
            }
        }


        [TestMethod]
        public void JsonTest()
        {
            string retString = "{ \"my_name\":\"Brian\", \"nest\" : { \"blah_name\":\"hi\"} }";

            MyTest test = _parser.Parse<MyTest>(retString);
        }

    }
    public class MyTest
    {
        //[JsonConstructor]
        //public MyTest(string my_name, NestClass  another_class, NestClass nest)
        //{
        //    MyName = my_name;
        //    AnotherClass = another_class;
        //    Class = nest;
        //}
        
        [JsonProperty("my_name")]
        public string MyName { get; private set; }

        [JsonProperty("another_class")]
        public NestClass AnotherClass { get; private set; }

        [JsonProperty("nest")]
        public NestClass Class { get; private set; }
    }

    public class NestClass
    {
        [JsonConstructor]
        public NestClass(string blah_name)
        {
            Name = blah_name;
        }

        public string Name { get; private set; }
    }
}
