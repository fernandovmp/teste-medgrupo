using AppContatos.Aplicacao.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppContatos.Aplicacao.Entidades
{
    public class Contato
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public EstadoEnum Estado { get; set; }
        public SexoEnum Sexo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public int ObterIdade(Services.IDateTimeProvider _dateTimeProvider)
        {
            TimeSpan timeSpan = DateTime.Today - DataNascimento;
            return (int) Math.Floor(timeSpan.TotalDays / 365.25);
        }
    }
}
