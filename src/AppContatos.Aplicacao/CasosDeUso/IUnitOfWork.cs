using AppContatos.Aplicacao.Repositorios;

namespace AppContatos.Aplicacao.CasosDeUso
{
    public interface IUnitOfWork
    {
        IContatoRepositorio ContatoRepositorio { get; }
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
