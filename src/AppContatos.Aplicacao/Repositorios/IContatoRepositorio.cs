using AppContatos.Aplicacao.Consultas;
using AppContatos.Aplicacao.Entidades;

namespace AppContatos.Aplicacao.Repositorios
{
    public interface IContatoRepositorio
    {
        Task<Contato> AdicionarAsync(Contato contato, CancellationToken cancellationToken);
        Task<ListaPaginada<Contato>> ListarPaginadoAsync(IConsultaPaginadaEspecificacao<Contato> consultaEspecificacao, CancellationToken cancellationToken);
        Task<Contato?> ObterPrimeiroAsync(IConsultaEspecificacao<Contato> consultaEspecificacao, CancellationToken cancellationToken);
        void Remover(Contato contato);
    }
}
