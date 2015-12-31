using MongoDB.Bson;
using Xunit;

namespace AspNet.Identity.MongoDB.Tests
{
    public class IdentityRoleTests
    {
        [Fact]
        public void ToBsonDocument_IdAssigned_MapsToBsonObjectId()
        {
            var role = new IdentityRole();
            role.SetId(ObjectId.GenerateNewId().ToString());

            var document = role.ToBsonDocument();

            Assert.IsType(typeof(BsonObjectId), document["_id"]);
        }

        [Fact]
        public void Create_WithoutRoleName_HasIdAssigned()
        {
            var role = new IdentityRole();

            var parsed = role.Id.SafeParseObjectId();

            Assert.NotNull(parsed);
            Assert.NotEqual(ObjectId.Empty, parsed);
        }

        [Fact]
        public void Create_WithRoleName_SetsName()
        {
            var name = "admin";

            var role = new IdentityRole(name);

            Assert.Equal(name, role.Name);
        }

        [Fact]
        public void Create_WithRoleName_SetsId()
        {
            var role = new IdentityRole("admin");

            var parsed = role.Id.SafeParseObjectId();
            Assert.NotNull(parsed);
            Assert.NotEqual(ObjectId.Empty, parsed);
        }

        [Fact]
        public void Create_NewIdentityUser_ClaimsNotNull()
        {
            var user = new IdentityUser();

            Assert.NotNull(user.Claims);
        }

        [Fact]
        public void Create_NullClaims_DoesNotSerializeClaims()
        {
            // serialized nulls can cause havoc in deserialization, overwriting the constructor's initial empty list 
            var user = new IdentityUser();
            user.Claims = null;

            var document = user.ToBsonDocument();

            Assert.False(document.Contains("Claims"));
        }
    }
}