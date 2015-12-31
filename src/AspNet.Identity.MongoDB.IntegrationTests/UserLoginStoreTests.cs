﻿namespace AspNet.Identity.MongoDB.IntegrationTests
{
	using System.Linq;
	using AspNet.Identity.MongoDB;
	using Microsoft.AspNet.Identity;
	using Xunit;

	
	public class UserLoginStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void AddLogin_NewLogin_Adds()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);

			manager.AddLogin(user.Id, login);

			var savedLogin = Users.FindAll().Single().Logins.Single();
			Expect(savedLogin.LoginProvider, Is.EqualTo("provider"));
			Expect(savedLogin.ProviderKey, Is.EqualTo("key"));
		}


		[Fact]
		public async void RemoveLogin_NewLogin_Removes()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);
			manager.AddLogin(user.Id, login);

			manager.RemoveLogin(user.Id, login);

			var savedUser = Users.FindAll().Single();
			Expect(savedUser.Logins, Is.Empty);
		}

		[Fact]
		public async void GetLogins_OneLogin_ReturnsLogin()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);
			manager.AddLogin(user.Id, login);

			var logins = manager.GetLogins(user.Id);

			var savedLogin = logins.Single();
			Expect(savedLogin.LoginProvider, Is.EqualTo("provider"));
			Expect(savedLogin.ProviderKey, Is.EqualTo("key"));
		}

		[Fact]
		public async void Find_UserWithLogin_FindsUser()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);
			manager.AddLogin(user.Id, login);

			var findUser = manager.Find(login);

			Expect(findUser, Is.Not.Null);
		}

		[Fact]
		public async void Find_UserWithDifferentKey_DoesNotFindUser()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);
			manager.AddLogin(user.Id, login);

			var findUser = manager.Find(new UserLoginInfo("provider", "otherkey"));

			Expect(findUser, Is.Null);
		}

		[Fact]
		public async void Find_UserWithDifferentProvider_DoesNotFindUser()
		{
			var manager = GetUserManager();
			var login = new UserLoginInfo("provider", "key");
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);
			manager.AddLogin(user.Id, login);

			var findUser = manager.Find(new UserLoginInfo("otherprovider", "key"));

			Expect(findUser, Is.Null);
		}
	}
}