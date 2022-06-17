using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppContatos.Aplicacao.Excecoes
{
    public class ErroValidacaoExcecao : Exception
    {
        public ErroValidacaoExcecao(IEnumerable<ValidationFailure> erros) : base("Ocorreu um ou mais erros de validação")
        {
            Erros = erros;
        }

        public IEnumerable<ValidationFailure> Erros { get; }
    }
}
