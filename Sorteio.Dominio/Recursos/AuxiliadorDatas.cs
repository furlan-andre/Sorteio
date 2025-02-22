namespace Sorteio.Dominio.Recursos;

public static class AuxiliadorDatas
{
    public static DateTime ObterDataNascimento(int limiteInferior, int limiteSuperior)
    {
        var random = new Random();
        var idade = random.Next(limiteInferior, limiteSuperior);
        return  DateTime.Now.AddYears(-idade);
    }
}