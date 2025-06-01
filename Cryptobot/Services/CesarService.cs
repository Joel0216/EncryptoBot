// CryptoBot/Services/Features/Crypto/CaesarCipherService.cs
using CryptoBot.Domain.Entities;

namespace CryptoBot.Services.Features.Crypto;

public class CaesarCipherService
{
    private readonly List<CryptoMessage> _history;
    private int _nextId = 1;

    public CaesarCipherService()
    {
        _history = new List<CryptoMessage>();
    }

    // Método principal para encriptar
    public CryptoMessage Encrypt(string text, int shift)
    {
        var encryptedText = ProcessText(text, shift, true);
        
        var cryptoMessage = new CryptoMessage
        {
            Id = _nextId++,
            OriginalText = text,
            ProcessedText = encryptedText,
            Shift = shift,
            Operation = "Encrypt",
            ProcessedAt = DateTime.UtcNow
        };

        _history.Add(cryptoMessage);
        return cryptoMessage;
    }

    // Método principal para desencriptar
    public CryptoMessage Decrypt(string text, int shift)
    {
        var decryptedText = ProcessText(text, shift, false);
        
        var cryptoMessage = new CryptoMessage
        {
            Id = _nextId++,
            OriginalText = text,
            ProcessedText = decryptedText,
            Shift = shift,
            Operation = "Decrypt",
            ProcessedAt = DateTime.UtcNow
        };

        _history.Add(cryptoMessage);
        return cryptoMessage;
    }

    // Método para obtener el historial
    public IEnumerable<CryptoMessage> GetHistory()
    {
        return _history.OrderByDescending(h => h.ProcessedAt);
    }

    // Método para obtener por ID
    public CryptoMessage? GetById(int id)
    {
        return _history.FirstOrDefault(h => h.Id == id);
    }

    // Método para limpiar historial
    public bool ClearHistory()
    {
        _history.Clear();
        _nextId = 1;
        return true;
    }

    // Lógica del cifrado César
    private string ProcessText(string text, int shift, bool isEncryption)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        // Si es desencriptación, invertimos el desplazamiento
        if (!isEncryption)
            shift = -shift;

        var result = new char[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            char currentChar = text[i];

            if (char.IsLetter(currentChar))
            {
                // Determinar si es mayúscula o minúscula
                bool isUpper = char.IsUpper(currentChar);
                char baseChar = isUpper ? 'A' : 'a';

                // Aplicar el cifrado César
                int shifted = ((currentChar - baseChar + shift) % 26 + 26) % 26;
                result[i] = (char)(baseChar + shifted);
            }
            else
            {
                // Mantener caracteres que no son letras sin cambios
                result[i] = currentChar;
            }
        }

        return new string(result);
    }

    // Método adicional: fuerza bruta para desencriptar sin conocer el shift
    public List<string> BruteForceDecrypt(string text)
    {
        var possibilities = new List<string>();
        
        for (int shift = 1; shift <= 25; shift++)
        {
            var decrypted = ProcessText(text, shift, false);
            possibilities.Add($"Shift {shift}: {decrypted}");
        }
        
        return possibilities;
    }
}
