using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Models;

namespace XOLIT.API.DTOs
{
    public class ADDDetalleFactura
    {
        public INFOProductos Producto { get; set; }
        public int CantidadProducto { get; set; }
    }
}
