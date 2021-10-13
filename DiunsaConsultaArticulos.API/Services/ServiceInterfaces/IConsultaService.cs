using DiunsaConsultaArticulos.API.Models;
using DiunsaConsultaArticulos.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiunsaConsultaArticulos.API.Services.ServiceInterfaces
{
    public interface IConsultaService
    {
        ArticuloInfoDTO CosultaArticulo(string barCode, string macAddress);
    }
}
