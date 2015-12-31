using MongoDB.Bson;
using Xunit;

namespace AspNet.Identity.MongoDB.Tests
{
    public class IdentityUserTests
	{
		[Fact]
		public void ToBsonDocument_IdAssigned_MapsToBsonObjectId()
		{
			var user = new IdentityUser();
			user.SetId(ObjectId.GenerateNewId().ToString());

			var document = user.ToBsonDocument();

            Assert.IsType(typeof(BsonObjectId), document["_id"]);
		}

		[Fact]
		public void Create_NewIdentityUser_HasIdAssigned()
		{
			var user = new IdentityUser();

			var parsed = user.Id.SafeParseObjectId();
            Assert.NotNull(parsed);
            Assert.NotEqual(ObjectId.Empty, parsed);
		}

		[Fact]
		public void Create_NoPassword_DoesNotSerializePasswordField()
		{
			// if a particular consuming application doesn't intend to use passwords, there's no reason to store a null entry except for padding concerns, if that is the case then the consumer may want to create a custom class map to serialize as desired.

			var user = new IdentityUser();

			var document = user.ToBsonDocument();

            Assert.False(document.Contains("PasswordHash"));
		}

		[Fact]
		public void Create_NewIdentityUser_RolesNotNull()
		{
			var user = new IdentityUser();

            Assert.NotNull(user.Roles);
		}

		[Fact]
		public void Create_NullRoles_DoesNotSerializeRoles()
		{
			// serialized nulls can cause havoc in deserialization, overwriting the constructor's initial empty list 
			var user = new IdentityUser();
			user.Roles = null;

			var document = user.ToBsonDocument();

            Assert.False(document.Contains("Roles"));
		}

		// todo consider if we want to not serialize the empty Roles array, also empty Logins array

		[Fact]
		public void Create_NewIdentityUser_LoginsNotNull()
		{
			var user = new IdentityUser();

            Assert.NotNull(user.Logins);
		}

		[Fact]
		public void Create_NullLogins_DoesNotSerializeLogins()
		{
			// serialized nulls can cause havoc in deserialization, overwriting the constructor's initial empty list 
			var user = new IdentityUser();
			user.Logins = null;

			var document = user.ToBsonDocument();

            Assert.False(document.Contains("Logins"));
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