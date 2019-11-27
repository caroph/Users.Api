using Microsoft.Extensions.DependencyInjection;
using System;
using Users.Data.Repository;
using Users.Domain.Services;

namespace Users.Test
{
    public static class Container
    {
        public static IServiceProvider RegisterServices()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddTransient<IUserService, UserService>();
            collection.AddTransient<IUserRepository, UserRepositoryMemory>();
            return collection.BuildServiceProvider();
        }
    }
}
