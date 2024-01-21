using Entidad_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CD_Dashboard
    {

        public Dashboard Listar()
        {
            try
            {
                TADIAdminEntities TADI = new TADIAdminEntities();
                var result = TADI.sp_ReporteDashboard().FirstOrDefault();

                Dashboard objDashBoard = new Dashboard();
                objDashBoard.TotalCliente = Convert.ToInt32(result.TotalCliente);
                objDashBoard.TotalVenta = Convert.ToInt32(result.TotalVenta);
                objDashBoard.TotalProducto = Convert.ToInt32(result.TotalProducto);

                return objDashBoard;
            }
            catch
            {
                return null;
            }
        }




        public List<Reporte> ListarReporte(string fechaInicio, string fechaFin, string idTransaccion)
        {
            try
            {
                using (var TADI = new TADIAdminEntities()) // Replace "YourEntities" with the name of your EF context
                {
                    var result = TADI.sp_ReporteVentas(fechaInicio, fechaFin, idTransaccion).ToList();

                    // Convert the result to a list of ReporteobjReporte objects
                    List<Reporte> objListaReportes = new List<Reporte>();
                    foreach (var miObjeto in result)
                    {

                        Reporte objReporte = new Reporte();
                        objReporte.FechaVenta = miObjeto.FechaVenta;
                        objReporte.Cliente = miObjeto.Cliente;
                        objReporte.Producto = miObjeto.Producto;
                        objReporte.Precio = Convert.ToDecimal(miObjeto.Precio);
                        objReporte.Cantidad = Convert.ToInt32(miObjeto.Cantidad);
                        objReporte.Total = Convert.ToDecimal(miObjeto.Total);
                        objReporte.IdTransaccion = miObjeto.IdTransaccion;
                        objListaReportes.Add(objReporte);
                    }

                    return objListaReportes;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
