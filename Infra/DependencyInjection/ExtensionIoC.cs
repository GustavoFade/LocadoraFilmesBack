using LocadoraClientes.Infra.Data.Repositories;
using LocadoraFilmes.Application.Abstractions;
using LocadoraFilmes.Application.Mapper;
using LocadoraFilmes.Application.Services;
using LocadoraFilmes.Domain.Abstractions;
using LocadoraFilmes.Domain.Abstractions.Common;
using LocadoraFilmes.Domain.Abstractions.Security;
using LocadoraFilmes.Infra.Data.Repositories;
using LocadoraFilmes.Infra.Data.Repositories.Common;
using LocadoraFilmes.Infra.Data.Security;
using Microsoft.Extensions.DependencyInjection;

namespace LocadoraFilmes.Infra.IoC.DependencyInjection
{
    public static class ExtensionIoC
    {
        public static void AddSerives(this IServiceCollection services)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IFilmeRepository, FilmeRepository>();
            services.AddTransient<IGeneroRepository, GeneroRepository>();
            services.AddTransient<ILocacaoRepository, LocacaoRepository>();

            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IFilmeService, FilmeService>();
            services.AddTransient<IGeneroService, GeneroService>();

            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<ICryptographyPassword, CryptographyPassword>();

            services.AddAutoMapper(typeof(MapperConfig));
        }

    }
}
