using Microsoft.AspNetCore.Mvc;
using Sorteio.Aplicacao.Familias.Armazenadores;
using Sorteio.Aplicacao.Familias.Consultas;
using Sorteio.Aplicacao.Familias.Dtos;
using Sorteio.Aplicacao.Familias.Mappers;
using Sorteio.Aplicacao.Familias.Pessoas.Dtos;

namespace Sorteio.Api.Familias;

[ApiController]
[Route("api/[controller]")]
public class FamiliaController : ControllerBase
{
    private readonly IConsultaFamilia _consultaFamilia;
    private readonly IArmazenadorFamilia _armazenadorFamilia;
    
    public FamiliaController(IConsultaFamilia consultaFamilia, IArmazenadorFamilia armazenadorFamilia)
    {
        _consultaFamilia = consultaFamilia;
        _armazenadorFamilia = armazenadorFamilia;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FamiliaDto>>> ObterTodos()
    {
        var pessoas = await _consultaFamilia.ObterTodosAsync();
        return Ok(pessoas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<FamiliaDto>>> ObterPorId(int id)
    {
        var pessoas = await _consultaFamilia.ObterPorIdAsync(id);
        return Ok(pessoas);
    }
    
    [HttpPost]
    public async Task<ActionResult<IEnumerable<FamiliaDto>>> CriarFamilia([FromBody] GerenciaFamiliaDto dto)
    {
        var familiaCriada = await _armazenadorFamilia.ArmazenarAsync(dto);
        var retorno = FamiliaMapper.ParaDto(familiaCriada);
        return CreatedAtAction(nameof(ObterPorId), new { id = familiaCriada.Id }, retorno);
    }
}