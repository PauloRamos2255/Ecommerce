using ClosedXML.Excel;
using Entidad;
using Entidad_;
using Negocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Adnistrador.Controllers
{
    [Authorize]
    public class AdministradorController : Controller
    {
        // GET: Administrador

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListaRol()
        {
            List<Rol> oLista = new List<Rol>();
            oLista = new CN_Roles().ListarRol();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarUsuario()
        {
            List<Usuarios> oLista = new List<Usuarios>();
            oLista = new CN_Usuarios().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuarios objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.id == 0)
            {
                resultado = new CN_Usuarios().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Usuarios().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult ListarReporte()
        {
            Dashboard oLista = new CN_Dashboard().Listar();
            return Json(new { resultado = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarReporteVenta(string fechainicio, string fechafin, string transaccion)
        {
            List<Reporte> oLista = new List<Reporte>();
            oLista = new CN_Dashboard().ListarReporte(fechainicio, fechafin, transaccion);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportarVenta(string fechainicio, string fechafin, string transaccion)
        {
            List<Reporte> oLista = new List<Reporte>();
            oLista = new CN_Dashboard().ListarReporte( fechainicio,  fechafin,  transaccion);
            DataTable dt = new DataTable();

            dt.Locale = new System.Globalization.CultureInfo("es-PE");
            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("IdTransaccion", typeof(string));

            foreach(Reporte rp in oLista)
            {
                dt.Rows.Add(new object[]
                {
                    rp.FechaVenta,
                    rp.Cliente,
                    rp.Producto,
                    rp.Precio,
                    rp.Cantidad,
                    rp.Total,
                    rp.IdTransaccion
                });
            }

            dt.TableName = "Datos";

            using(XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using(MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");
                }
            }



        }



    }
}