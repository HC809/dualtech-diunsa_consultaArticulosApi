using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiunsaConsultaArticulos.API.Models
{
    public class ArticuloInfo
    {
        public string tienda { get; set; }
        public string codigoArticulo { get; set; }
        public string descripcion { get; set; }
        public decimal precioNormal { get; set; }
        public decimal precioOferta { get; set; }
        public decimal precioAhorroMas { get; set; }
        public decimal precioCrediDiunsa { get; set; }
    }
}
