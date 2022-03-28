using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.DTOs;
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
        public async Task<IActionResult> AgregarpedidoAsync(ADDFactura InfoFactura)
        {
            var result = await _service.GuardarFacturaAsync(InfoFactura);

            if (result.StatusResult == 400)
            {
                return this.BadRequest("Parametros INvalidos");
            }
            else if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok(new { message = "factura creada"});
        }

        [HttpPost("agregarCliente")]
        public async Task<IActionResult> AgregarClienteAsync(ADDCliente infocliente)
        {
            var result = await _service.AgregarClienteAsync(infocliente);

            if (result.StatusResult == 400)
            {
                return this.BadRequest("Parametros Invalidos");
            }
            else if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok(new { message = "cliente creado" });
        }

        [HttpGet("Productos")]
        public async Task<IActionResult> BuscarProductossAsync(int idProducto, string nombreProducto)
        {
            var result = await _service.BuscarProductoAsync(idProducto,nombreProducto);

            if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok(result.Data);
        }

    }

  
    
}
