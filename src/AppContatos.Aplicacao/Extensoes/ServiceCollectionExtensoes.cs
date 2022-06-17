using AppContatos.Aplicacao.CasosDeUso;
using AppContatos.Aplicacao.CasosDeUso.CriarContato;
using AppContatos.Aplicacao.CasosDeUso.ExcluirContato;
using AppContatos.Aplicacao.CasosDeUso.InativarContato;
using AppContatos.Aplicacao.CasosDeUso.ListarContatos;
using AppContatos.Aplicacao.CasosDeUso.VerDetalhesContatos;
using AppContatos.Aplicacao.Consultas;
using AppContatos.Aplicacao.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppContatos.Aplicacao.Extensoes
{
    public static class ServiceCollectionExtensoes
    {
        public static IServiceCollection AdicionarCasosDeUso(this IServiceCollection services)
        {
            return services
                .AddScoped<ICasoDeUso<CriarContatoInput, CriarContatoOutput>, CriarContatoCasoDeUso>()
                .AddScoped<ICasoDeUso<InativarContatoInput, Unit>, InativarContatoCasoDeUso>()
                .AddScoped<ICasoDeUso<ExcluirContatoInput, Unit>, ExcluirContatoCasoDeUso>()
                .AddScoped<ICasoDeUso<ListarContatosInput, ListaPaginada<ListarContatosOutput>>, ListarContatosCasoDeUso>()
                .AddScoped<ICasoDeUso<VerDetalhesContatoInput, VerDetalhesContatoOutput>, VerDetalhesContatoCasoDeUso>()
                .AddSingleton<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
