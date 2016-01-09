using System.Linq;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Xunit;
using MongoDB.Driver;

namespace AspNet.Identity.MongoDB.IntegrationTests
{	
	public class UserRoleStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void GetRoles_UserHasNoRoles_ReturnsNoRoles()
		{
            

            var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			await manager.CreateAsync(user);

			var roles = await manager.GetRolesAsync(user);

		    Assert.Empty(roles);
		}

		[Fact]
		public async void AddRole_Adds()
		{
            

            var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			await manager.CreateAsync(user);

			await manager.AddToRoleAsync(user, "role");

			var savedUser = await Users.Find(u => true).SingleAsync();
		    Assert.Equal(1, savedUser.Roles.Count);
		    Assert.Equal("role", savedUser.Roles.ToList()[0]);
		    Assert.True(await manager.IsInRoleAsync(user, "role"));
		}

		[Fact]
		public async void RemoveRole_Removes()
		{
            

            var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			await manager.CreateAsync(user);
			await manager.AddToRoleAsync(user, "role");

			await manager.RemoveFromRoleAsync(user, "role");

			var savedUser = await Users.Find(_ => true).SingleAsync();
		    Assert.Empty(savedUser.Roles);
		    Assert.False(await manager.IsInRoleAsync(user, "roles"));
		}
	}
}