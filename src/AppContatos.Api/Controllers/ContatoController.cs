using AppContatos.Api.Model;
using AppContatos.Api.Model.Contatos.ListarContatos;
using AppContatos.Aplicacao.CasosDeUso;
using AppContatos.Aplicacao.CasosDeUso.CriarContato;
using AppContatos.Aplicacao.CasosDeUso.ExcluirContato;
using AppContatos.Aplicacao.CasosDeUso.InativarContato;
using AppContatos.Aplicacao.CasosDeUso.ListarContatos;
using AppContatos.Aplicacao.CasosDeUso.VerDetalhesContatos;
using AppContatos.Aplicacao.Consultas;
using AppContatos.Aplicacao.Consultas.Contatos;
using AppContatos.Aplicacao.Entidades;
using AppContatos.Aplicacao.Enums;
using AppContatos.Aplicacao.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace AppContatos.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContatoController : BaseController
{
    private readonly ICasoDeUso<CriarContatoInput, CriarContatoOutput> _criarContato;
    private readonly ICasoDeUso<ExcluirContatoInput, Unit> _excluirContato;
    private readonly ICasoDeUso<InativarContatoInput, Unit> _inativarContato;
    private readonly ICasoDeUso<VerDetalhesContatoInput, VerDetalhesContatoOutput> _verDetalhes;
    private readonly ICasoDeUso<ListarContatosInput, ListaPaginada<ListarContatosOutput>> _listarContatos;
    public ContatoController(ICasoDeUso<CriarContatoInput, CriarContatoOutput> criarContato, ICasoDeUso<ExcluirContatoInput, Unit> excluirContato, ICasoDeUso<InativarContatoInput, Unit> inativarContato, ICasoDeUso<VerDetalhesContatoInput, VerDetalhesContatoOutput> verDetalhes, ICasoDeUso<ListarContatosInput, ListaPaginada<ListarContatosOutput>> listarContatos)
    {
        _criarContato = criarContato;
        _excluirContato = excluirContato;
        _inativarContato = inativarContato;
        _verDetalhes = verDetalhes;
        _listarContatos = listarContatos;
    }

    [HttpPost]
    public async Task<ActionResult<CriarContatoOutput>> CriarContato(CriarContatoInput input, CancellationToken cancellationToken)
    {
        var resultado = await _criarContato.ExecutarAsync(input, cancellationToken)
            .ConfigureAwait(false);
        return RespostaResultado(resultado,
            resultadoSucesso: valor =>
                CreatedAtAction(nameof(ObterPorId), new { id = valor!.Id }, valor));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Contato>> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var resultado = await _verDetalhes.ExecutarAsync(new VerDetalhesContatoInput
        {
            Id = id
        }, cancellationToken)
            .ConfigureAwait(false);
        return RespostaResultado(resultado, resultadoSucesso: contato => Ok(contato));
    }

    [HttpGet]
    public async Task<ActionResult<ListaPaginada<ListarContatosOutput>>> ListarContatos([FromQuery] FiltroListarContatosDTO filtros, CancellationToken cancellationToken)
    {
        var resultado = await _listarContatos.ExecutarAsync(new ListarContatosInput
        {
            Pagina = filtros.Pagina,
            Tamanho = filtros.Tamanho,
            Sexo = filtros.Sexo
        }, cancellationToken)
            .ConfigureAwait(false);
        return RespostaResultado(resultado, resultadoSucesso: contatos => Ok(contatos));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> ExcluirPorId(Guid id, CancellationToken cancellationToken)
    {
        var resultado = await _excluirContato.ExecutarAsync(new ExcluirContatoInput
        {
            Id = id
        }, cancellationToken)
            .ConfigureAwait(false);
        return RespostaResultado(resultado, resultadoSucesso: _ => NoContent());
    }

    [HttpPost("{id:guid}/inativar")]
    public async Task<ActionResult> InativarPorId(Guid id, CancellationToken cancellationToken)
    {
        var resultado = await _inativarContato.ExecutarAsync(new InativarContatoInput
        {
            Id = id
        }, cancellationToken)
            .ConfigureAwait(false);
        return RespostaResultado(resultado, resultadoSucesso: _ => NoContent());
    }
}
