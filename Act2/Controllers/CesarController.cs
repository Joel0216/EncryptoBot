using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CesarController : ControllerBase
    {
        private readonly ICesarService _cesarService;

        public CesarController(ICesarService cesarService)
        {
            _cesarService = cesarService;
        }

        [HttpPost("cifrar")]
        public IActionResult CifrarMensaje([FromBody] CesarRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Mensaje))
                return BadRequest("Mensaje inválido");

            string mensajeCifrado = _cesarService.Cifrar(request.Mensaje, request.Desplazamiento);
            return Ok(new { MensajeCifrado = mensajeCifrado });
        }
    }

    public class CesarRequest
    {
        public string Mensaje { get; set; } = string.Empty;
        public int Desplazamiento { get; set; }
    }
}
