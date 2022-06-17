using AppContatos.Aplicacao.Consultas;
using AppContatos.Aplicacao.Consultas.Contatos;
using AppContatos.Aplicacao.Repositorios;
using AppContatos.Aplicacao.Resultado;
using AppContatos.Aplicacao.Services;
using Microsoft.Extensions.Logging;

namespace AppContatos.Aplicacao.CasosDeUso.ListarContatos
{
    public class ListarContatosCasoDeUso : ICasoDeUso<ListarContatosInput, ListaPaginada<ListarContatosOutput>>
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ILogger<ListarContatosCasoDeUso> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ListarContatosCasoDeUso(IContatoRepositorio contatoRepositorio, ILogger<ListarContatosCasoDeUso> logger, IDateTimeProvider dateTimeProvider)
        {
            _contatoRepositorio = contatoRepositorio;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Resultado<ListaPaginada<ListarContatosOutput>>> ExecutarAsync(ListarContatosInput input, CancellationToken cancellationToken)
        {
            try
            {
                var consulta = new ListarContatosPaginados(input.Pagina, input.Tamanho)
                {
                    Sexo = input.Sexo
                };
                var lista = await _contatoRepositorio.ListarPaginadoAsync(consulta, cancellationToken)
                    .ConfigureAwait(false);
                var novaLista = lista.Projetar(c => new ListarContatosOutput
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Idade = c.ObterIdade(_dateTimeProvider),
                    Sexo = c.Sexo.ToString()
                });
                return new Resultado<ListaPaginada<ListarContatosOutput>>(novaLista);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new Resultado<ListaPaginada<ListarContatosOutput>>(e);
            }
        }
    }
}
