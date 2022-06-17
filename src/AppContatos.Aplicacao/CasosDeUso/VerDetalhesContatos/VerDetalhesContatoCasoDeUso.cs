using AppContatos.Aplicacao.Consultas.Contatos;
using AppContatos.Aplicacao.Enums;
using AppContatos.Aplicacao.Excecoes;
using AppContatos.Aplicacao.Repositorios;
using AppContatos.Aplicacao.Resultado;
using AppContatos.Aplicacao.Services;
using Microsoft.Extensions.Logging;

namespace AppContatos.Aplicacao.CasosDeUso.VerDetalhesContatos
{
    public class VerDetalhesContatoCasoDeUso : ICasoDeUso<VerDetalhesContatoInput, VerDetalhesContatoOutput>
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ILogger<VerDetalhesContatoCasoDeUso> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public VerDetalhesContatoCasoDeUso(IContatoRepositorio contatoRepositorio, ILogger<VerDetalhesContatoCasoDeUso> logger, IDateTimeProvider dateTimeProvider)
        {
            _contatoRepositorio = contatoRepositorio;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Resultado<VerDetalhesContatoOutput>> ExecutarAsync(VerDetalhesContatoInput input, CancellationToken cancellationToken)
        {
            try
            {
                var consulta = new ObterContatoPorId(input.Id)
                {
                    Estado = EstadoEnum.Ativo
                };
                var contato = await _contatoRepositorio
                    .ObterPrimeiroAsync(consulta, cancellationToken)
                    .ConfigureAwait(false);
                if (contato is null)
                {
                    var excecao = new EntidadeNaoExisteExcecao("Contato não existe");
                    return new Resultado<VerDetalhesContatoOutput>(excecao);
                }
                var output = new VerDetalhesContatoOutput
                {
                    Id = contato.Id,
                    Idade = contato.ObterIdade(_dateTimeProvider),
                    Nome = contato.Nome,
                    Sexo = contato.Sexo.ToString()
                };
                return new Resultado<VerDetalhesContatoOutput>(output);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new Resultado<VerDetalhesContatoOutput>(e);
            }
        }
    }
}
