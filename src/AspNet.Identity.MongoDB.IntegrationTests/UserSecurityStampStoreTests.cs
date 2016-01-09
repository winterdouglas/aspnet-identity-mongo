using System.Linq;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Xunit;
using MongoDB.Driver;

namespace AspNet.Identity.MongoDB.IntegrationTests
{
	public class UserSecurityStampStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void Create_NewUser_HasSecurityStamp()
		{
            

            var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};

			await manager.CreateAsync(user);

			var savedUser = await Users.Find(u => true).SingleAsync();
			Assert.NotNull(savedUser.SecurityStamp);
		}

		[Fact]
		public async void GetSecurityStamp_NewUser_ReturnsStamp()
		{
            

            var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			await manager.CreateAsync(user);

			var stamp = await manager.GetSecurityStampAsync(user);

			Assert.NotNull(stamp);
		}
	}
}