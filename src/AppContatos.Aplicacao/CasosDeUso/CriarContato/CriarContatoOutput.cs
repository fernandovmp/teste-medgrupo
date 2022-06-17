using AppContatos.Aplicacao.Enums;

namespace AppContatos.Aplicacao.CasosDeUso.CriarContato
{
    public class CriarContatoOutput
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public SexoEnum Sexo { get; set; }
    }
}