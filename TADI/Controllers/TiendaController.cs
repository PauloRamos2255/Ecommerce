using Entidad_;
using Negocios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TADI.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListarProducto(int IdCategoria, int IdMarca)
        {
            List<Producto> lista = new List<Producto>();
            bool conversion;


            lista = new CN_Producto().Listar()
            .Where(p => (IdCategoria == 0 || p.IdCategoria == IdCategoria) &&
                (IdMarca == 0 || p.IdMarca == IdMarca) &&
                p.Stock > 0 && p.Activo)
                .Select(p => new Producto
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    IdMarca = p.IdMarca,
                    IdCategoria = p.IdCategoria,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    RutaImagen = p.RutaImagen,
                    Base64 = CN_Recurso.CovertirBase64(Path.Combine(p.RutaImagen, p.NombreImagen), out conversion),
                    Extension = Path.GetExtension(p.NombreImagen),
                    Activo = p.Activo
                })
           .ToList();
            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;

        }

    }
}