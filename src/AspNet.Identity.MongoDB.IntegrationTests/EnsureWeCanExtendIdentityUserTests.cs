using System.Linq;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Xunit;
using MongoDB.Driver;
using Microsoft.Extensions.OptionsModel;
using System.Threading.Tasks;
using System;

namespace AspNet.Identity.MongoDB.IntegrationTests
{
    public class ExtendedIdentityUser : IdentityUser
    {
        public string ExtendedField { get; set; }
    }

    public class ExtendedUserContext : IdentityContext<ExtendedIdentityUser>
    {
        public ExtendedUserContext() 
            : base(ConnectionSettings.ConnectionString, ConnectionSettings.DatabaseName + Guid.NewGuid())
        {
        }

        public new IMongoDatabase Database => base.Database;
        public new IMongoClient Client => base.Client;
        public string DatabaseName => Database.DatabaseNamespace.DatabaseName;
    }

    public class EnsureWeCanExtendIdentityUserTests : IDisposable
    {
        private readonly UserManager<ExtendedIdentityUser> _manager;
        private readonly ExtendedUserContext _context;

        public EnsureWeCanExtendIdentityUserTests()
        {
            _context = new ExtendedUserContext();
            _manager = GetUserManager(_context);

            Task.Run(Clear).Wait();
        }

        private async Task Clear()
        {
            await _context.Database.DropCollectionAsync(ConnectionSettings.UserCollectionName);
            await _context.Database.DropCollectionAsync(ConnectionSettings.RoleCollectionName);
        }

        protected UserManager<ExtendedIdentityUser> GetUserManager(ExtendedUserContext context, IOptions<IdentityOptions> options = null)
        {
            var store = new UserStore<ExtendedIdentityUser>(context);
            return new UserManager<ExtendedIdentityUser>(store,
                options ?? new IdentityOptionsAcessor(),
                new PasswordHasher<ExtendedIdentityUser>(),
                new[] { new UserValidator<ExtendedIdentityUser>() },
                new[] { new PasswordValidator<ExtendedIdentityUser>() },
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                null,
                new TestLogger<UserManager<ExtendedIdentityUser>>(),
                null);
        }

        [Fact]
        public async void Create_ExtendedUserType_SavesExtraFields()
        {
            var user = new ExtendedIdentityUser
            {
                UserName = "bob",
                ExtendedField = "extendedField"
            };
            await _manager.CreateAsync(user);

            var savedUser = await _context.Users.Find(u => true).SingleAsync();

            Assert.Equal("extendedField", savedUser.ExtendedField);
        }

        [Fact]
        public async void Create_ExtendedUserType_ReadsExtraFields()
        {
            var user = new ExtendedIdentityUser
            {
                UserName = "bob",
                ExtendedField = "extendedField"
            };

            await _manager.CreateAsync(user);

            var savedUser = await _manager.FindByIdAsync(user.Id);
            Assert.Equal("extendedField", savedUser.ExtendedField);
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