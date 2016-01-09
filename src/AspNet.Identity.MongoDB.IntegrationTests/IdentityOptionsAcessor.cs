using Microsoft.AspNet.Identity;
using Microsoft.Extensions.OptionsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet.Identity.MongoDB.IntegrationTests
{
    internal class IdentityOptionsAcessor : IOptions<IdentityOptions>
    {
        public IdentityOptions Value { get; } = new IdentityOptions();
    }
}
