using Users.Data.Repository;
using Users.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using FluentMigrator.Runner;
using Users.Data.Migrations;
using Users.Infra.Options;

namespace Users.Api.Infra
{
    public static class IoCResolver
    {
        public static IServiceProvider CreateServiceProvider(IConfigurationRoot configuration, IServiceCollection services)
        {
            ResolveOptions(configuration, services);
            ResolveScoped(services);
            ResolveMigrations(services, configuration);

            return services.BuildServiceProvider();
        }

        private static void ResolveMigrations(IServiceCollection services, IConfigurationRoot configuration)
        {
            var connection = configuration.GetSection("AppSettings:PostgresConnection").Value;

            services.AddFluentMigratorCore();
            services.ConfigureRunner(rb => rb.AddPostgres().
                WithGlobalConnectionString(connection)
                .ScanIn(typeof(AddUserTable).Assembly).For.Migrations());

            services.AddLogging(lb => lb.AddFluentMigratorConsole());
        }

        static void ResolveScoped(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostgresRepository, PostgresRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        static void ResolveOptions(IConfigurationRoot configuration, IServiceCollection services)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        }
    }
}
