using AppContatos.Aplicacao.Entidades;
using AppContatos.Aplicacao.Enums;

namespace AppContatos.Aplicacao.Consultas.Contatos
{
    public class ObterContatoPorId : IConsultaEspecificacao<Contato>
    {
        public ObterContatoPorId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public EstadoEnum? Estado { get; set; }
        public bool NoTracking { get; set; } = true;

        public IQueryable<Contato> AplicarConsulta(IQueryable<Contato> contexto)
        {
            IQueryable<Contato> query = contexto;
            query = query.Where(c => c.Id == Id);
            if(Estado.HasValue)
            {
                query = query.Where(c => c.Estado == Estado.Value);
            }
            return query;
        }
    }
}
