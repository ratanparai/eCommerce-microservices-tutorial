using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebMVC.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDbContext<TContext>(this IHost host)
            where TContext : DbContext
        {
            // any scoped services that have been resolved from ServiceProvider will be disposed.
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            
            var context = services.GetService<TContext>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);
                context.Database.Migrate();
                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while migrating the database used on context {DbContextName}", typeof(TContext).Name);
            }

            return host;
        }
    }
}