namespace Sorteio.Domain.Recursos;

public static class Mensagens
{
    public static string CampoObrigatorio = $"O campo {0} é obrigatório.";
    public static string CampoInvalido = $"O campo {0} é inválido.";

    public static string FormatarMensagem(string mensagem, string parametro) => string.Format(mensagem, parametro);
}