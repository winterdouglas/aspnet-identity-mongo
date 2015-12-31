using MongoDB.Driver;
using System.Linq;

namespace AspNet.Identity.MongoDB
{
    public class IdentityContext : IdentityContext<IdentityUser, IdentityRole>
    {
        public IdentityContext(string connectionString, string databaseName)
            : base(connectionString, databaseName)
        {
        }
    }

    public class IdentityContext<TUser> : IdentityContext<TUser, IdentityRole>
        where TUser : IdentityUser
    {
        public IdentityContext(string connectionString, string databaseName)
            : base(connectionString, databaseName)
        {
        }
    }

    public class IdentityContext<TUser, TRole> : IdentityContextBase
        where TUser : IdentityUser
        where TRole : IdentityRole
    {
        public IdentityContext(string connectionString, string databaseName)
            : base(connectionString, databaseName)
        {
        }

        public IMongoCollection<TUser> Users
        {
            get
            {
                return GetUsers<TUser>();
            }
        }

        public IMongoCollection<TRole> Roles
        {
            get
            {
                return GetRoles<TRole>();
            }
        }
    }

    public abstract class IdentityContextBase
    {
        protected readonly string _userCollectionName = "users";
        protected readonly string _roleCollectionName = "roles";

        protected virtual IMongoClient Client { get; }
        protected virtual IMongoDatabase Database { get; }

        public IdentityContextBase(string connectionString, string databaseName, string userCollectionName, string roleCollectionName)
            : this(connectionString, databaseName)
        {
            _userCollectionName = userCollectionName;
            _roleCollectionName = roleCollectionName;
        }

        public IdentityContextBase(string connectionString, string databaseName)
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
        }

        public IMongoCollection<TDocument> GetUsers<TDocument>()
            where TDocument : IdentityUser
        {
            return Database.GetCollection<TDocument>(_userCollectionName);
        }

        public IMongoCollection<TDocument> GetRoles<TDocument>()
            where TDocument : IdentityRole
        {
            return Database.GetCollection<TDocument>(_roleCollectionName);
        }
    }
}
