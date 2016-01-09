using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Xunit;

namespace AspNet.Identity.MongoDB.IntegrationTests
{
	public class UserEmailStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void Create_NewUser_HasNoEmail()
		{
            

            var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			await manager.CreateAsync(user);

			var email = await manager.GetEmailAsync(user);

		    Assert.Null(email);
		}

		[Fact]
		public async void SetEmail_SetsEmail()
		{
            

            var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			await manager.CreateAsync(user);

			await manager.SetEmailAsync(user, "email");

			Assert.Equal("email", await manager.GetEmailAsync(user));
		}

		[Fact]
		public async void FindUserByEmail_ReturnsUser()
		{
            

            var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			await manager.CreateAsync(user);
		    Assert.Null(await manager.FindByEmailAsync("email"));

		    await manager.SetEmailAsync(user, "email");

		    Assert.NotNull(manager.FindByEmailAsync("email"));
		}

		[Fact]
		public async void Create_NewUser_IsNotEmailConfirmed()
		{
            

            var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			await manager.CreateAsync(user);

			var isConfirmed = await manager.IsEmailConfirmedAsync(user);
		    Assert.False(isConfirmed);
		}

		[Fact]
		public async void SetEmailConfirmed_IsConfirmed()
		{
            

            var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			await manager.CreateAsync(user);
		    manager.RegisterTokenProvider("Default", new EmailTokenProvider<IdentityUser>());
			var token = await manager.GenerateEmailConfirmationTokenAsync(user);

			await manager.ConfirmEmailAsync(user, token);

			var isConfirmed = await manager.IsEmailConfirmedAsync(user);
		    Assert.True(isConfirmed);
		}

		[Fact]
		public async void ChangeEmail_AfterConfirmedOriginalEmail_NotEmailConfirmed()
		{
            

            var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			await manager.CreateAsync(user);
            manager.RegisterTokenProvider("Default", new EmailTokenProvider<IdentityUser>());
            var token = await manager.GenerateEmailConfirmationTokenAsync(user);
			await manager.ConfirmEmailAsync(user, token);

			await manager.SetEmailAsync(user, "new@email.com");

			var isConfirmed = await manager.IsEmailConfirmedAsync(user);
		    Assert.False(isConfirmed);
		}
	}
}