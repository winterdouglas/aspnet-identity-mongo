namespace AspNet.Identity.MongoDB.IntegrationTests
{
	using AspNet.Identity.MongoDB;
	using Microsoft.AspNet.Identity;
	using Xunit;

	
	public class UserPhoneNumberStoreTests : UserIntegrationTestsBase
	{
		private const string PhoneNumber = "1234567890";

		[Fact]
		public async void SetPhoneNumber_StoresPhoneNumber()
		{
            

            var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			await manager.CreateAsync(user);

			await manager.SetPhoneNumberAsync(user, PhoneNumber);

			Assert.Equal(PhoneNumber, await manager.GetPhoneNumberAsync(user));
		}

		[Fact]
		public async void ConfirmPhoneNumber_StoresPhoneNumberConfirmed()
		{
            

            var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			await manager.CreateAsync(user);
			var token = await manager.GenerateChangePhoneNumberTokenAsync(user, PhoneNumber);

			await manager.ChangePhoneNumberAsync(user, PhoneNumber, token);

			Assert.True(await manager.IsPhoneNumberConfirmedAsync(user));
		}

		[Fact]
		public async void ChangePhoneNumber_OriginalPhoneNumberWasConfirmed_NotPhoneNumberConfirmed()
		{
            

            var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			await manager.CreateAsync(user);
			var token = await manager.GenerateChangePhoneNumberTokenAsync(user, PhoneNumber);
			await manager.ChangePhoneNumberAsync(user, PhoneNumber, token);

			await manager.SetPhoneNumberAsync(user, PhoneNumber);

			Assert.False(await manager.IsPhoneNumberConfirmedAsync(user));
		}
	}
}