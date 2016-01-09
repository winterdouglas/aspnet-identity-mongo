namespace AspNet.Identity.MongoDB.IntegrationTests
{
	using System.Linq;
	using System.Security.Claims;
	using AspNet.Identity.MongoDB;
	using Microsoft.AspNet.Identity;
	using Xunit;

	
	public class UserClaimStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void Create_NewUser_HasNoClaims()
		{
            

            var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			await manager.CreateAsync(user);

		    var claims = await manager.GetClaimsAsync(user);
		    Assert.Empty(claims);
		}

		[Fact]
		public async void AddClaim_ReturnsClaim()
		{
            

            var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			await manager.CreateAsync(user);

			await manager.AddClaimAsync(user, new Claim("type", "value"));

			var claim = await manager.GetClaimsAsync(user);
            
			Assert.Equal("type", claim.Single().Type);
			Assert.Equal("value", claim.Single().Value);
		}

		[Fact]
		public async void RemoveClaim_RemovesExistingClaim()
		{
            

            var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			await manager.CreateAsync(user);
			await manager.AddClaimAsync(user, new Claim("type", "value"));

			await manager.RemoveClaimAsync(user, new Claim("type", "value"));

		    Assert.Empty(await manager.GetClaimsAsync(user));
		}

		[Fact]
		public async void RemoveClaim_DifferentType_DoesNotRemoveClaim()
		{
            

            var user = new IdentityUser { UserName = "bob" };
			var manager = GetUserManager();
			await manager.CreateAsync(user);
			await manager.AddClaimAsync(user, new Claim("type", "value"));

			await manager.RemoveClaimAsync(user, new Claim("otherType", "value"));

			Assert.NotEmpty(await manager.GetClaimsAsync(user));
		}

		[Fact]
		public async void RemoveClaim_DifferentValue_DoesNotRemoveClaim()
		{
            

            var user = new IdentityUser { UserName = "bob" };
			var manager = GetUserManager();
			await manager.CreateAsync(user);
			await manager.AddClaimAsync(user, new Claim("type", "value"));

			await manager.RemoveClaimAsync(user, new Claim("type", "otherValue"));

		    Assert.NotEmpty(await manager.GetClaimsAsync(user));
		}
	}
}