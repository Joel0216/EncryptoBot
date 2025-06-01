using System;

namespace TuProyecto.Services
{
    public interface ICesarService
    {
        string DesencriptarMensaje(string mensajeEncriptado, int desplazamiento);
    }

    public class CesarService : ICesarService
    {
        public string DesencriptarMensaje(string mensajeEncriptado, int desplazamiento)
        {
            if (string.IsNullOrEmpty(mensajeEncriptado))
            {
                return "El mensaje no puede estar vacío";
            }

            string resultado = "";
            
            // Recorremos cada letra del mensaje
            foreach (char letra in mensajeEncriptado)
            {
                // Si es una letra mayúscula
                if (char.IsUpper(letra))
                {
                    // Convertimos a número (A=0, B=1, etc.)
                    int numeroLetra = letra - 'A';
                    // Aplicamos el desplazamiento hacia atrás
                    int nuevoNumero = (numeroLetra - desplazamiento + 26) % 26;
                    // Convertimos de vuelta a letra
                    char nuevaLetra = (char)(nuevoNumero + 'A');
                    resultado += nuevaLetra;
                }
                // Si es una letra minúscula
                else if (char.IsLower(letra))
                {
                    // Mismo proceso pero con minúsculas
                    int numeroLetra = letra - 'a';
                    int nuevoNumero = (numeroLetra - desplazamiento + 26) % 26;
                    char nuevaLetra = (char)(nuevoNumero + 'a');
                    resultado += nuevaLetra;
                }
                // Si no es letra (espacios, números, símbolos) los dejamos igual
                else
                {
                    resultado += letra;
                }
            }

            return resultado;
        }
    }
}
