namespace AspNet.Identity.MongoDB.IntegrationTests
{
	using System.Linq;
	using AspNet.Identity.MongoDB;
	using Microsoft.AspNet.Identity;
	using Xunit;

	
	public class UserPasswordStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void HasPassword_NoPassword_ReturnsFalse()
		{
			var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			manager.Create(user);

			var hasPassword = manager.HasPassword(user.Id);

			Expect(hasPassword, Is.False);
		}

		[Fact]
		public async void AddPassword_NewPassword_CanFindUserByPassword()
		{
			var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			manager.Create(user);

			manager.AddPassword(user.Id, "testtest");

			var findUserByPassword = manager.Find("bob", "testtest");
			Expect(findUserByPassword, Is.Not.Null);
		}

		[Fact]
		public async void RemovePassword_UserWithPassword_SetsPasswordNull()
		{
			var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			manager.Create(user);
			manager.AddPassword(user.Id, "testtest");

			manager.RemovePassword(user.Id);

			var savedUser = Users.FindAll().Single();
			Expect(savedUser.PasswordHash, Is.Null);
		}
	}
}