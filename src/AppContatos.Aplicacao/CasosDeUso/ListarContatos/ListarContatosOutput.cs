namespace AppContatos.Aplicacao.CasosDeUso.ListarContatos
{
    public class ListarContatosOutput
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public int Idade { get; set; }
        public string? Sexo { get; set; }
    }
}