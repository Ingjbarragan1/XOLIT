
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Common;
using XOLIT.API.Database;
using XOLIT.API.DTOs;
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

        public async Task<Result> AgregarClienteAsync(ADDCliente infoCliente)
        {
            try
            {
                var isInvalidCliente = string.IsNullOrWhiteSpace(infoCliente.Nombre) ||
                                    string.IsNullOrWhiteSpace(infoCliente.Direccion) ||
                                    string.IsNullOrWhiteSpace(infoCliente.Direccion) ||
                                    infoCliente.NumeroIdentificacion == 0 ||
                                    string.IsNullOrWhiteSpace(infoCliente.Telefono);
                if (isInvalidCliente)
                {
                    return new Result() { StatusResult = 400, StatusMessage = "parametros invalidos" };
                }

                var cliente = new Cliente()
                {
                    Nombre = infoCliente.Nombre,
                    Apellido = infoCliente.Apellido,
                    NumeroIdentificacion = infoCliente.NumeroIdentificacion,
                    Direccion = infoCliente.Direccion,
                    Telefono = infoCliente.Telefono
                };

                _context.cliente.Add(cliente);
                await _context.SaveChangesAsync();

                return new Result() { StatusResult = 200, StatusMessage = "Ok" };
            }
            catch (Exception ex)
            {
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }

        }

        public async Task<Result> BuscarClienteAsync(int numeroIdentificacion)
        {
            try
            {
                var querable = this._context.cliente.AsQueryable();

                if (!numeroIdentificacion.Equals(0))
                {
                    querable = querable.Where(x => x.NumeroIdentificacion.Equals(numeroIdentificacion));
                }

                var cliente = await querable.Select(x => new Cliente()
                {
                    Id = x.Id,
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

        public async Task<Result> BuscarProductoAsync(int idProducto, string nombreProducto)
        {
            try
            {
                var querable = this._context.producto.AsQueryable();

                if (!string.IsNullOrWhiteSpace(nombreProducto))
                {
                    querable = querable.Where(x => x.Nombre.Contains(nombreProducto));
                }

                if (!idProducto.Equals(0))
                {
                    querable = querable.Where(x => x.Id.Equals(idProducto));
                }


                var producto = await querable.Select(x => new GETProducto()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    ValorVentaConIVA = x.ValorVentaConIVA,
                    CantidadUnidadesIventario = x.CantidadUnidadesIventario,
                    PorcentajeIVAAplicado = x.PorcentajeIVAAplicado
                }).ToListAsync();

                return new Result() { StatusResult = 200, StatusMessage = "Ok", Data = producto };
            }
            catch (Exception ex)
            {
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }


        }

        public async Task<Result> ActualizarProductoAsync(Producto infoProducto)
        {
            try
            { 
                _context.producto.Update(infoProducto);
                await _context.SaveChangesAsync();


                return new Result() { StatusResult = 200, StatusMessage = "Ok" };
            }
            catch (Exception ex)
            {
                // Codigo para loguear error
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
        }

        public async Task<Result> GuardarFacturaAsync(ADDFactura InfoFactura)
        {
            try
            {
                var querable = this._context.cliente.AsQueryable();

                if (InfoFactura.cliente.NumeroIdentificacion != 0)
                {
                    querable = querable.Where(x => x.NumeroIdentificacion.Equals(InfoFactura.cliente.NumeroIdentificacion));
                }

                var cliente = await querable.Select(x => new Cliente()
                {
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    NumeroIdentificacion = x.NumeroIdentificacion,
                    Direccion = x.Direccion,
                    Telefono = x.Telefono
                }).ToListAsync();

                if (cliente.Count <= 0) // si el cliente no existe se crea con la informacion que se diligencio
                {
                    var newCliente = new Cliente()
                    {
                        Nombre = InfoFactura.cliente.Nombre,
                        Apellido = InfoFactura.cliente.Apellido,
                        NumeroIdentificacion = InfoFactura.cliente.NumeroIdentificacion,
                        Direccion = InfoFactura.cliente.Direccion,
                        Telefono = InfoFactura.cliente.Telefono
                    };

                    _context.cliente.Add(newCliente);
                    await _context.SaveChangesAsync();
                }

                //se consulta el ID del cliente para ingresarlo a la factura
                var CLienteBD = this._context.cliente.AsQueryable();

                if (!InfoFactura.cliente.NumeroIdentificacion.Equals(0))
                {
                    CLienteBD = CLienteBD.Where(x => x.NumeroIdentificacion.Equals(InfoFactura.cliente.NumeroIdentificacion));
                }

                var idcliente = await CLienteBD.Select(x => new Cliente()
                {
                    Id = x.Id,
                }).ToListAsync();



                var factura = new Factura()
                {
                    FechaVenta = DateTime.Now,
                    TotalPrecioVenta = InfoFactura.TotalPrecioVenta,
                    SubTotalSinIVA = InfoFactura.SubTotalSinIVA,
                    FechaEntrega = InfoFactura.FechaEntrega,
                    ClienteId = idcliente[0].Id

                };


                _context.factura.Add(factura);
                await _context.SaveChangesAsync();

                //Guardar el detalle de la factura
                for (int i = 0; i < InfoFactura.detalleFactura.Count; i++)
                {
                    var editProducto = this._context.producto.AsQueryable();

                    if (InfoFactura.detalleFactura[i].Producto.Id != 0)
                    {
                        editProducto = editProducto.Where(x => x.Id.Equals(InfoFactura.detalleFactura[i].Producto.Id));
                    }

                    var productoBD = await editProducto.Select(x => new Producto()
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        ValorVentaConIVA = x.ValorVentaConIVA,
                        CantidadUnidadesIventario = x.CantidadUnidadesIventario,
                        PorcentajeIVAAplicado = x.PorcentajeIVAAplicado
                    }).ToListAsync();

                    decimal valorProductoSinIVA = productoBD[i].ValorVentaConIVA - (productoBD[i].ValorVentaConIVA * productoBD[i].PorcentajeIVAAplicado);

                    var detallefactura = new DetalleFactura()
                    {
                        ProductoId = InfoFactura.detalleFactura[i].Producto.Id,
                        CantidadUnidades = InfoFactura.detalleFactura[i].CantidadProducto,
                        ValorUnitarioSinIVA = valorProductoSinIVA,
                        valorUnitarioconIVA = productoBD[i].ValorVentaConIVA,
                        ValorTotalCompra = InfoFactura.TotalPrecioVenta

                    };

                    await GuardarDetalleFacturaAsync(detallefactura);

                    //actualiza la cantidad del producto en el inventario                  

                    

                    int cantidadProductoInventario = productoBD[0].CantidadUnidadesIventario;

                    var infoProducto = new Producto()
                    {
                        Id = productoBD[0].Id,
                        Nombre = productoBD[0].Nombre,
                        ValorVentaConIVA = productoBD[0].ValorVentaConIVA,
                        CantidadUnidadesIventario = productoBD[0].CantidadUnidadesIventario - InfoFactura.detalleFactura[i].CantidadProducto,
                        PorcentajeIVAAplicado = productoBD[0].PorcentajeIVAAplicado
                    };

                    await ActualizarProductoAsync(infoProducto);
                }

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
                _context.detalleFactura.Add(detalleFactura);
                await _context.SaveChangesAsync();
                return new Result() { StatusResult = 200, StatusMessage = "Ok" };
            }
            catch (Exception ex)
            {
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }

        }

    }
}
