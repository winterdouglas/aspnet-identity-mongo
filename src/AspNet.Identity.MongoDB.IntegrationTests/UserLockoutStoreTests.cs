﻿namespace AspNet.Identity.MongoDB.IntegrationTests
{
	using System;
	using AspNet.Identity.MongoDB;
	using Microsoft.AspNet.Identity;
	using Xunit;

	
	public class UserLockoutStoreTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void AccessFailed_IncrementsAccessFailedCount()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);
			manager.MaxFailedAccessAttemptsBeforeLockout = 3;

			manager.AccessFailed(user.Id);

			Expect(manager.GetAccessFailedCount(user.Id), Is.EqualTo(1));
		}

		[Fact]
		public async void IncrementAccessFailedCount_ReturnsNewCount()
		{
			var store = new UserStore<IdentityUser>(null);
			var user = new IdentityUser {UserName = "bob"};

			var count = store.IncrementAccessFailedCountAsync(user);

			Expect(count.Result, Is.EqualTo(1));
		}

		[Fact]
		public async void ResetAccessFailed_AfterAnAccessFailed_SetsToZero()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);
			manager.MaxFailedAccessAttemptsBeforeLockout = 3;
			manager.AccessFailed(user.Id);

			manager.ResetAccessFailedCount(user.Id);

			Expect(manager.GetAccessFailedCount(user.Id), Is.EqualTo(0));
		}

		[Fact]
		public async void AccessFailed_NotOverMaxFailures_NoLockoutEndDate()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);
			manager.MaxFailedAccessAttemptsBeforeLockout = 3;

			manager.AccessFailed(user.Id);

			Expect(manager.GetLockoutEndDate(user.Id), Is.EqualTo(DateTimeOffset.MinValue));
		}

		[Fact]
		public async void AccessFailed_ExceedsMaxFailedAccessAttempts_LocksAccount()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);
			manager.MaxFailedAccessAttemptsBeforeLockout = 0;
			manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromHours(1);

			manager.AccessFailed(user.Id);

			var lockoutEndDate = manager.GetLockoutEndDate(user.Id);
			Expect(lockoutEndDate.Subtract(DateTime.UtcNow).TotalHours, Is.GreaterThan(0.9).And.LessThan(1.1));
		}

		[Fact]
		public async void SetLockoutEnabled()
		{
			var manager = GetUserManager();
			var user = new IdentityUser {UserName = "bob"};
			manager.Create(user);

			manager.SetLockoutEnabled(user.Id, true);
			Expect(manager.GetLockoutEnabled(user.Id));

			manager.SetLockoutEnabled(user.Id, false);
			Expect(manager.GetLockoutEnabled(user.Id), Is.False);
		}
	}
}