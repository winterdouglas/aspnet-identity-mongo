using System.Security.Claims;
using Xunit;

namespace AspNet.Identity.MongoDB.Tests
{
    public class IdentityRoleClaimTests
    {
        [Fact]
        public void Create_FromClaim_SetsTypeAndValue()
        {
            var claim = new Claim("type", "value");

            var userClaim = new IdentityRoleClaim(claim.Type, claim.Value);

            Assert.Equal("type", userClaim.Type);
            Assert.Equal("value", userClaim.Value);
        }
    }
}
