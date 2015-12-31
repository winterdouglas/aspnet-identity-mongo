namespace AspNet.Identity.MongoDB.IntegrationTests
{
    using System.Linq;
    using AspNet.Identity.MongoDB;
    using Microsoft.AspNet.Identity;
    using MongoDB.Bson;
    using Xunit;
    using Tests;
    using global::MongoDB.Bson;

    public class UserStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void Create_NewUser_Saves()
		{
			var userName = "name";
			var user = new IdentityUser {UserName = userName};
			var manager = GetUserManager();

			await manager.CreateAsync(user);

			var savedUser = Users.FindAll().Single();
            Assert.Equal(user.UserName, savedUser.UserName);
		}

		[Fact]
		public async void FindByName_SavedUser_ReturnsUser()
		{
			var userName = "name";
			var user = new IdentityUser {UserName = userName};
			var manager = GetUserManager();
			await manager.CreateAsync(user);

			var foundUser = await manager.FindByNameAsync(userName);

            Assert.NotNull(foundUser);
            Assert.Equal(userName, foundUser.UserName);
		}

		[Fact]
		public async void FindByName_NoUser_ReturnsNull()
		{
			var manager = GetUserManager();

			var foundUser = await manager.FindByNameAsync("nouserbyname");

            Assert.Null(foundUser);
		}

		[Fact]
		public async void FindById_SavedUser_ReturnsUser()
		{
			var user = new IdentityUser {UserName = "name"};
            var userId = user.Id;

			var manager = GetUserManager();
			await manager.CreateAsync(user);

			var foundUser = await manager.FindByIdAsync(userId);

            Assert.NotNull(foundUser);
            Assert.Equal(userId, foundUser.Id);
		}

		[Fact]
		public async void FindById_NoUser_ReturnsNull()
		{
			var manager = GetUserManager();

			var foundUser = await manager.FindByIdAsync(ObjectId.GenerateNewId().ToString());

            Assert.Null(foundUser);
		}

		[Fact]
		public async void Delete_ExistingUser_Removes()
		{
			var user = new IdentityUser {UserName = "name"};
			var manager = GetUserManager();
			await manager.CreateAsync(user);

            Assert.NotEmpty(Users.FindAll());

			await manager.DeleteAsync(user);

            Assert.Empty(Users.FindAll());
		}

		[Fact]
		public async void Update_ExistingUser_Updates()
		{
			var user = new IdentityUser {UserName = "name"};
			var manager = GetUserManager();
			await manager.CreateAsync(user);
			var savedUser = await manager.FindByIdAsync(user.Id);
			savedUser.UserName = "newname";

			await manager.UpdateAsync(savedUser);

			var changedUser = Users.FindAll().Single();
            Assert.NotNull(changedUser);
            Assert.Equal("newname", changedUser.UserName);
		}
	}
}