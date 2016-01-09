using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet.Identity.MongoDB.IntegrationTests
{
    public interface ITestLogger
    {
        IList<string> Messages { get; }
    }

    public class TestLogger<TName> : ILogger<TName>, ITestLogger
    {
        public IList<string> Messages { get; } = new List<string>();

        public IDisposable BeginScopeImpl(object state)
        {
            Messages.Add(state?.ToString());
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            Messages.Add(formatter == null ? state.ToString() : formatter(state, exception));
        }
    }
}
