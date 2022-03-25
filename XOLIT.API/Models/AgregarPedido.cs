using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XOLIT.API.Models
{
    public class Pedido
    {
        public IList<Producto> productos { get; set; }
        public Cliente cliente { get; set; }

    }
}
