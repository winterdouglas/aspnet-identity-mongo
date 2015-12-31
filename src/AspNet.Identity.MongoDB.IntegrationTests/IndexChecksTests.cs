using System.Linq;
using MongoDB.Driver;
using Xunit;

namespace AspNet.Identity.MongoDB.IntegrationTests
{	
	//public class IndexChecksTests : UserIntegrationTestsBase
	//{
	//	[Fact]
	//	public async void EnsureUniqueIndexOnUserName_NoIndexOnUserName_AddsUniqueIndexOnUserName()
	//	{
	//		var userCollectionName = "userindextest";
	//		await Database.DropCollectionAsync(userCollectionName);
	//		var usersNewApi = Database.GetCollection<IdentityUser>(userCollectionName);

	//		IndexChecks.EnsureUniqueIndexOnUserName(usersNewApi);

	//		var users = Database.GetCollection<IdentityUser>(userCollectionName);
 //           var index = users.Indexes
	//			.Where(i => i.IsUnique)
	//			.Where(i => i.Key.Count() == 1)
	//			.First(i => i.Key.Contains("UserName"));
 //           Assert.Equal(1, index.Key.Count());
	//	}

	//	[Fact]
	//	public async void EnsureEmailUniqueIndex_NoIndexOnEmail_AddsUniqueIndexOnEmail()
	//	{
	//		var userCollectionName = "userindextest";
	//		await Database.DropCollectionAsync(userCollectionName);

 //           var users = Database.GetCollection<IdentityUser>(userCollectionName);

 //           IndexChecks.EnsureUniqueIndexOnEmail(users);

	//		var index = users.Indexes
	//			.Where(i => i.IsUnique)
	//			.Where(i => i.Key.Count() == 1)
	//			.First(i => i.Key.Contains("Email"));
 //           Assert.Equal(1, index.Key.Count());
	//	}

	//	[Fact]
	//	public async void EnsureUniqueIndexOnRoleName_NoIndexOnRoleName_AddsUniqueIndexOnRoleName()
	//	{
	//		var roleCollectionName = "roleindextest";
	//		await Database.DropCollectionAsync(roleCollectionName);

 //           var roles = Database.GetCollection<IdentityRole>(roleCollectionName);

 //           IndexChecks.EnsureUniqueIndexOnRoleName(roles);
 //           var index = roles.Indexes
	//			.Where(i => i.IsUnique)
	//			.Where(i => i.Key.Count() == 1)
	//			.First(i => i.Key.Contains("Name"));
 //           Assert.Equal(1, index.Key.Count());
	//	}
	//}
}