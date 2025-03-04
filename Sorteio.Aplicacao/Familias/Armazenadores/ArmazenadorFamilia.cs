using System.Data;
using Sorteio.Aplicacao.Familias.Dtos;
using Sorteio.Aplicacao.Familias.Pessoas.Armazenadores;
using Sorteio.Aplicacao.Familias.Pessoas.Dtos;
using Sorteio.Aplicacao.Persistencia;
using Sorteio.Dominio.Familias;
using Sorteio.Dominio.Familias.Pessoas;

namespace Sorteio.Aplicacao.Familias.Armazenadores;

public class ArmazenadorFamilia : IArmazenadorFamilia
{
    private readonly IFamiliaRepository _familiaRepository;
    private readonly IArmazenadorPessoa _armazenadorPessoa;
    private readonly IUnitOfWork _unitOfWork;

    public ArmazenadorFamilia(IFamiliaRepository familiaRepository, IArmazenadorPessoa armazenadorPessoa, IUnitOfWork unitOfWork)
    {
        _familiaRepository = familiaRepository;
        _armazenadorPessoa = armazenadorPessoa;
        _unitOfWork = unitOfWork;
    }

    public async Task<Familia> ArmazenarAsync(GerenciaFamiliaDto gerenciaFamiliaDto)
    {
        await _unitOfWork.IniciarTransacaoAsync();

        try
        {
            var responsavel = await _armazenadorPessoa.ArmazenarAsync(gerenciaFamiliaDto.Responsavel);
            
            var dependentes = new List<Pessoa>();
            var tasks = gerenciaFamiliaDto.Dependentes.Select(async pessoa =>
            {
                var dependente = await _armazenadorPessoa.ArmazenarAsync(pessoa);
                dependentes.Add(dependente);    
            });
            
            await Task.WhenAll(tasks);
            var familia = new Familia(responsavel, dependentes);

            if (gerenciaFamiliaDto.Conjuge != null)
            {
                var conjuge = await _armazenadorPessoa.ArmazenarAsync(gerenciaFamiliaDto.Conjuge);
                familia.AdicionarConjuge(conjuge);
            }

            await _familiaRepository.AdicionarAsync(familia);
            await _unitOfWork.ConfirmarTransacaoAsync();
            return familia;
        }
        catch (ArgumentException e)
        {
            await _unitOfWork.RetrocederTransacaoAsync();
            throw;
        }
    }
}