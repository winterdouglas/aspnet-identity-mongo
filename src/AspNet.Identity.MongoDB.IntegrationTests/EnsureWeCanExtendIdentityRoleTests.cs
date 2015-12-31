using System.Linq;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Xunit;
using MongoDB.Driver;

namespace AspNet.Identity.MongoDB.IntegrationTests
{	
	public class EnsureWeCanExtendIdentityRoleTests : UserIntegrationTestsBase
	{
		private RoleManager<ExtendedIdentityRole> _Manager;
		private ExtendedIdentityRole _Role;

		public class ExtendedIdentityRole : IdentityRole
		{
			public string ExtendedField { get; set; }
		}

		[SetUp]
		public void BeforeEachTestAfterBase()
		{
			var roles = Database.GetCollection<ExtendedIdentityRole>("roles");
			var roleStore = new RoleStore<ExtendedIdentityRole>();
			_Manager = new RoleManager<ExtendedIdentityRole>(roleStore);
			_Role = new ExtendedIdentityRole
			{
				Name = "admin"
			};
		}

		[Fact]
		public async void Create_ExtendedRoleType_SavesExtraFields()
		{
			_Role.ExtendedField = "extendedField";

			await _Manager.CreateAsync(_Role);

			var savedRole = await Roles.Find(_ => true).SingleAsync();
			Expect(savedRole.ExtendedField, Is.EqualTo("extendedField"));
		}

		[Fact]
		public async void Create_ExtendedRoleType_ReadsExtraFields()
		{
			_Role.ExtendedField = "extendedField";

			_Manager.Create(_Role);

			var savedRole = _Manager.FindById(_Role.Id);
			Expect(savedRole.ExtendedField, Is.EqualTo("extendedField"));
		}
	}
}