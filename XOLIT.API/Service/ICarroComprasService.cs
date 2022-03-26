using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Common;
using XOLIT.API.DTOs;
using XOLIT.API.Models;

namespace XOLIT.API.Service
{
    public interface ICarroComprasService
    {
        Task<Result> AgregarClienteAsync(ADDCliente cliente);

        Task<Result> BuscarClienteAsync(int numeroIdentificacion);

        Task<Result> ActualizarClienteAsync(Cliente cliente);
        Task<Result> BuscarProductoAsync(int idProducto,string nombreProducto);

        Task<Result> GuardarFacturaAsync(ADDFactura InfoFactura);

        Task<Result> BuscarFacturaAsync(int nombreCliente);

        Task<Result> GuardarDetalleFacturaAsync(DetalleFactura detalleFactura);
    }
}
