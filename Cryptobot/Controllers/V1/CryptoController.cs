// CryptoBot/Controllers/V1/CryptoController.cs
using CryptoBot.Domain.Entities;
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

    // POST api/v1/crypto/encrypt
    [HttpPost("encrypt")]
    public IActionResult Encrypt([FromBody] EncryptRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest(new { Message = "El texto no puede estar vacío." });
        }

        if (request.Shift < 1 || request.Shift > 25)
        {
            return BadRequest(new { Message = "El desplazamiento debe estar entre 1 y 25." });
        }

        var result = _cipherService.Encrypt(request.Text, request.Shift);
        return Ok(result);
    }

    // POST api/v1/crypto/decrypt
    [HttpPost("decrypt")]
    public IActionResult Decrypt([FromBody] DecryptRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest(new { Message = "El texto no puede estar vacío." });
        }

        if (request.Shift < 1 || request.Shift > 25)
        {
            return BadRequest(new { Message = "El desplazamiento debe estar entre 1 y 25." });
        }

        var result = _cipherService.Decrypt(request.Text, request.Shift);
        return Ok(result);
    }

    // POST api/v1/crypto/bruteforce
    [HttpPost("bruteforce")]
    public IActionResult BruteForce([FromBody] BruteForceRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest(new { Message = "El texto no puede estar vacío." });
        }

        var possibilities = _cipherService.BruteForceDecrypt(request.Text);
        return Ok(new { 
            OriginalText = request.Text,
            Possibilities = possibilities 
        });
    }

    // GET api/v1/crypto/history
    [HttpGet("history")]
    public IActionResult GetHistory()
    {
        var history = _cipherService.GetHistory();
        return Ok(history);
    }

    // GET api/v1/crypto/history/{id}
    [HttpGet("history/{id:int}")]
    public IActionResult GetHistoryById(int id)
    {
        var item = _cipherService.GetById(id);
        if (item == null)
        {
            return NotFound(new { Message = $"No se encontró el elemento con ID {id}." });
        }
        return Ok(item);
    }

    // DELETE api/v1/crypto/history
    [HttpDelete("history")]
    public IActionResult ClearHistory()
    {
        _cipherService.ClearHistory();
        return Ok(new { Message = "Historial limpiado exitosamente." });
    }
}

// Clases para las peticiones
public class EncryptRequest
{
    public required string Text { get; set; }
    public int Shift { get; set; }
}

public class DecryptRequest
{
    public required string Text { get; set; }
    public int Shift { get; set; }
}

public class BruteForceRequest
{
    public required string Text { get; set; }
}