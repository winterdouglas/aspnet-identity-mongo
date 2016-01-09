using System.Linq;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Xunit;
using MongoDB.Driver;

namespace AspNet.Identity.MongoDB.IntegrationTests
{
    public class UserLoginStoreTests : UserIntegrationTestsBase
    {
        [Fact]
        public async void AddLogin_NewLogin_Adds()
        {
            var manager = GetUserManager();
            var login = new UserLoginInfo("provider", "key", "display");
            var user = new IdentityUser { UserName = "bob" };
            await manager.CreateAsync(user);

            await manager.AddLoginAsync(user, login);

            var foundUser = await Users.Find(u => true).SingleAsync();
            var foundLogin = user.Logins.Single();

            Assert.Equal("provider", foundLogin.LoginProvider);
            Assert.Equal("key", foundLogin.ProviderKey);
            Assert.Equal("display", foundLogin.ProviderDisplayName);
        }


        [Fact]
        public async void RemoveLogin_NewLogin_Removes()
        {
            var manager = GetUserManager();
            var login = new UserLoginInfo("provider", "key", "display");
            var user = new IdentityUser { UserName = "bob" };
            await manager.CreateAsync(user);
            await manager.AddLoginAsync(user, login);

            await manager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);

            var savedUser = await Users.Find(u => true).SingleAsync();
            Assert.Empty(savedUser.Logins);
        }

        [Fact]
        public async void GetLogins_OneLogin_ReturnsLogin()
        {
            var manager = GetUserManager();
            var login = new UserLoginInfo("provider", "key", "display");
            var user = new IdentityUser { UserName = "bob" };
            await manager.CreateAsync(user);
            await manager.AddLoginAsync(user, login);

            var logins = await manager.GetLoginsAsync(user);
            var savedLogin = logins.Single();

            Assert.Equal("provider", savedLogin.LoginProvider);
            Assert.Equal("key", savedLogin.ProviderKey);
            Assert.Equal("display", savedLogin.ProviderDisplayName);
        }

        [Fact]
        public async void Find_UserWithLogin_FindsUser()
        {
            var manager = GetUserManager();
            var login = new UserLoginInfo("provider", "key", "display");
            var user = new IdentityUser { UserName = "bob" };
            await manager.CreateAsync(user);
            await manager.AddLoginAsync(user, login);

            var foundUser = manager.FindByLoginAsync(login.LoginProvider, login.ProviderKey);

            Assert.NotNull(foundUser);
        }

        [Fact]
        public async void Find_UserWithDifferentKey_DoesNotFindUser()
        {
            var manager = GetUserManager();
            var login = new UserLoginInfo("provider", "key", "display");
            var user = new IdentityUser { UserName = "bob" };
            await manager.CreateAsync(user);
            await manager.AddLoginAsync(user, login);

            var foundUser = await manager.FindByLoginAsync(login.LoginProvider, "anotherkey");

            Assert.Null(foundUser);
        }

        [Fact]
        public async void Find_UserWithDifferentProvider_DoesNotFindUser()
        {
            var manager = GetUserManager();
            var login = new UserLoginInfo("provider", "key", "display");
            var user = new IdentityUser { UserName = "bob" };
            await manager.CreateAsync(user);
            await manager.AddLoginAsync(user, login);

            var foundUser = await manager.FindByLoginAsync("otherprovider", login.ProviderKey);

            Assert.Null(foundUser);
        }
    }
}