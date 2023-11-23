using LocadoraFilmes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocadoraFilmes.Infra.IoC.DependencyInjection
{
    public static class ExtensionDatabase
    {
        public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    options.UseSqlServer(connectionString);
                }
            });
        }
    }
}
