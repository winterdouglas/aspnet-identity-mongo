using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AspNet.Identity.MongoDB.Tests
{
    public class IdentityUserLoginTests
    {
        [Fact]
        public void Create_NewIdentityUserLogin_SetsProperties()
        {
            var loginProvider = "provider";
            var providerKey = "key";
            var displayName = "displayname";

            var userLogin = new IdentityUserLogin(loginProvider, providerKey, displayName);

            Assert.Equal(loginProvider, userLogin.LoginProvider);
            Assert.Equal(providerKey, userLogin.ProviderKey);
            Assert.Equal(displayName, userLogin.ProviderDisplayName);
        }
    }
}
