namespace AppContatos.Api.Model
{
    public class ErroDTO
    {
        public string? Mensagem { get; set; }
        public IEnumerable<ItemErroDTO> Erros { get; set; } = new List<ItemErroDTO>();
    }
}
