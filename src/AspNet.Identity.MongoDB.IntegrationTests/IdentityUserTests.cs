using AspNet.Identity.MongoDB.Tests;
using MongoDB.Bson;
using Xunit;

namespace AspNet.Identity.MongoDB.IntegrationTests
{
    public class IdentityUserTests : UserIntegrationTestsBase
	{
		[Fact]
		public async void Insert_NoId_SetsId()
		{
			var user = new IdentityUser();
			await Users.InsertOneAsync(user);

            Assert.NotNull(user.Id);
            var parsed = user.Id.SafeParseObjectId();

            Assert.NotNull(parsed);
            Assert.NotEqual(parsed, ObjectId.Empty);
		}
	}
}