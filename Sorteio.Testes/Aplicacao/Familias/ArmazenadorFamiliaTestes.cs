using FluentAssertions;
using Moq;
using Sorteio;
using Sorteio.Aplicacao.Familias.Armazenadores;
using Sorteio.Aplicacao.Familias.Dtos;
using Sorteio.Aplicacao.Familias.Pessoas.Armazenadores;
using Sorteio.Aplicacao.Familias.Pessoas.Mappers;
using Sorteio.Aplicacao.Persistencia;
using Sorteio.Dominio.Familias;
using Sorteio.Dominio.Familias.Pessoas;

public class ArmazenadorFamiliaTests
{
    private readonly Mock<IFamiliaRepository> _familiaRepositoryMock;
    private readonly Mock<IArmazenadorPessoa> _armazenadorPessoaMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ArmazenadorFamilia _armazenadorFamilia;

    public ArmazenadorFamiliaTests()
    {
        _familiaRepositoryMock = new Mock<IFamiliaRepository>();
        _armazenadorPessoaMock = new Mock<IArmazenadorPessoa>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _armazenadorFamilia = new ArmazenadorFamilia(
            _familiaRepositoryMock.Object,
            _armazenadorPessoaMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task DeveArmazenarFamiliaComResponsavelEDependentesComSucesso()
    {
        var responsavelPessoa = new PessoaBuilder().Novo().ComMaioridade().ComCpf(AuxiliadorCpf.ObterCpfValido(0)).Criar();
        var responsavelDto = PessoaMapper.ParaArmazenaPessoaDto(responsavelPessoa);
        var dependentePessoa = new PessoaBuilder().Novo().ComMenoridade().ComCpf(AuxiliadorCpf.ObterCpfValido(1)).Criar();
        var dependenteDto = PessoaMapper.ParaArmazenaPessoaDto(dependentePessoa);
        var gerenciaFamiliaDto = new GerenciaFamiliaDto
        {
            Responsavel = responsavelDto,
            Dependentes = [dependenteDto]
        };
        _armazenadorPessoaMock.Setup(p => p.ArmazenarAsync(responsavelDto)).ReturnsAsync(responsavelPessoa);
        _armazenadorPessoaMock.Setup(p => p.ArmazenarAsync(dependenteDto)).ReturnsAsync(dependentePessoa);

        var familiaCriada = await _armazenadorFamilia.ArmazenarAsync(gerenciaFamiliaDto);
        
        familiaCriada.Should().NotBeNull();
        familiaCriada.Responsavel.Should().BeEquivalentTo(responsavelPessoa);
        familiaCriada.Dependentes.Should().ContainSingle().Which.Should().BeEquivalentTo(dependentePessoa);
        _unitOfWorkMock.Verify(u => u.IniciarTransacaoAsync(), Times.Once);
        _unitOfWorkMock.Verify(u => u.ConfirmarTransacaoAsync(), Times.Once);
        _unitOfWorkMock.Verify(u => u.RetrocederTransacaoAsync(), Times.Never);
        _familiaRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Familia>()), Times.Once);
    }

    [Fact]
    public async Task DeveArmazenarFamiliaComResponsavelEConjugeComSucesso()
    {
        var responsavelPessoa = new PessoaBuilder().Novo().ComMaioridade().ComCpf(AuxiliadorCpf.ObterCpfValido(0)).Criar();
        var responsavelDto = PessoaMapper.ParaArmazenaPessoaDto(responsavelPessoa);
        var conjugePessoa = new PessoaBuilder().Novo().ComMaioridade().ComCpf(AuxiliadorCpf.ObterCpfValido(1)).Criar();
        var conjugeDto = PessoaMapper.ParaArmazenaPessoaDto(conjugePessoa);
        var gerenciaFamiliaDto = new GerenciaFamiliaDto
        {
            Responsavel = responsavelDto,
            Conjuge = conjugeDto
        };
        _armazenadorPessoaMock.Setup(p => p.ArmazenarAsync(responsavelDto)).ReturnsAsync(responsavelPessoa);
        _armazenadorPessoaMock.Setup(p => p.ArmazenarAsync(conjugeDto)).ReturnsAsync(conjugePessoa);

        var familiaCriada = await _armazenadorFamilia.ArmazenarAsync(gerenciaFamiliaDto);
        
        familiaCriada.Should().NotBeNull();
        familiaCriada.Conjuge.Should().BeEquivalentTo(conjugePessoa);
        _unitOfWorkMock.Verify(u => u.IniciarTransacaoAsync(), Times.Once);
        _unitOfWorkMock.Verify(u => u.ConfirmarTransacaoAsync(), Times.Once);
        _unitOfWorkMock.Verify(u => u.RetrocederTransacaoAsync(), Times.Never);
        _familiaRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Familia>()), Times.Once);
    }
   

    [Fact]
    public async Task DeveRetrocederTransacaoQuandoFalharArmazenadorDePessoa()
    {
        var mensagemEsperada = "Teste de excecao";
        var responsavelPessoa = new PessoaBuilder().Novo().ComMaioridade().ComCpf(AuxiliadorCpf.ObterCpfValido(0)).Criar();
        var responsavelDto = PessoaMapper.ParaArmazenaPessoaDto(responsavelPessoa);
        var gerenciaFamiliaDto = new GerenciaFamiliaDto
        {
            Responsavel = responsavelDto
        };
        _armazenadorPessoaMock.Setup(p => p.ArmazenarAsync(responsavelDto))
            .ThrowsAsync(new ArgumentException(mensagemEsperada));
    
        Func<Task> act = async () => await _armazenadorFamilia.ArmazenarAsync(gerenciaFamiliaDto);
    
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(mensagemEsperada);
        _unitOfWorkMock.Verify(u => u.IniciarTransacaoAsync(), Times.Once);
        _unitOfWorkMock.Verify(u => u.ConfirmarTransacaoAsync(), Times.Never);
        _unitOfWorkMock.Verify(u => u.RetrocederTransacaoAsync(), Times.Once);
    }
}
