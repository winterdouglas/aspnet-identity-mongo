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
            

            var user = new IdentityUser { UserName = "bob" };
            var manager = GetUserManager();
            await manager.CreateAsync(user);

            await manager.SetTwoFactorEnabledAsync(user, true);

            Assert.True(await manager.GetTwoFactorEnabledAsync(user));
        }

        [Fact]
        public async void ClearTwoFactorEnabled_PreviouslyEnabled_NotEnabled()
        {
            

            var user = new IdentityUser { UserName = "bob" };
            var manager = GetUserManager();
            await manager.CreateAsync(user);
            await manager.SetTwoFactorEnabledAsync(user, true);

            await manager.SetTwoFactorEnabledAsync(user, false);

            Assert.False(await manager.GetTwoFactorEnabledAsync(user));
        }
    }
}