using Microsoft.AspNetCore.Mvc;

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

        public class CesarRequest
        {
            public string Mensaje { get; set; }
            public int Desplazamiento { get; set; }
        }

        [HttpPost("cifrar")]
        public IActionResult Cifrar([FromBody] CesarRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Mensaje))
                return BadRequest("El mensaje no puede estar vacío.");

            var textoCifrado = _cesarService.Cifrar(request.Mensaje, request.Desplazamiento);

            return Ok(new { MensajeCifrado = textoCifrado });
        }
    }
}
