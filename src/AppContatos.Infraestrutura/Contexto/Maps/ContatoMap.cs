using AppContatos.Aplicacao.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AppContatos.Infraestrutura.Contexto.Maps
{
    internal class ContatoMap : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(32);
            builder.Property(x => x.Estado).IsRequired();
            builder.Property(x => x.Sexo).IsRequired();
            builder.Property(x => x.DataNascimento).IsRequired();
            builder.Property(x => x.DataCriacao).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.DataAtualizacao).HasDefaultValueSql("GETDATE()");
        }
    }
}
