namespace AspNet.Identity.MongoDB.IntegrationTests
{
	using System.Linq;
	using AspNet.Identity.MongoDB;
	using Microsoft.AspNet.Identity;
	using Xunit;

	
	public class UserSecurityStampStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void Create_NewUser_HasSecurityStamp()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};

			manager.Create(user);

			var savedUser = Users.FindAll().Single();
			Expect(savedUser.SecurityStamp, Is.Not.Null);
		}

		[Fact]
		public async void GetSecurityStamp_NewUser_ReturnsStamp()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);

			var stamp = manager.GetSecurityStamp(user.Id);

			Expect(stamp, Is.Not.Null);
		}
	}
}