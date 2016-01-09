using System.Linq;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Xunit;
using MongoDB.Driver;

namespace AspNet.Identity.MongoDB.IntegrationTests
{
    public class UserPasswordStoreTests : UserIntegrationTestsBase
    {
        [Fact]
        public async void HasPassword_NoPassword_ReturnsFalse()
        {


            var user = new IdentityUser { UserName = "bob" };
            var manager = GetUserManager();
            await manager.CreateAsync(user);

            var hasPassword = await manager.HasPasswordAsync(user);

            Assert.False(hasPassword);
        }

        [Fact]
        public async void AddPassword_NewPassword_CheckPasswordReturnsTrue()
        {
            var user = new IdentityUser { UserName = "bob" };
            var manager = GetUserManager();
            await manager.CreateAsync(user);

            await manager.AddPasswordAsync(user, "Test123@");

            bool passwordResult = await manager.CheckPasswordAsync(user, "Test123@");
            Assert.True(passwordResult);
        }

        [Fact]
        public async void RemovePassword_UserWithPassword_SetsPasswordNull()
        {
            var user = new IdentityUser { UserName = "bob" };
            var manager = GetUserManager();
            await manager.CreateAsync(user);
            await manager.AddPasswordAsync(user, "testtest");

            await manager.RemovePasswordAsync(user);

            var savedUser = await Users.Find(u => true).SingleAsync();
            Assert.Null(savedUser.PasswordHash);
        }
    }
}