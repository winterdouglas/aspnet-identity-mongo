using MongoDB.Driver;

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

        public virtual IMongoCollection<TUser> Users => GetUsers<TUser>();

        public virtual IMongoCollection<TRole> Roles => GetRoles<TRole>();
    }

    public abstract class IdentityContextBase
    {
        private readonly string _userCollectionName = "users";
        private readonly string _roleCollectionName = "roles";

        protected IMongoClient Client { get; }
        protected IMongoDatabase Database { get; }

        protected IdentityContextBase(string connectionString, string databaseName, string userCollectionName, string roleCollectionName)
            : this(connectionString, databaseName)
        {
            _userCollectionName = userCollectionName;
            _roleCollectionName = roleCollectionName;
        }

        protected IdentityContextBase(string connectionString, string databaseName)
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
