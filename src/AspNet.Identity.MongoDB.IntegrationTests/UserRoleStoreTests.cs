namespace AspNet.Identity.MongoDB.IntegrationTests
{
	using System.Linq;
	using AspNet.Identity.MongoDB;
	using Microsoft.AspNet.Identity;
	using Xunit;

	
	public class UserRoleStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void GetRoles_UserHasNoRoles_ReturnsNoRoles()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);

			var roles = manager.GetRoles(user.Id);

			Expect(roles, Is.Empty);
		}

		[Fact]
		public async void AddRole_Adds()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);

			manager.AddToRole(user.Id, "role");

			var savedUser = Users.FindAll().Single();
			Expect(savedUser.Roles, Is.EquivalentTo(new[] {"role"}));
			Expect(manager.IsInRole(user.Id, "role"), Is.True);
		}

		[Fact]
		public async void RemoveRole_Removes()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);
			manager.AddToRole(user.Id, "role");

			manager.RemoveFromRole(user.Id, "role");

			var savedUser = Users.FindAll().Single();
			Expect(savedUser.Roles, Is.Empty);
			Expect(manager.IsInRole(user.Id, "role"), Is.False);
		}
	}
}