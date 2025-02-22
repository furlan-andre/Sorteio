namespace Sorteio.Dominio.Recursos;

public static class StringExtensions
{
    public static string SomenteNumeros(this string parametro)
    {
        return string.IsNullOrWhiteSpace(parametro)
            ? string.Empty
            : string.Concat(parametro.Where(char.IsDigit));
    }
}