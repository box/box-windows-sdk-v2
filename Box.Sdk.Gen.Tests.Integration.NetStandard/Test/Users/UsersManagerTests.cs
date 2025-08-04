using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class UsersManagerTests {
        public BoxClient client { get; }

        public UsersManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetUsers() {
            Users users = await client.Users.GetUsersAsync();
            Assert.IsTrue(NullableUtils.Unwrap(users.TotalCount) >= 0);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetUserMe() {
            UserFull currentUser = await client.Users.GetUserMeAsync();
            Assert.IsTrue(StringUtils.ToStringRepresentation(currentUser.Type?.Value) == "user");
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateUpdateGetDeleteUser() {
            string userName = Utils.GetUUID();
            string userLogin = string.Concat(Utils.GetUUID(), "@gmail.com");
            UserFull user = await client.Users.CreateUserAsync(requestBody: new CreateUserRequestBody(name: userName) { Login = userLogin, IsPlatformAccessOnly = true });
            Assert.IsTrue(user.Name == userName);
            UserFull userById = await client.Users.GetUserByIdAsync(userId: user.Id);
            Assert.IsTrue(userById.Id == user.Id);
            string updatedUserName = Utils.GetUUID();
            UserFull updatedUser = await client.Users.UpdateUserByIdAsync(userId: user.Id, requestBody: new UpdateUserByIdRequestBody() { Name = updatedUserName });
            Assert.IsTrue(updatedUser.Name == updatedUserName);
            await client.Users.DeleteUserByIdAsync(userId: user.Id);
        }

    }
}