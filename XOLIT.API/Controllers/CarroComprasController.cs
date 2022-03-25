using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Models;
using XOLIT.API.Service;

namespace XOLIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroComprasController : ControllerBase
    {
        private readonly ICarroComprasService _service;
        public CarroComprasController(ICarroComprasService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost("AgregarPedido")]
        public async Task<IActionResult> AgregarpedidoAsync(Factura infoFactura)
        {

            return Ok("Property created");


        }

    }

  
    
}
