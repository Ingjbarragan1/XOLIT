using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Models;

namespace XOLIT.API.DTOs
{
    public class ADDDetalleFactura
    {
        public List<INFOProductos> Productos { get; set; }
        public int CantidadUnidades { get; set; }
        public decimal ValorUnitarioSinIVA { get; set; }
        public decimal valorUnitarioconIVA { get; set; }
        public decimal ValorTotalCompra { get; set; }
    }
}
