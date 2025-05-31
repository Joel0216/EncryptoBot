// CryptoBot/Domain/Entities/CryptoMessage.cs
namespace CryptoBot.Domain.Entities;

public class CryptoMessage
{
    public int Id { get; set; }
    public required string OriginalText { get; set; }
    public required string ProcessedText { get; set; }
    public int Shift { get; set; }
    public string Operation { get; set; } = string.Empty; // "Encrypt" o "Decrypt"
    public DateTime ProcessedAt { get; set; }
    
    public CryptoMessage()
    {
        OriginalText = string.Empty;
        ProcessedText = string.Empty;
        ProcessedAt = DateTime.UtcNow;
    }
}