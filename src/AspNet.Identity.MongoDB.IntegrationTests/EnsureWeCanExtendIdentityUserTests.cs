namespace AspNet.Identity.MongoDB.IntegrationTests
{
	using System.Linq;
	using AspNet.Identity.MongoDB;
	using Microsoft.AspNet.Identity;
	using Xunit;

	
	public class EnsureWeCanExtendIdentityUserTests : UserIntegrationTestsBase
	{
		private UserManager<ExtendedIdentityUser> _Manager;
		private ExtendedIdentityUser _User;

		public class ExtendedIdentityUser : IdentityUser
		{
			public string ExtendedField { get; set; }
		}

        public EnsureWeCanExtendIdentityUserTests()
        {

        }

		[SetUp]
		public async void BeforeEachTestAfterBase()
		{
            _Manager = GetUserManager();

			var users = Database.GetCollection<ExtendedIdentityUser>("users");
			var userStore = new UserStore<ExtendedIdentityUser>(users);
			_Manager = new UserManager<ExtendedIdentityUser>(userStore);
			_User = new ExtendedIdentityUser
			{
				UserName = "bob"
			};
		}

		[Fact]
		public async void Create_ExtendedUserType_SavesExtraFields()
		{
			_User.ExtendedField = "extendedField";

			_Manager.Create(_User);

			var savedUser = Users.FindAllAs<ExtendedIdentityUser>().Single();
			Expect(savedUser.ExtendedField, Is.EqualTo("extendedField"));
		}

		[Fact]
		public async void Create_ExtendedUserType_ReadsExtraFields()
		{
			_User.ExtendedField = "extendedField";

			_Manager.Create(_User);

			var savedUser = _Manager.FindById(_User.Id);
			Expect(savedUser.ExtendedField, Is.EqualTo("extendedField"));
		}
	}
}