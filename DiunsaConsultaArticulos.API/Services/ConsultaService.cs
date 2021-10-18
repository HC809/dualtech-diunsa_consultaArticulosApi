using AutoMapper;
using Dapper;
using DiunsaConsultaArticulos.API.Helpers.Queries;
using DiunsaConsultaArticulos.API.Models;
using DiunsaConsultaArticulos.API.Models.DTOs;
using DiunsaConsultaArticulos.API.Services.ServiceInterfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DiunsaConsultaArticulos.API.Services
{
    public class ConsultaService : IConsultaService
    {
        private IConfiguration _configuration;
        private IMapper _mapper;

        public ConsultaService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("ConsultaDB"));
        }

        public ArticuloInfoDTO CosultaArticulo(string barCode, string macAddress)
        {
            double tasaInteresNormal = 0.0708;
            double tasaInteresVIP = 0.0407;

            ArticuloInfoDTO consultaArticulo = new ArticuloInfoDTO();

            var tasks = new List<Task>()
                 {
                     GetArticuloInfo(barCode, macAddress),
                     GetArticuloImagenUrl(barCode),
                 };

            var completedTasks = Task.WhenAll(tasks.ToArray());

            completedTasks.Wait();

            consultaArticulo = ((Task<ArticuloInfoDTO>)tasks[0]).Result;

            if (consultaArticulo != null)
            {
                consultaArticulo.imagenUrl = ((Task<string>)tasks[1]).Result;

                decimal precioParaCuota = consultaArticulo.precioOferta > 0 ? consultaArticulo.precioOferta : consultaArticulo.precioNormal;

                consultaArticulo.cuotaCrediDiunsaNormal = Math.Round(GetCuotaCrediDiunsa(precioParaCuota, tasaInteresNormal), 2);
                consultaArticulo.cuotaCrediDiunsaVIP = Math.Round(GetCuotaCrediDiunsa(precioParaCuota, tasaInteresVIP), 2);

                if (consultaArticulo.precioCrediDiunsa == 0) consultaArticulo.precioCrediDiunsa = precioParaCuota;
            }

            return consultaArticulo;
        }

        public ArticuloInfoDTO CosultaPrecios(string barCode)
        {
            double tasaInteresNormal = 0.0708;
            double tasaInteresVIP = 0.0407;

            ArticuloInfoDTO consultaArticulo = new ArticuloInfoDTO();


            var tasks = new List<Task>()
                 {
                     GetArticuloPrecios(barCode),
                     GetArticuloImagenUrl(barCode),
                 };

            var completedTasks = Task.WhenAll(tasks.ToArray());

            completedTasks.Wait();

            consultaArticulo = ((Task<ArticuloInfoDTO>)tasks[0]).Result;

            if (consultaArticulo != null)
            {
                consultaArticulo.tienda = "T01";
                consultaArticulo.imagenUrl = ((Task<string>)tasks[1]).Result;

                decimal precioParaCuota = consultaArticulo.precioOferta > 0 ? consultaArticulo.precioOferta : consultaArticulo.precioNormal;

                consultaArticulo.cuotaCrediDiunsaNormal = Math.Round(GetCuotaCrediDiunsa(precioParaCuota, tasaInteresNormal), 2);
                consultaArticulo.cuotaCrediDiunsaVIP = Math.Round(GetCuotaCrediDiunsa(precioParaCuota, tasaInteresVIP), 2);

                if (consultaArticulo.precioCrediDiunsa == 0) consultaArticulo.precioCrediDiunsa = precioParaCuota;
            }

            return consultaArticulo;
        }

        private async Task<ArticuloInfoDTO> GetArticuloInfo(string barCode, string macAddress)
        {
            IDbConnection db = GetDbconnection();
            ArticuloInfoDTO articuloInfo;

            using (db)
            {
                db.Open();
                var response = await db.QueryFirstOrDefaultAsync<ArticuloInfo>(ConsultaQueries.InfoArticulo, new { barCode, macAddress });
                articuloInfo = _mapper.Map<ArticuloInfo, ArticuloInfoDTO>(response);
                db.Close();
            }

            return articuloInfo;
        }

        private async Task<ArticuloInfoDTO> GetArticuloPrecios(string barCode)
        {
            IDbConnection db = GetDbconnection();
            ArticuloInfoDTO articuloInfo;

            using (db)
            {
                db.Open();
                var response = await db.QueryFirstOrDefaultAsync<ArticuloInfo>(ConsultaQueries.ConsultaPrecios, new { barCode });
                articuloInfo = _mapper.Map<ArticuloInfo, ArticuloInfoDTO>(response);
                db.Close();
            }

            return articuloInfo;
        }

        private async Task<string> GetArticuloImagenUrl(string barCode)
        {
            IDbConnection db = GetDbconnection();
            string url;

            using (db)
            {
                db.Open();
                url = await db.QueryFirstOrDefaultAsync<string>(ConsultaQueries.Imagen, new { barCode });
                db.Close();
            }

            return url;
        }

        private decimal GetCuotaCrediDiunsa(decimal precio, double tasaInteres, int meses = 36)
        {
            precio = precio * 0.7m; //Le quitamos el 30% de prima
            double PV1 = (double)precio * -1;
            double RATE = tasaInteres;//Tasa de Interes
            long FV = 0;
            int TYPE = 0;
            decimal seguro = (decimal)0.0051;
            decimal percentSum = seguro * Math.Abs(precio);
            decimal result = Convert.ToDecimal(-RATE * (FV + PV1 * Math.Pow(1 + RATE, meses)) / ((Math.Pow(1 + RATE, meses) - 1) * (1 + RATE * TYPE)));

            return result + percentSum;
        }

        //private async Task<double> GetArticuloOfertas(string barCode)
        //{
        //    IDbConnection db = GetDbconnection();
        //    List<OfertaDTO> ofertas;

        //    using (db)
        //    {
        //        db.Open();
        //        var response = await db.QueryAsync<Oferta>(ConsultaQueries.Ofertas, new { barCode });
        //        ofertas = _mapper.Map<List<Oferta>, List<OfertaDTO>>(response.ToList());
        //        db.Close();
        //    }

        //    return ofertas.Select(c => c.precioOfertaConIsv).FirstOrDefault();
        //}
    }
}
