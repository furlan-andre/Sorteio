using Microsoft.AspNetCore.Mvc;
using Sorteio.Aplicacao.Classificacoes;

namespace Sorteio.Api.Classificacoes;

[Controller]
[Route("api/[controller]")]
public class ClassificacaoController : ControllerBase
{
    private readonly IClassificacaoFamilia _classificacaoFamilia;

    public ClassificacaoController(IClassificacaoFamilia classificacaoFamilia)
    {
        _classificacaoFamilia = classificacaoFamilia;
    }

    [HttpPost]
    public async Task<ActionResult> RealizarClassificacao()
    {
        return Ok(await _classificacaoFamilia.RealizarClassificacao());
    }
}