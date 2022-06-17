using AppContatos.Aplicacao.Entidades;
using AppContatos.Aplicacao.Enums;
using AppContatos.Aplicacao.Excecoes;
using AppContatos.Aplicacao.Resultado;
using AppContatos.Aplicacao.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppContatos.Aplicacao.CasosDeUso.CriarContato
{
    public class CriarContatoCasoDeUso : ICasoDeUso<CriarContatoInput, CriarContatoOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CriarContatoCasoDeUso> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CriarContatoCasoDeUso(IUnitOfWork unitOfWork, ILogger<CriarContatoCasoDeUso> logger, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Resultado<CriarContatoOutput>> ExecutarAsync(CriarContatoInput input, CancellationToken cancellationToken)
        {
            try
            {
                var resultadoValidacao = new CriarContatoValidacao(_dateTimeProvider)
                    .Validate(input);
                if(!resultadoValidacao.IsValid)
                {
                    var excecao = new ErroValidacaoExcecao(resultadoValidacao.Errors);
                    return new Resultado<CriarContatoOutput>(excecao);
                }
                var contato = new Contato
                {
                    Id = Guid.Empty,
                    Nome = input.Nome,
                    DataNascimento = input.DataNascimento,
                    Estado = EstadoEnum.Ativo,
                    Sexo = input.Sexo
                };
                await _unitOfWork.ContatoRepositorio.AdicionarAsync(contato, cancellationToken)
                    .ConfigureAwait(false);
                var output = new CriarContatoOutput
                {
                    Id = contato.Id,
                    Nome = contato.Nome,
                    Sexo = contato.Sexo,
                    Idade = contato.ObterIdade(_dateTimeProvider)
                };
                await _unitOfWork.CommitAsync(cancellationToken)
                    .ConfigureAwait(false);
                _logger.LogInformation($"Novo contato criado {contato.Id}");
                return new Resultado<CriarContatoOutput>(output);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return new Resultado<CriarContatoOutput>(e);
            }
        }
    }
}
