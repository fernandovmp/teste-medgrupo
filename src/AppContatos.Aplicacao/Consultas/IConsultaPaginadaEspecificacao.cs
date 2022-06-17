namespace AppContatos.Aplicacao.Consultas
{
    public interface IConsultaPaginadaEspecificacao<T> : IConsultaEspecificacao<T> where T : class
    {
        int Pagina { get; }
        int Tamanho { get; }
        IQueryable<T> AplicarFiltro(IQueryable<T> conexto);
    }
}
