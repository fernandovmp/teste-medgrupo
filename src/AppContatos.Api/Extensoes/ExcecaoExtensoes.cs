using AppContatos.Api.Model;
using AppContatos.Aplicacao.Excecoes;

namespace AppContatos.Api.Extensoes
{
    public static class ExcecaoExtensoes
    {
        public static ErroDTO MapearParaMensagemErro(this ErroValidacaoExcecao excecao)
        {
            return new ErroDTO
            {
                Mensagem = excecao.Message,
                Erros = excecao.Erros.Select(x => new ItemErroDTO
                {
                    Propriedade = x.PropertyName,
                    Mensagem = x.ErrorMessage
                })
            };
        }

        public static ErroDTO MapearParaMensagemErro(this EntidadeNaoExisteExcecao excecao)
        {
            return new ErroDTO
            {
                Mensagem = excecao.Message
            };
        }
    }
}
