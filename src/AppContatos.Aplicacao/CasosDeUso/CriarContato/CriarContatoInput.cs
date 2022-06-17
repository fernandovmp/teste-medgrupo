using AppContatos.Aplicacao.Enums;

namespace AppContatos.Aplicacao.CasosDeUso.CriarContato
{
    public class CriarContatoInput
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public SexoEnum Sexo { get; set; }
    }
}