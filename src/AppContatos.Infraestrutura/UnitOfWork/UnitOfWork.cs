using AppContatos.Aplicacao.CasosDeUso;
using AppContatos.Aplicacao.Repositorios;
using AppContatos.Infraestrutura.Contexto;
using AppContatos.Infraestrutura.Repositorios;

namespace AppContatos.Infraestrutura.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContatoContexto _contexto;

        public UnitOfWork(ContatoContexto contexto)
        {
            _contexto = contexto;
            ContatoRepositorio = new ContatoRepositorio(_contexto);
        }

        public IContatoRepositorio ContatoRepositorio { get; }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _contexto.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
