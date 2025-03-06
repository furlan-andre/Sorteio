using Microsoft.AspNetCore.Mvc;
using Sorteio.Aplicacao.Familias.Pessoas.Armazenadores;
using Sorteio.Aplicacao.Familias.Pessoas.Atualizadores;
using Sorteio.Aplicacao.Familias.Pessoas.Consultas;
using Sorteio.Aplicacao.Familias.Pessoas.Dtos;

namespace Sorteio.Api.Pessoas;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly IConsultaPessoa _consultaPessoa;
    private readonly IArmazenadorPessoa _armazenadorPessoa;
    private readonly IAtualizadorPessoa _atualizadorPessoa;
    
    public PessoaController(IConsultaPessoa consultaPessoa, IArmazenadorPessoa armazenadorPessoa, IAtualizadorPessoa atualizadorPessoa)
    {
        _consultaPessoa = consultaPessoa;
        _armazenadorPessoa = armazenadorPessoa;
        _atualizadorPessoa = atualizadorPessoa;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PessoaDto>>> ObterTodos()
    {
        var pessoas = await _consultaPessoa.ObterTodosAsync();
        return Ok(pessoas);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<PessoaDto>>> ObterPorId(int id)
    {
        var pessoas = await _consultaPessoa.ObterPorIdAsync(id);
        return Ok(pessoas);
    }
    
    [HttpPost]
    public async Task<ActionResult<IEnumerable<ArmazenaPessoaDto>>> CriarPessoa([FromBody] ArmazenaPessoaDto armazenaPessoaDto)
    {
        await _armazenadorPessoa.ArmazenarAsync(armazenaPessoaDto);
        return Created();
    }
        
    [HttpPut("{id}")]
    public async Task<ActionResult<IEnumerable<AtualizaPessoaDto>>> AtualizarPessoa(
        [FromBody] AtualizaPessoaDto atualizaPessoaDto,
        int id)
    {
        await _atualizadorPessoa.AtualizarAsync(id, atualizaPessoaDto);
        return Ok();
    }
}