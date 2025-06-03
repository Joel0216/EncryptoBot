public interface ICesarService
{
    string Cifrar(string texto, int desplazamiento);
}

public class CesarService : ICesarService
{
    public string Cifrar(string texto, int desplazamiento)
    {
        if (string.IsNullOrEmpty(texto)) return texto;

        desplazamiento = desplazamiento % 26;

        char CifrarCaracter(char c)
        {
            if (!char.IsLetter(c))
                return c;

            char offset = char.IsUpper(c) ? 'A' : 'a';
            return (char)(((c + desplazamiento - offset) % 26) + offset);
        }

        return new string(texto.Select(CifrarCaracter).ToArray());
    }
}
