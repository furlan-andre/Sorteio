using Sorteio.Aplicacao.Familias.Pessoas.Armazenadores;
using Sorteio.Aplicacao.Familias.Pessoas.Dtos;
using Sorteio.Aplicacao.Familias.Pessoas.Mappers;
using Sorteio.Aplicacao.Persistencia;
using Sorteio.Dominio.Familias;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Aplicacao.Familias.Armazenadores;

public class ArmazenadorDependente : IArmazenadorDependente
{
    private readonly IArmazenadorPessoa _armazenadorPessoa;
    private readonly IFamiliaRepository _familiaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ArmazenadorDependente(IArmazenadorPessoa armazenadorPessoa, IFamiliaRepository familiaRepository, IUnitOfWork unitOfWork)
    {
        _armazenadorPessoa = armazenadorPessoa;
        _familiaRepository = familiaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PessoaDto> ArmazenarDependente(int familiaId, ArmazenaPessoaDto armazenaPessoaDto)
    {
        var familia = await _familiaRepository.ObterPorIdAsync(familiaId);
        if (familia == null)
            throw new ArgumentException(Mensagens.FormatarMensagem(Mensagens.NaoFoiEncontrada, "Familia"));

        _unitOfWork.IniciarTransacaoAsync();
        try
        {
            var pessoa = await _armazenadorPessoa.ArmazenarAsync(armazenaPessoaDto);
            familia.AdicionarDependente(pessoa);
            await _familiaRepository.AtualizarAsync(familia);
            _unitOfWork.ConfirmarTransacaoAsync();
            return PessoaMapper.ParaDto(pessoa);
        }
        catch(Exception ex)
        {
            _unitOfWork.RetrocederTransacaoAsync();
            throw ex;
        }
    }
}