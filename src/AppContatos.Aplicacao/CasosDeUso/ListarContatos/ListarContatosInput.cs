using AppContatos.Aplicacao.Enums;

namespace AppContatos.Aplicacao.CasosDeUso.ListarContatos
{
    public class ListarContatosInput
    {
        public SexoEnum? Sexo { get; set; }
        public int Pagina { get; set; }
        public int Tamanho { get; set; }
    }
}