using AppContatos.Aplicacao.Consultas;
using AppContatos.Aplicacao.Entidades;
using AppContatos.Aplicacao.Repositorios;
using AppContatos.Infraestrutura.Contexto;
using Microsoft.EntityFrameworkCore;

namespace AppContatos.Infraestrutura.Repositorios
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly ContatoContexto _contexto;

        public ContatoRepositorio(ContatoContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Contato> AdicionarAsync(Contato contato, CancellationToken cancellationToken)
        {
            await _contexto.Contatos.AddAsync(contato, cancellationToken)
                .ConfigureAwait(false);
            return contato;
        }

        public async Task<ListaPaginada<Contato>> ListarPaginadoAsync(IConsultaPaginadaEspecificacao<Contato> consultaEspecificacao, CancellationToken cancellationToken)
        {
            int total = await consultaEspecificacao
                .AplicarFiltro(_contexto.Contatos.AsNoTracking())
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);
            IQueryable<Contato> query = _contexto.Contatos;
            if (consultaEspecificacao.NoTracking)
            {
                query = query.AsNoTracking();
            }
            IEnumerable<Contato> contatos = await consultaEspecificacao
                .AplicarConsulta(query)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            return new ListaPaginada<Contato>(
                consultaEspecificacao.Pagina,
                consultaEspecificacao.Tamanho,
                total,
                contatos);
        }

        public async Task<Contato?> ObterPrimeiroAsync(IConsultaEspecificacao<Contato> consultaEspecificacao, CancellationToken cancellationToken)
        {
            IQueryable<Contato> query = _contexto.Contatos;
            if (consultaEspecificacao.NoTracking)
            {
                query = query.AsNoTracking();
            }
            return await consultaEspecificacao
                .AplicarConsulta(query)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public void Remover(Contato contato)
        {
            _contexto.Contatos.Remove(contato);
        }
    }
}
