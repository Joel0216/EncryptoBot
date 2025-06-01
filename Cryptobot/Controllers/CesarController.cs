using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

namespace TuProyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CesarController : ControllerBase
    {
        private readonly ICesarService _cesarService;

        // Constructor para inyectar el service
        public CesarController(ICesarService cesarService)
        {
            _cesarService = cesarService;
        }

        [HttpGet("desencriptar")]
        public IActionResult DesencriptarMensaje(string mensaje, int desplazamiento = 3)
        {
            try
            {
                if (string.IsNullOrEmpty(mensaje))
                {
                    return BadRequest("Debes enviar un mensaje para desencriptar");
                }

                if (desplazamiento < 0 || desplazamiento > 25)
                {
                    return BadRequest("El desplazamiento debe estar entre 0 y 25");
                }

                string mensajeDesencriptado = _cesarService.DesencriptarMensaje(mensaje, desplazamiento);

                var respuesta = new DesencriptarResponse
                {
                    MensajeOriginal = mensaje,
                    MensajeDesencriptado = mensajeDesencriptado,
                    DesplazamientoUsado = desplazamiento
                };

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }

    // Clase para manejar la respuesta
    public class DesencriptarResponse
    {
        public string MensajeOriginal { get; set; }
        public string MensajeDesencriptado { get; set; }
        public int DesplazamientoUsado { get; set; }
    }
}

