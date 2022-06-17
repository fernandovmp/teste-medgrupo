namespace AppContatos.Aplicacao.Consultas
{
    public interface IConsultaEspecificacao<T> where T : class
    {
        bool NoTracking { get; }
        IQueryable<T> AplicarConsulta(IQueryable<T> contexto);
    }
}
