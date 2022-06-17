using AppContatos.Aplicacao.CasosDeUso;
using AppContatos.Aplicacao.Repositorios;
using AppContatos.Infraestrutura.Contexto;
using AppContatos.Infraestrutura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppContatos.Infraestrutura.Extensoes
{
    public static class ServiceCollectionExtensoes
    {
        public static IServiceCollection AdicionarBancoDeDados(this IServiceCollection services, string connectionString)
        {
            return services
                .AddDbContext<ContatoContexto>(options => options.UseSqlServer(connectionString))
                .AddScoped<IContatoRepositorio, ContatoRepositorio>()
                .AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
