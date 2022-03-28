using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XOLIT.API.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int NumeroIdentificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public IList<Factura> factura { get; set; }

    }
}
