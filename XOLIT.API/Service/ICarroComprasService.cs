using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Common;
using XOLIT.API.Models;

namespace XOLIT.API.Service
{
    public interface ICarroComprasService
    {
        Task<Result> AgregarClienteAsync(Cliente cliente);

        Task<Result> BuscarClienteAsync(string nombreCliente);

        Task<Result> ActualizarClienteAsync(Cliente cliente);
        Task<Result> BuscarProductoAsync(string nombreProducto);

        Task<Result> GuardarFacturaAsync(Factura factura);

        Task<Result> BuscarFacturaAsync(string nombreCliente);

        Task<Result> GuardarDetalleFacturaAsync(DetalleFactura detalleFactura);
    }
}
