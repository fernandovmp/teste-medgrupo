namespace AppContatos.Aplicacao.Excecoes
{
    public class EntidadeNaoExisteExcecao : Exception
    {
        public EntidadeNaoExisteExcecao(string mensagem) : base(mensagem)
        {
        }
    }
}
