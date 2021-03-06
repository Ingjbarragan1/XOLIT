using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XOLIT.API.Models
{
    public class DetalleFactura
    {
        public int id { get; set; }        
        public int CantidadUnidades { get; set; }
        public decimal ValorUnitarioSinIVA { get; set; }
        public decimal valorUnitarioconIVA { get; set; }
        public decimal ValorTotalCompra { get; set; }        
        public int ProductoId { get; set; }
        public IList<Factura> factura { get; set; }
    }
}
