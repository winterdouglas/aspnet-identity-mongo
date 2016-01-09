using System.Linq;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using Xunit;
using MongoDB.Driver;

namespace AspNet.Identity.MongoDB.IntegrationTests
{	
	public class RoleStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void Create_NewRole_Saves()
		{
		    

            var roleName = "admin";
			var role = new IdentityRole(roleName);
			var manager = GetRoleManager();

			await manager.CreateAsync(role);

			var savedRole = await Roles.Find(_ => true).SingleAsync();
            Assert.Equal(roleName, savedRole.Name);
		}

		[Fact]
		public async void FindByName_SavedRole_ReturnsRole()
		{
            

            var roleName = "name";
			var role = new IdentityRole {Name = roleName};
			var manager = GetRoleManager();
			await manager.CreateAsync(role);

			var foundRole = await manager.FindByNameAsync(roleName);

            Assert.NotNull(foundRole);
            Assert.Equal(roleName, foundRole.Name);
		}

		[Fact]
		public async void FindById_SavedRole_ReturnsRole()
		{
            

            var role = new IdentityRole {Name = "name"};
			var manager = GetRoleManager();
			await manager.CreateAsync(role);

            Assert.NotNull(role.Id);

			var foundRole = await manager.FindByIdAsync(role.Id);

            Assert.NotNull(foundRole);
            Assert.Equal(role.Id, foundRole.Id);
		}

		[Fact]
		public async void Delete_ExistingRole_Removes()
		{
            

            var role = new IdentityRole {Name = "name"};
			var manager = GetRoleManager();
			await manager.CreateAsync(role);
			Assert.NotEmpty(await Roles.Find(_ => true).ToListAsync());

			await manager.DeleteAsync(role);

            Assert.Empty(await Roles.Find(_ => true).ToListAsync());
		}

		[Fact]
		public async void Update_ExistingRole_Updates()
		{
            

            var role = new IdentityRole {Name = "name"};
			var manager = GetRoleManager();
			await manager.CreateAsync(role);
			var savedRole = await manager.FindByIdAsync(role.Id);
			savedRole.Name = "newname";

			await manager.UpdateAsync(savedRole);

			var changedRole = await Roles.Find(_ => true).SingleAsync();
            Assert.NotNull(changedRole);
            Assert.Equal("newname", changedRole.Name);
		}
	}
}