using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Common;
using XOLIT.API.Database;
using XOLIT.API.Models;

namespace XOLIT.API.Service
{
    public class CarroComprasService : ICarroComprasService
    {
        private readonly DatabaseContext _context;

        public CarroComprasService(DatabaseContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result> AgregarClienteAsync(Cliente cliente)
        {
            try
            {
                var isInvalidCliente = string.IsNullOrWhiteSpace(cliente.Nombre) ||
                                    string.IsNullOrWhiteSpace(cliente.Direccion) ||
                                    string.IsNullOrWhiteSpace(cliente.Direccion) ||
                                    cliente.NumeroIdentificacion == 0 ||
                                    cliente.Telefono == 0;
                if (isInvalidCliente)
                {
                    return new Result() { StatusResult = 400, StatusMessage = "parametros invalidos" };
                }

                _context.cliente.Add(cliente);
                await _context.SaveChangesAsync();

                return new Result() { StatusResult = 200, StatusMessage = "Ok" };
            }
            catch (Exception ex)
            {
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
           
        }

        public async Task<Result> BuscarClienteAsync(string nombreCliente)
        {
            try
            {
                var querable = this._context.cliente.AsQueryable();

                if (!string.IsNullOrWhiteSpace(nombreCliente))
                {
                    querable = querable.Where(x => x.Nombre.Contains(nombreCliente));
                }

                var cliente = await querable.Select(x => new Cliente()
                {
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    NumeroIdentificacion = x.NumeroIdentificacion,
                    Direccion = x.Direccion,
                    Telefono = x.Telefono
                }).ToListAsync();

                return new Result() { StatusResult = 200, StatusMessage = "Ok", Data = cliente };

            }
            catch (Exception ex)
            {
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
           
        }

        public async Task<Result> ActualizarClienteAsync(Cliente infoCliente)
        {
            try
            {
                var cliente = await this._context.cliente.FindAsync(infoCliente.NumeroIdentificacion);

                if (cliente is null)
                {
                    return new Result() { StatusResult = 404, StatusMessage = "cliente no Existe" };
                }

                cliente.Nombre = (!string.IsNullOrWhiteSpace(infoCliente.Nombre)) ? infoCliente.Nombre : cliente.Nombre;
                cliente.Apellido = (!string.IsNullOrWhiteSpace(infoCliente.Apellido)) ? infoCliente.Apellido : cliente.Apellido;
                cliente.Direccion = (!string.IsNullOrWhiteSpace(infoCliente.Direccion)) ? infoCliente.Direccion : cliente.Direccion;
                cliente.Telefono = (cliente.Telefono != infoCliente.Telefono) ? infoCliente.Telefono : cliente.Telefono;

                _context.cliente.Update(cliente);
                await _context.SaveChangesAsync();

                return new Result() { StatusResult = 200, StatusMessage = "Ok" };


            }
            catch (Exception ex)
            {
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
           
        }

        public async Task<Result> BuscarProductoAsync(string nombreProducto)
        {
            try
            {
                var querable = this._context.producto.AsQueryable();

                if (!string.IsNullOrWhiteSpace(nombreProducto))
                {
                    querable = querable.Where(x => x.Nombre.Contains(nombreProducto));
                }

                var producto = await querable.Select(x => new Producto()
                {
                    Nombre = x.Nombre,
                    ValorVentaConIVA = x.ValorVentaConIVA,
                    CantidadUnidadesIventario = x.CantidadUnidadesIventario,
                    PorcentajeIVAAplicado = x.PorcentajeIVAAplicado
                }).ToListAsync();

                return new Result() { StatusResult = 200, StatusMessage = "Ok", Data = producto };
            }
            catch (Exception)
            {

                throw;
            }

       
        }

        public async Task<Result> ActualizarProductoAsync(IList<Producto> productos)
        {
            try
            {
                for (int i = 0; i < productos.Count; i++)
                {
                    var producto = await this._context.producto.FindAsync(productos[i].Nombre);

                    if (producto is null)
                    {
                        return new Result() { StatusResult = 404, StatusMessage = "Producto No Existe" };
                    }

                    producto.CantidadUnidadesIventario = (productos[i].CantidadUnidadesIventario != productos[i].CantidadUnidadesIventario) ? productos[i].CantidadUnidadesIventario : producto.CantidadUnidadesIventario;

                    _context.producto.Update(producto);
                    await _context.SaveChangesAsync();
                }

                return new Result() { StatusResult = 200, StatusMessage = "Ok" };
            }
            catch (Exception ex)
            {
                // Codigo para loguear error
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
        }

        public async Task<Result> GuardarFacturaAsync(Factura factura)
        {
            try
            {
                var cliente = await this._context.cliente.FindAsync(factura.Clientes[0].NumeroIdentificacion);

                if (cliente is null) // si el cliente no existe se crea con la informacion que se diligencio
                {
                    _context.cliente.Add(factura.Clientes[0]);
                    cliente = await this._context.cliente.FindAsync(factura.Clientes[0].NumeroIdentificacion);
                }

                factura.Clientes[0] = cliente;
                _context.factura.Add(factura);
                await _context.SaveChangesAsync();

                //se actualiza la cantida del producto en el inventario
                await ActualizarProductoAsync(factura.productos);

                return new Result() { StatusResult = 200, StatusMessage = "Ok" };
            }
            catch (Exception ex)
            {
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
           
        }

        public async Task<Result> BuscarFacturaAsync(int numeroFactura)
        {
            try
            {
                var factura = await this._context.factura.FindAsync(numeroFactura);


                if (factura is null)
                {
                    return new Result() { StatusResult = 400, StatusMessage = "Factura No Existe" };
                }

                return new Result() { StatusResult = 200, StatusMessage = "Ok", Data = factura };
            }
            catch (Exception ex)
            {

                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
            
        }

        public async Task<Result> GuardarDetalleFacturaAsync(DetalleFactura detalleFactura)
        {
            try
            {
                var producto = await _context.producto.FindAsync(detalleFactura.Producto.Nombre);
                detalleFactura.Producto = producto;
                _context.detalleFactura.Add(detalleFactura);

                var factura = await _context.factura.FindAsync(detalleFactura.Factura.Id);
                detalleFactura.Factura = factura;
                _context.detalleFactura.Add(detalleFactura);
                return new Result() { StatusResult = 200, StatusMessage = "Ok" };
            }
            catch (Exception ex)
            {
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }

        }

        

    }
}
