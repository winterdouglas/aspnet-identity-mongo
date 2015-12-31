namespace AspNet.Identity.MongoDB.IntegrationTests
{
	using AspNet.Identity.MongoDB;
	using Microsoft.AspNet.Identity;
	using Xunit;

	
	public class UserTwoFactorStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void SetTwoFactorEnabled()
		{
			var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			manager.Create(user);

			manager.SetTwoFactorEnabled(user.Id, true);

			Expect(manager.GetTwoFactorEnabled(user.Id));
		}

		[Fact]
		public async void ClearTwoFactorEnabled_PreviouslyEnabled_NotEnabled()
		{
			var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			manager.Create(user);
			manager.SetTwoFactorEnabled(user.Id, true);

			manager.SetTwoFactorEnabled(user.Id, false);

			Expect(manager.GetTwoFactorEnabled(user.Id), Is.False);
		}
	}
}