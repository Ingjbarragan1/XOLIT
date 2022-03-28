using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOLIT.API.Models;

namespace XOLIT.API.DTOs
{
    public class ADDFactura
    {
        public decimal TotalPrecioVenta { get; set; }
        public decimal SubTotalSinIVA { get; set; }
        public DateTime FechaEntrega { get; set; }
        public ADDCliente cliente { get; set; }
        public List<ADDDetalleFactura> detalleFactura { get; set; }
    }
}
