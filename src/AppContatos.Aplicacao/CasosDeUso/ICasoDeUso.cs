using AppContatos.Aplicacao.Resultado;
namespace AppContatos.Aplicacao.CasosDeUso
{
    public interface ICasoDeUso<TInput, TOutput> where TInput : class where TOutput : class
    {
        Task<Resultado<TOutput>> ExecutarAsync(TInput input, CancellationToken cancellationToken);
    }
}
