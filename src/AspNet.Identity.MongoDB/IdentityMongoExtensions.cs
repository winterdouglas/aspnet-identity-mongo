using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace AspNet.Identity.MongoDB
{
    public static class IdentityMongoExtensions
    {
        public static IdentityBuilder AddMongoStores<TContext>(this IdentityBuilder builder)
            where TContext : IdentityContextBase
        {
            builder.Services.TryAdd(GetDefaultServices(builder.UserType, builder.RoleType, typeof(TContext)));
            return builder;
        }

        private static IServiceCollection GetDefaultServices(Type userType, Type roleType, Type contextType)
        {
            var userStoreType = typeof(UserStore<,>).MakeGenericType(userType, contextType);
            var roleStoreType = typeof(RoleStore<,>).MakeGenericType(roleType, contextType);

            var services = new ServiceCollection();
            services.AddScoped(
                typeof(IUserStore<>).MakeGenericType(userType),
                userStoreType);
            services.AddScoped(
                typeof(IRoleStore<>).MakeGenericType(roleType),
                roleStoreType);
            services.AddScoped(contextType);
            return services;
        }
    }
}
