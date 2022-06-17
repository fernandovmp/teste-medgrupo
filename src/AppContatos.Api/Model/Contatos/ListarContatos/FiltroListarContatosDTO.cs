using AppContatos.Aplicacao.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AppContatos.Api.Model.Contatos.ListarContatos
{
    public class FiltroListarContatosDTO
    {
        [FromQuery]
        public int Pagina { get; set; } = 1;
        [FromQuery]
        public int Tamanho { get; set; } = 10;
        [FromQuery]
        public SexoEnum? Sexo { get; set; }
    }
}
