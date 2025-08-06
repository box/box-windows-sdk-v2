using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class EmailAliasesManagerTests {
        public BoxClient client { get; }

        public EmailAliasesManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestEmailAliases() {
            string newUserName = Utils.GetUUID();
            string newUserLogin = string.Concat(Utils.GetUUID(), "@boxdemo.com");
            UserFull newUser = await client.Users.CreateUserAsync(requestBody: new CreateUserRequestBody(name: newUserName) { Login = newUserLogin });
            EmailAliases aliases = await client.EmailAliases.GetUserEmailAliasesAsync(userId: newUser.Id);
            Assert.IsTrue(aliases.TotalCount == 0);
            string newAliasEmail = string.Concat(newUser.Id, "@boxdemo.com");
            EmailAlias newAlias = await client.EmailAliases.CreateUserEmailAliasAsync(userId: newUser.Id, requestBody: new CreateUserEmailAliasRequestBody(email: newAliasEmail));
            EmailAliases updatedAliases = await client.EmailAliases.GetUserEmailAliasesAsync(userId: newUser.Id);
            Assert.IsTrue(updatedAliases.TotalCount == 1);
            Assert.IsTrue(NullableUtils.Unwrap(updatedAliases.Entries)[0].Email == newAliasEmail);
            await client.EmailAliases.DeleteUserEmailAliasByIdAsync(userId: newUser.Id, emailAliasId: NullableUtils.Unwrap(newAlias.Id));
            EmailAliases finalAliases = await client.EmailAliases.GetUserEmailAliasesAsync(userId: newUser.Id);
            Assert.IsTrue(finalAliases.TotalCount == 0);
            await client.Users.DeleteUserByIdAsync(userId: newUser.Id);
        }

    }
}