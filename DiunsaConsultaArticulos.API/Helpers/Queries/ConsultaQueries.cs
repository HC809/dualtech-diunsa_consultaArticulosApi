using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiunsaConsultaArticulos.API.Helpers.Queries
{
    public class ConsultaQueries
    {
        public const string InfoArticulo = @"select
	                                            dp.TIENDA tienda,
	                                            ra.CODIGO_ARTICULO codigoArticulo,
	                                            ra.DESCRIPCION descripcion,
	                                            sp.[Unit Price Including VAT] precioNormal,
	                                            COALESCE(ofn.PRECIO_OFERTA_VAT,0) precioOferta,
	                                            COALESCE(am.PRECIO_OFERTA_VAT,0) precioAhorroMas,
	                                            COALESCE(cred.PRECIO_OFERTA_VAT,0) precioCrediDiunsa
                                            from CONS_ResumenArticulos ra
	                                            OUTER APPLY (SELECT TIENDA FROM RBOINTERFACES.dbo.DISPOSITIVOS_TIENDA dp WHERE dp.MAC_ADDRESS = @macAddress) dp
	                                            left join LSRETAIL01A.dbo.[DIUNSA$Sales Price] sp on ra.CODIGO_ARTICULO = sp.[Item No_]
	                                            left join RBOINTERFACES.dbo.OFERTAS_NAV ofn on sp.[Item No_] = ofn.ITEMID collate SQL_Latin1_General_CP1_CI_AS and ofn.TIENDA = dp.TIENDA
	                                            left join RBOINTERFACES.dbo.OFERTAS_NAV_AM am on sp.[Item No_] = am.ITEMID collate SQL_Latin1_General_CP1_CI_AS and am.TIENDA = dp.TIENDA
	                                            left join RBOINTERFACES.dbo.OFERTAS_NAV_VAP cred on sp.[Item No_] = cred.ITEMID collate SQL_Latin1_General_CP1_CI_AS and cred.TIENDA = dp.TIENDA
                                            where (CODIGO_BARRA = @barCode and sp.[Sales Code] = 'PV');";

        //public const string PrecioOferta = @"SELECT PRECIO_OFERTA_VAT precioOferta 
        //	FROM RBOINTERFACES.dbo.OFERTAS_NAV 
        //	WHERE ITEMID = @itemId AND TIENDA = @codigoTienda";

        //public const string PrecioAhorroMas = @"SELECT PRECIO_OFERTA_VAT precioAhorroMas 
        //	FROM RBOINTERFACES.dbo.OFERTAS_NAV_AM 
        //	WHERE ITEMID = @itemId AND TIENDA = @codigoTienda";

        //public const string PrecioCrediDiunsa = @"SELECT PRECIO_OFERTA_VAT precioCrediDiunsa 
        //	FROM RBOINTERFACES.dbo.OFERTAS_NAV_VAP 
        //	WHERE ITEMID = @itemId AND TIENDA = @codigoTienda";

        public const string Imagen = @"select top 1 url from JUPITER.dbo.vst_imagenesArticulos where TRIM(codigobarra) = @barCode;";
    }
}
