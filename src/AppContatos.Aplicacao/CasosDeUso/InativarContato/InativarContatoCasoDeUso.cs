using AppContatos.Aplicacao.Consultas.Contatos;
using AppContatos.Aplicacao.Enums;
using AppContatos.Aplicacao.Excecoes;
using AppContatos.Aplicacao.Resultado;
using AppContatos.Aplicacao.Services;
using Microsoft.Extensions.Logging;

namespace AppContatos.Aplicacao.CasosDeUso.InativarContato
{
    public class InativarContatoCasoDeUso : ICasoDeUso<InativarContatoInput, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<InativarContatoCasoDeUso> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public InativarContatoCasoDeUso(IUnitOfWork unitOfWork, ILogger<InativarContatoCasoDeUso> logger, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Resultado<Unit>> ExecutarAsync(InativarContatoInput input, CancellationToken cancellationToken)
        {
            try
            {
                var consulta = new ObterContatoPorId(input.Id)
                {
                    NoTracking = false,
                    Estado = EstadoEnum.Ativo
                };
                var contato = await _unitOfWork.ContatoRepositorio
                    .ObterPrimeiroAsync(consulta, cancellationToken)
                    .ConfigureAwait(false);
                if(contato is null)
                {
                    var excecao = new EntidadeNaoExisteExcecao("Contato não existe");
                    return new Resultado<Unit>(excecao);
                }
                contato.Estado = EstadoEnum.Inativo;
                contato.DataAtualizacao = _dateTimeProvider.Now();
                await _unitOfWork.CommitAsync(cancellationToken)
                    .ConfigureAwait(false);
                return new Resultado<Unit>(Unit.Padrao);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new Resultado<Unit>(e);
            }
        }
    }
}
