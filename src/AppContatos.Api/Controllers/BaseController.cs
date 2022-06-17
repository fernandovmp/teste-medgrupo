using AppContatos.Api.Extensoes;
using AppContatos.Aplicacao.Excecoes;
using AppContatos.Aplicacao.Resultado;
using Microsoft.AspNetCore.Mvc;

namespace AppContatos.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult RespostaResultado<T>(Resultado<T> resultado, Func<T?, ActionResult> resultadoSucesso)
        {
            if(resultado.Estado == ResultadoEstadoEnum.Sucesso)
            {
                return resultadoSucesso(resultado.Valor);
            }
            if (resultado.Excecao is ErroValidacaoExcecao erroValidacao)
            {
                return BadRequest(erroValidacao.MapearParaMensagemErro());
            }
            else if(resultado.Excecao is EntidadeNaoExisteExcecao entidadeNaoExiste)
            {
                return NotFound(entidadeNaoExiste.MapearParaMensagemErro());
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
