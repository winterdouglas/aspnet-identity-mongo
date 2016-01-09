using System;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Xunit;
using MongoDB.Driver;

namespace AspNet.Identity.MongoDB.IntegrationTests
{
    public class UserLockoutStoreTests : UserIntegrationTestsBase
    {
        [Fact]
        public async void AccessFailed_IncrementsAccessFailedCount()
        {
            var options = new IdentityOptionsAcessor();
            options.Value.Lockout.MaxFailedAccessAttempts = 3;

            var manager = GetUserManager(options);
            var user = new IdentityUser { UserName = "bob" };
            await manager.CreateAsync(user);

            await manager.AccessFailedAsync(user);

            Assert.Equal(1, await manager.GetAccessFailedCountAsync(user));
        }

        [Fact]
        public async void IncrementAccessFailedCount_ReturnsNewCount()
        {
            var store = new UserStore<IdentityUser>(Context);
            var user = new IdentityUser { UserName = "bob" };

            var count = await store.IncrementAccessFailedCountAsync(user);

            Assert.Equal(1, count);
        }

        [Fact]
        public async void ResetAccessFailed_AfterAnAccessFailed_SetsToZero()
        {
            var options = new IdentityOptionsAcessor();
            options.Value.Lockout.MaxFailedAccessAttempts = 3;

            var manager = GetUserManager(options);
            var user = new IdentityUser { UserName = "bob" };
            await manager.CreateAsync(user);

            await manager.AccessFailedAsync(user);

            await manager.ResetAccessFailedCountAsync(user);

            Assert.Equal(0, await manager.GetAccessFailedCountAsync(user));
        }

        [Fact]
        public async void AccessFailed_NotOverMaxFailures_NoLockoutEndDate()
        {
            var options = new IdentityOptionsAcessor();
            options.Value.Lockout.MaxFailedAccessAttempts = 3;

            var manager = GetUserManager(options);
            var user = new IdentityUser { UserName = "bob" };
            await manager.CreateAsync(user);
            await manager.AccessFailedAsync(user);

            Assert.Null(await manager.GetLockoutEndDateAsync(user));
        }

        [Fact]
        public async void AccessFailed_ExceedsMaxFailedAccessAttempts_LocksAccount()
        {
            var options = new IdentityOptionsAcessor();
            options.Value.Lockout.MaxFailedAccessAttempts = 0;
            options.Value.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);

            var manager = GetUserManager(options);
            var user = new IdentityUser { UserName = "bob" };
            await manager.CreateAsync(user);

            await manager.AccessFailedAsync(user);

            var lockoutEndDate = await manager.GetLockoutEndDateAsync(user);
            Assert.InRange(lockoutEndDate.Value.Subtract(DateTime.UtcNow).TotalHours, 0.9, 1.1);
        }

        [Fact]
        public async void SetLockoutEnabled()
        {
            var manager = GetUserManager();
            var user = new IdentityUser { UserName = "bob" };
            await manager.CreateAsync(user);

            await manager.SetLockoutEnabledAsync(user, true);
            Assert.True(await manager.GetLockoutEnabledAsync(user));

            await manager.SetLockoutEnabledAsync(user, false);
            Assert.False(await manager.GetLockoutEnabledAsync(user));
        }
    }
}