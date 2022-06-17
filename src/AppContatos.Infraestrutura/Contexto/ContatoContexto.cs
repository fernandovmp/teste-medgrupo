using AppContatos.Aplicacao.Entidades;
using AppContatos.Infraestrutura.Contexto.Maps;
using Microsoft.EntityFrameworkCore;

namespace AppContatos.Infraestrutura.Contexto
{
    public class ContatoContexto : DbContext
    {
        public ContatoContexto(DbContextOptions<ContatoContexto> options) : base(options)
        {
        }

        public DbSet<Contato> Contatos => Set<Contato>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());
        }
    }
}
