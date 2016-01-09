using System.Linq;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Xunit;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace AspNet.Identity.MongoDB.IntegrationTests
{

    public class ExtendedIdentityRole : IdentityRole
    {
        public string ExtendedField { get; set; }
    }

    public class ExtendedRoleContext : IdentityContext<IdentityUser, ExtendedIdentityRole>
    {
        public ExtendedRoleContext()
            : base(ConnectionSettings.ConnectionString, ConnectionSettings.DatabaseName + Guid.NewGuid())
        {
        }

        public new IMongoDatabase Database => base.Database;
        public new IMongoClient Client => base.Client;
        public string DatabaseName => Database.DatabaseNamespace.DatabaseName;
    }

    public class EnsureWeCanExtendIdentityRoleTests : IDisposable
    {
        private readonly RoleManager<ExtendedIdentityRole> _manager;
        private readonly ExtendedRoleContext _context;

        public EnsureWeCanExtendIdentityRoleTests()
        {
            _context = new ExtendedRoleContext();
            _manager = GetRoleManager(_context);

            Task.Run(Clear).Wait();
        }

        private async Task Clear()
        {
            await _context.Database.DropCollectionAsync(ConnectionSettings.UserCollectionName);
            await _context.Database.DropCollectionAsync(ConnectionSettings.RoleCollectionName);
        }

        protected RoleManager<ExtendedIdentityRole> GetRoleManager(ExtendedRoleContext context)
        {
            var store = new RoleStore<ExtendedIdentityRole>(context);
            return new RoleManager<ExtendedIdentityRole>(store, new[] { new RoleValidator<ExtendedIdentityRole>() }, null, null, new TestLogger<RoleManager<ExtendedIdentityRole>>(), null);
        }

        [Fact]
        public async void Create_ExtendedRoleType_SavesExtraFields()
        {
            var role = new ExtendedIdentityRole
            {
                Name = "admin",
                ExtendedField = "extendedField"
            };

            await _manager.CreateAsync(role);

            var savedRole = await _manager.FindByIdAsync(role.Id);
            Assert.Equal("extendedField", savedRole.ExtendedField);
        }

        [Fact]
        public async void Create_ExtendedRoleType_ReadsExtraFields()
        {
            var role = new ExtendedIdentityRole
            {
                Name = "admin",
                ExtendedField = "extendedField"
            };

            await _manager.CreateAsync(role);

            var savedRole = await _manager.FindByIdAsync(role.Id);
            Assert.Equal("extendedField", savedRole.ExtendedField);
        }

        public async void Dispose()
        {
            if (_context != null)
            {
                await _context.Client.DropDatabaseAsync(_context.DatabaseName);
            }
        }
    }
}