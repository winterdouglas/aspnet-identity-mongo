using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using System.Threading.Tasks;
using Xunit;

namespace AspNet.Identity.MongoDB.IntegrationTests
{
    public static class ConnectionSettings
    {
        public static string ConnectionString = "mongodb://localhost:27017";
        public static string DatabaseName = "identity-testing";
        public static string UserCollectionName = "users";
        public static string RoleCollectionName = "roles";
    }

    public class UserTestContext : IdentityContext
    {
        public UserTestContext()
            : base(ConnectionSettings.ConnectionString,
                  ConnectionSettings.DatabaseName)
        {
        }

        public new IMongoDatabase Database { get { return base.Database; } }
    }

    public class UserIntegrationTestsBase
    {
        const string connectionString = "mongodb://localhost:27017";
        const string databaseName = "identity-testing";
        const string userCollectionName = "users";
        const string roleCollectionName = "roles";

        protected UserTestContext Context;
        protected IMongoDatabase Database;
        protected IMongoCollection<IdentityUser> Users;
        protected IMongoCollection<IdentityRole> Roles;


        public UserIntegrationTestsBase()
        {
            Task.Run(() => InitializeAsync()).Wait();
        }

        public async void InitializeAsync()
        {
            Context = new UserTestContext();

            Database = Context.Database;
            Users = Context.Users;
            Roles = Context.Roles;

            await Context.Database.DropCollectionAsync(userCollectionName);
            await Context.Database.DropCollectionAsync(roleCollectionName);
        }

        protected UserManager<IdentityUser> GetUserManager()
        {
            var store = new UserStore<IdentityUser>(Context);
            return new UserManager<IdentityUser>(store, null, null, null, null, null, null, null, null, null);
        }

        protected RoleManager<IdentityRole> GetRoleManager()
        {
            var store = new RoleStore<IdentityRole>(Context);
            return new RoleManager<IdentityRole>(store, null, null, null, null, null);
        }

        protected UserManager<TUser> GetUserManager<TUser>() where TUser : IdentityUser
        {
            var store = new UserStore<TUser>(Context);
            return new UserManager<IdentityUser>(store, null, null, null, null, null, null, null, null, null);
        }
    }
}