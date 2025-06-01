// CryptoBot/Controllers/V1/CryptoController.cs
using CryptoBot.Services.Features.Crypto;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBot.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class CryptoController : ControllerBase
{
    private readonly CaesarCipherService _cipherService;

    public CryptoController(CaesarCipherService cipherService)
    {
        _cipherService = cipherService;
    }

    // GET api/v1/crypto/process/{text}/{shift}/{operation}
    [HttpGet("process/{text}/{shift:int}/{operation}")]
    public IActionResult ProcessText(string text, int shift, string operation)
    {
        // Validaciones
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest(new { Message = "El texto no puede estar vacío." });
        }

        if (shift < 1 || shift > 25)
        {
            return BadRequest(new { Message = "El desplazamiento debe estar entre 1 y 25." });
        }

        var validOperations = new[] { "encrypt", "decrypt", "bruteforce" };
        if (!validOperations.Contains(operation.ToLower()))
        {
            return BadRequest(new { Message = "Operación válida: encrypt, decrypt, o bruteforce" });
        }

        try
        {
            switch (operation.ToLower())
            {
                case "encrypt":
                    var encryptResult = _cipherService.Encrypt(text, shift);
                    return Ok(new
                    {
                        Operation = "Encrypt",
                        OriginalText = text,
                        Result = encryptResult.ProcessedText,
                        Shift = shift,
                        ProcessedAt = encryptResult.ProcessedAt
                    });

                case "decrypt":
                    var decryptResult = _cipherService.Decrypt(text, shift);
                    return Ok(new
                    {
                        Operation = "Decrypt",
                        OriginalText = text,
                        Result = decryptResult.ProcessedText,
                        Shift = shift,
                        ProcessedAt = decryptResult.ProcessedAt
                    });

                case "bruteforce":
                    var possibilities = _cipherService.BruteForceDecrypt(text);
                    return Ok(new
                    {
                        Operation = "BruteForce",
                        OriginalText = text,
                        AllPossibilities = possibilities,
                        TotalPossibilities = possibilities.Count
                    });

                default:
                    return BadRequest(new { Message = "Operación no válida." });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error interno del servidor.", Error = ex.Message });
        }
    }
}
