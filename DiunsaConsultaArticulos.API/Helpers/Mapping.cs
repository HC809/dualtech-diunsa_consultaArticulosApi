using AutoMapper;
using DiunsaConsultaArticulos.API.Models;
using DiunsaConsultaArticulos.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiunsaConsultaArticulos.API.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<double, double>().ConvertUsing(x => Math.Round(x, 2));

            CreateMap<ArticuloInfo, ArticuloInfoDTO>();

            CreateMap<Oferta, OfertaDTO>()
            .ForMember(d => d.tienda, o => o.MapFrom(s => s.DESCRIPCION))
            .ForMember(d => d.promocion, o => o.MapFrom(s => s.DESCRIPCION_PROMOCION))
            .ForMember(d => d.precioOferta, o => o.MapFrom(s => s.PRECIO_OFERTA))
            .ForMember(d => d.precioOfertaConIsv, o => o.MapFrom(s => s.PRECIO_OFERTA_ISV));
        }
    }
}
