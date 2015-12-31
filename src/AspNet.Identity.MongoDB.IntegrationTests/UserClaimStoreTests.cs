﻿namespace AspNet.Identity.MongoDB.IntegrationTests
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
			manager.Create(user);

			var claims = manager.GetClaims(user.Id);

			Expect(claims, Is.Empty);
		}

		[Fact]
		public async void AddClaim_ReturnsClaim()
		{
			var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			manager.Create(user);

			manager.AddClaim(user.Id, new Claim("type", "value"));

			var claim = manager.GetClaims(user.Id).Single();
			Expect(claim.Type, Is.EqualTo("type"));
			Expect(claim.Value, Is.EqualTo("value"));
		}

		[Fact]
		public async void RemoveClaim_RemovesExistingClaim()
		{
			var user = new IdentityUser {UserName = "bob"};
			var manager = GetUserManager();
			manager.Create(user);
			manager.AddClaim(user.Id, new Claim("type", "value"));

			manager.RemoveClaim(user.Id, new Claim("type", "value"));

			Expect(manager.GetClaims(user.Id), Is.Empty);
		}

		[Fact]
		public async void RemoveClaim_DifferentType_DoesNotRemoveClaim()
		{
			var user = new IdentityUser { UserName = "bob" };
			var manager = GetUserManager();
			manager.Create(user);
			manager.AddClaim(user.Id, new Claim("type", "value"));

			manager.RemoveClaim(user.Id, new Claim("otherType", "value"));

			Expect(manager.GetClaims(user.Id), Is.Not.Empty);
		}

		[Fact]
		public async void RemoveClaim_DifferentValue_DoesNotRemoveClaim()
		{
			var user = new IdentityUser { UserName = "bob" };
			var manager = GetUserManager();
			manager.Create(user);
			manager.AddClaim(user.Id, new Claim("type", "value"));

			manager.RemoveClaim(user.Id, new Claim("type", "otherValue"));

			Expect(manager.GetClaims(user.Id), Is.Not.Empty);
		}
	}
}