namespace Sorteio;

public class AuxiliadorCpf
{
    public static string ObterCpfValido(int sequencial)
    {
        if (sequencial > 3 || sequencial < 0) return null;
        
        var _cpfValidos = new string[4];
        _cpfValidos[0] = "798.630.670-08";
        _cpfValidos[1] = "941.976.820-18";
        _cpfValidos[2]  = "571.986.750-34";
        _cpfValidos[3]  = "914.507.010-51";
        
        return _cpfValidos[sequencial];
    }
    
}