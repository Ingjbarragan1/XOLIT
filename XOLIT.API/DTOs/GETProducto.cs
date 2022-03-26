using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XOLIT.API.DTOs
{
    public class GETProducto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal ValorVentaConIVA { get; set; }

        public int CantidadUnidadesIventario { get; set; }

        public decimal PorcentajeIVAAplicado { get; set; }
    }
}
