using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XOLIT.API.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal TotalPrecioVenta { get; set; }
        public decimal SubTotalSinIVA { get; set; }
        public DateTime FechaEntrega { get; set; }
        public IList<Cliente> Clientes { get; set; }
        public IList<Producto> productos { get; set; }

    }
}
