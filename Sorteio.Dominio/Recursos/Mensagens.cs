namespace Sorteio.Dominio.Recursos;

public static class Mensagens
{
    public static string CampoObrigatorio = "O campo {0} é obrigatório.";
    public static string CampoInvalido = "O campo {0} é inválido.";
    public static string OCpfInformadoJaEstaCadastrado = "O CPF informado já está cadastrado.";
    public static string NaoFoiEncontrado = "{0} não foi encontrado.";
    public static string NaoFoiEncontrada = "{0} não foi encontrada.";
    public static string OIdDaURLDeveSerOMesmoDoCorpoDaRequisicao = "O Id da URL deve ser o mesmo do corpo da requisição.";
    
    public static string FormatarMensagem(string mensagem, string parametro) => string.Format(mensagem, parametro);
}