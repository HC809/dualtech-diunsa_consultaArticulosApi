using DiunsaConsultaArticulos.API.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiunsaConsultaArticulos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaArticulosController : ControllerBase
    {
        private readonly IConsultaService _service;

        public ConsultaArticulosController(IConsultaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("~/api/[controller]/{barCode}/{macAddress}")]
        public IActionResult Get(string barCode, string macAddress)
        {
            var response = _service.CosultaArticulo(barCode, macAddress);

            if (response == null)
                return NotFound($"No se encontro artículo con código de barra {barCode}");

            if (string.IsNullOrEmpty(response.tienda))
                return BadRequest($"El dispositivo no esta asociado a ninguna tienda. MAC Address: {macAddress}");

            return Ok(response);
        }

        [HttpGet]
        [Route("~/api/[controller]/Precios/{barCode}")]
        public IActionResult Get(string barCode)
        {
            var response = _service.CosultaPrecios(barCode);

            if (response == null)
                return NotFound($"No se encontro artículo con código de barra {barCode}");

            return Ok(response);
        }
    }
}
