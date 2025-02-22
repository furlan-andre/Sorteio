using System.Text.RegularExpressions;

namespace Sorteio.Dominio.Recursos;

public class ValidadorDocumento
{
    public static bool ValidarCpf(string cpf)
    {   if (cpf.Length != 11)
            return false;
        
        if (cpf.Distinct().Count() == 1)
            return false;
        
        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string copia = cpf.Substring(0, 9);
        int primeiroDigito = CalcularDigitoVerificador(copia, multiplicadores1);
        int segundoDigito = CalcularDigitoVerificador(copia + primeiroDigito, multiplicadores2);

        return cpf.EndsWith($"{primeiroDigito}{segundoDigito}");
    }
    
    private static int CalcularDigitoVerificador(string cpfParcial, int[] multiplicadores)
    {
        int somaDosProdutos = 0;

        for (int i = 0; i < cpfParcial.Length; i++)
        {
            int digito = int.Parse(cpfParcial.Substring(i,1)); 
            somaDosProdutos += digito * multiplicadores[i];
        }

        int restoDivisao = somaDosProdutos % 11;

        return (restoDivisao < 2) ? 0 : (11 - restoDivisao);
    }
}