using AppContatos.Aplicacao.Consultas.Contatos;
using AppContatos.Aplicacao.Excecoes;
using AppContatos.Aplicacao.Resultado;
using Microsoft.Extensions.Logging;

namespace AppContatos.Aplicacao.CasosDeUso.ExcluirContato
{
    public class ExcluirContatoCasoDeUso : ICasoDeUso<ExcluirContatoInput, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExcluirContatoCasoDeUso> _logger;

        public ExcluirContatoCasoDeUso(IUnitOfWork unitOfWork, ILogger<ExcluirContatoCasoDeUso> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Resultado<Unit>> ExecutarAsync(ExcluirContatoInput input, CancellationToken cancellationToken)
        {
            try
            {
                var consulta = new ObterContatoPorId(input.Id)
                {
                    NoTracking = false,
                    Estado = null
                };
                var contato = await _unitOfWork.ContatoRepositorio
                    .ObterPrimeiroAsync(consulta, cancellationToken)
                    .ConfigureAwait(false);
                if (contato is null)
                {
                    var excecao = new EntidadeNaoExisteExcecao("Contato não existe");
                    return new Resultado<Unit>(excecao);
                }
                _unitOfWork.ContatoRepositorio.Remover(contato);
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
