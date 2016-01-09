using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AspNet.Identity.MongoDB.IntegrationTests
{
    internal static class ConnectionSettings
    {
        public static string ConnectionString = "mongodb://localhost:27017";
        public static string DatabaseName = "identity-testing";
        public static string UserCollectionName = "users";
        public static string RoleCollectionName = "roles";
    }

    public class UserTestContext : IdentityContext
    {
        public UserTestContext()
            : base(ConnectionSettings.ConnectionString, ConnectionSettings.DatabaseName + Guid.NewGuid())
        {
        }

        public new IMongoDatabase Database => base.Database;
        public new IMongoClient Client => base.Client;
        public string DatabaseName => Database.DatabaseNamespace.DatabaseName;
    }

    public class UserIntegrationTestsBase : IDisposable
    {
        protected UserTestContext Context;
        protected IMongoDatabase Database;
        protected IMongoCollection<IdentityUser> Users;
        protected IMongoCollection<IdentityRole> Roles;

        public UserIntegrationTestsBase()
        {
            Task.Run(Initialize).Wait();
        }

        public async Task Initialize()
        {
            Context = new UserTestContext();
            Database = Context.Database;
            Users = Context.Users;
            Roles = Context.Roles;

            await Context.Database.DropCollectionAsync(ConnectionSettings.UserCollectionName);
            await Context.Database.DropCollectionAsync(ConnectionSettings.RoleCollectionName);
        }

        protected UserManager<IdentityUser> GetUserManager(IOptions<IdentityOptions> options = null)
        {
            var store = new UserStore<IdentityUser>(Context);
            return new UserManager<IdentityUser>(store,
                options ?? new IdentityOptionsAcessor(),
                new PasswordHasher<IdentityUser>(),
                new[] { new UserValidator<IdentityUser>() },
                new[] { new PasswordValidator<IdentityUser>() },
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                null,
                new TestLogger<UserManager<IdentityUser>>(),
                null);
        }

        protected RoleManager<IdentityRole> GetRoleManager()
        {
            var store = new RoleStore<IdentityRole>(Context);
            return new RoleManager<IdentityRole>(store, new[] { new RoleValidator<IdentityRole>() }, null, null, new TestLogger<RoleManager<IdentityRole>>(), null);
        }

        public async void Dispose()
        {
            if (Context != null)
            {
                await Context.Client.DropDatabaseAsync(Context.DatabaseName);
            }
        }
    }
}