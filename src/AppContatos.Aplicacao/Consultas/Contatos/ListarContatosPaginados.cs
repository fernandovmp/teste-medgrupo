using AppContatos.Aplicacao.Entidades;
using AppContatos.Aplicacao.Enums;

namespace AppContatos.Aplicacao.Consultas.Contatos
{
    public class ListarContatosPaginados : IConsultaPaginadaEspecificacao<Contato>
    {
        public ListarContatosPaginados(int pagina, int tamanho)
        {
            Pagina = Math.Max(pagina, 1);
            Tamanho = Math.Max(tamanho, 1);
        }

        public int Pagina { get; }
        public int Tamanho { get; }
        public bool NoTracking { get; set; } = true;
        public SexoEnum? Sexo { get; set; }

        public IQueryable<Contato> AplicarConsulta(IQueryable<Contato> contexto)
        {
            return AplicarFiltro(contexto)
                .OrderBy(c => c.Nome)
                .Skip((Pagina - 1) * Tamanho)
                .Take(Tamanho);
        }

        public IQueryable<Contato> AplicarFiltro(IQueryable<Contato> conexto)
        {
            IQueryable<Contato> query = conexto
                .Where(c => c.Estado == EstadoEnum.Ativo);
            if(Sexo.HasValue)
            {
                query = query.Where(c => c.Sexo == Sexo.Value);
            }
            return query;
        }
    }
}
