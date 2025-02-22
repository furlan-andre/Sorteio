using Microsoft.AspNetCore.Mvc;
using Sorteio.Aplicacao.Pessoas;
using Sorteio.Aplicacao.Pessoas.Dtos;
using Sorteio.Dominio.Familia.Pessoas;
using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Infra.Pessoas;

namespace Sorteio.Api.Pessoas;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IConsultaPessoa _consultaPessoa;

    public PessoaController(IConsultaPessoa consultaPessoa)
    {
        _consultaPessoa = consultaPessoa;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PessoaDto>>> ObterTodos()
    {
        var pessoas = await _consultaPessoa.ObterTodosAsync();
        return Ok(pessoas);
    }
    
    
    [HttpGet("/{id}")]
    public async Task<ActionResult<IEnumerable<PessoaDto>>> ObterPorId(int id)
    {
        var pessoas = await _consultaPessoa.ObterPorIdAsync(id);
        if (pessoas == null) return NotFound();
        
        return Ok(pessoas);
    }
}