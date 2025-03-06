using FluentAssertions;
using Moq;
using Sorteio;
using Sorteio.Aplicacao.Familias.Armazenadores;
using Sorteio.Aplicacao.Familias.Pessoas.Armazenadores;
using Sorteio.Aplicacao.Familias.Pessoas.Mappers;
using Sorteio.Aplicacao.Persistencia;
using Sorteio.Dominio.Familias;
using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Dominio.Recursos;

public class ArmazenadorDependenteTests
{
    private readonly Mock<IArmazenadorPessoa> _armazenadorPessoaMock;
    private readonly Mock<IFamiliaRepository> _familiaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ArmazenadorDependente _armazenadorDependente;

    public ArmazenadorDependenteTests()
    {
        _armazenadorPessoaMock = new Mock<IArmazenadorPessoa>();
        _familiaRepositoryMock = new Mock<IFamiliaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _armazenadorDependente = new ArmazenadorDependente(
            _armazenadorPessoaMock.Object,
            _familiaRepositoryMock.Object,
            _unitOfWorkMock.Object
        ); 
    }
    
    [Fact]
    public async Task DeveAdicionarDependenteQuandoFamiliaExistir()
    {
        var responsavel = new PessoaBuilder().Novo().ComMaioridade().ComCpf(AuxiliadorCpf.ObterCpfValido(0)).Criar();
        var familiaExistente = new Familia(responsavel);
        var dependente = new PessoaBuilder().Novo().ComMenoridade().ComCpf(AuxiliadorCpf.ObterCpfValido(1)).Criar();
        var armazenarDependenteDto = PessoaMapper.ParaArmazenaPessoaDto(dependente);
        _familiaRepositoryMock.Setup(repositorio => repositorio.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(familiaExistente);
        _armazenadorPessoaMock.Setup(p => p.ArmazenarAsync(armazenarDependenteDto)).ReturnsAsync(dependente);
        
        await _armazenadorDependente.ArmazenarDependente(1, armazenarDependenteDto);
        
        _unitOfWorkMock.Verify(u => u.IniciarTransacaoAsync(), Times.Once);
        _unitOfWorkMock.Verify(u => u.ConfirmarTransacaoAsync(), Times.Once);
        _unitOfWorkMock.Verify(u => u.RetrocederTransacaoAsync(), Times.Never);
        familiaExistente.Dependentes.Should().ContainSingle().Which.Should().BeEquivalentTo(dependente);
        _familiaRepositoryMock.Verify(repositorio => repositorio.AtualizarAsync(It.IsAny<Familia>()), Times.Once);
    }

    [Fact]
    public async Task NaoDevePermitirAdicionarDependenteQuandoFamiliaNaoExistir()
    {
        var mensagemEsperada = Mensagens.FormatarMensagem(Mensagens.NaoFoiEncontrada, "Familia");
        var dependente = new PessoaBuilder().Novo().ComMenoridade().ComCpf(AuxiliadorCpf.ObterCpfValido(1)).Criar();
        var armazenarDependenteDto = PessoaMapper.ParaArmazenaPessoaDto(dependente);
        _familiaRepositoryMock.Setup(repositorio => repositorio.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync((Familia)null);
        var acao = async () => await _armazenadorDependente.ArmazenarDependente(1, armazenarDependenteDto);
    
        await acao.Should().ThrowAsync<ArgumentException>().WithMessage(mensagemEsperada);
        _familiaRepositoryMock.Verify(repositorio => repositorio.AtualizarAsync(It.IsAny<Familia>()), Times.Never);
    }
    
    [Fact]
    public async Task DeveRetrocederQuandoAcontecerAlgoInesperadoNaTransacao()
    {
        var mensagemEsperada = "Erro ao armazenar dependente";
        var responsavel = new PessoaBuilder().Novo().ComMaioridade().ComCpf(AuxiliadorCpf.ObterCpfValido(0)).Criar();
        var familiaExistente = new Familia(responsavel);
        var dependente = new PessoaBuilder().Novo().ComMenoridade().ComCpf(AuxiliadorCpf.ObterCpfValido(1)).Criar();
        var armazenarDependenteDto = PessoaMapper.ParaArmazenaPessoaDto(dependente);
        _familiaRepositoryMock.Setup(repo => 
            repo.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(familiaExistente);
        _armazenadorPessoaMock.Setup(p => 
            p.ArmazenarAsync(armazenarDependenteDto)).ThrowsAsync(new Exception(mensagemEsperada));
    
        var acao = async () => await _armazenadorDependente.ArmazenarDependente(1, armazenarDependenteDto);
    
        await acao.Should().ThrowAsync<Exception>().WithMessage(mensagemEsperada);
        _unitOfWorkMock.Verify(u => u.IniciarTransacaoAsync(), Times.Once);
        _unitOfWorkMock.Verify(u => u.RetrocederTransacaoAsync(), Times.Once);
        _unitOfWorkMock.Verify(u => u.ConfirmarTransacaoAsync(), Times.Never);
    }
}