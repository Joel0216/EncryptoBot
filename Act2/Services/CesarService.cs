namespace WebAPI.Services
{
    public interface ICesarService
    {
        string Cifrar(string mensaje, int desplazamiento);
    }

    public class CesarService : ICesarService
    {
        public string Cifrar(string mensaje, int desplazamiento)
        {
            if (string.IsNullOrEmpty(mensaje))
                return string.Empty;

            desplazamiento = desplazamiento % 26;
            var resultado = new char[mensaje.Length];

            for (int i = 0; i < mensaje.Length; i++)
            {
                char c = mensaje[i];

                if (char.IsLetter(c))
                {
                    char baseLetra = char.IsUpper(c) ? 'A' : 'a';
                    resultado[i] = (char)((((c - baseLetra) + desplazamiento + 26) % 26) + baseLetra);
                }
                else
                {
                    resultado[i] = c;
                }
            }

            return new string(resultado);
        }
    }
}
