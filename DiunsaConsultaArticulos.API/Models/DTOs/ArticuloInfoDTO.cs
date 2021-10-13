using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiunsaConsultaArticulos.API.Models.DTOs
{
    public class ArticuloInfoDTO
    {
        public string tienda { get; set; }
        public string codigoArticulo { get; set; }
        public string descripcion { get; set; }
        public decimal precioNormal { get; set; }
        public decimal precioOferta { get; set; }
        public decimal precioAhorroMas { get; set; }
        public decimal precioCrediDiunsa { get; set; }
        public decimal cuotaCrediDiunsaNormal { get; set; }
        public decimal cuotaCrediDiunsaVIP { get; set; }
        public string imagenUrl { get; set; }
    }
}
