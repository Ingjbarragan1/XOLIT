using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XOLIT.API.DTOs
{
    public class ADDCliente
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int NumeroIdentificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
