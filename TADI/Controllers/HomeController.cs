using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Entidad;
using Datos;

namespace TADI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Prueba()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Registrar()
        {

            return View();
        }

        public ActionResult Recuperar()
        {

            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();
        }


        [HttpPost]

        public ActionResult Index(string correo, string clave)
        {
            Clientes oCliente = new Clientes();

            oCliente = new CN_Cliente().ListarCliente().Where(item => item.Correo == correo && item.Contraseña == CN_Recurso.ConverttirSha256(clave)).FirstOrDefault();

            if (oCliente == null)
            {
                ViewBag.Error = "Correo o contraseña no son correctas";
                return View();
            }
            else
            {
                if (oCliente.Restablecer)
                {
                    TempData["IdCliente"] = oCliente.id;
                    return RedirectToAction("CambiarClave", "Home");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(oCliente.Correo, false);
                    Session["Cliente"] = oCliente;
                    ViewBag.Error = null;
                    return RedirectToAction("Menu", "Home");
                }

            }
        }


        [HttpPost]
        public ActionResult Registrar(Clientes objeto)
        {
            int resultado;
            string mensaje = string.Empty;

            ViewData["Nombres"] = string.IsNullOrEmpty(objeto.Nombre) ? "" : objeto.Nombre;
            ViewData["Apellidos"] = string.IsNullOrEmpty(objeto.Apellido) ? "" : objeto.Apellido;
            ViewData["Correo"] = string.IsNullOrEmpty(objeto.Correo) ? "" : objeto.Correo;
            ViewData["Contraseña"] = string.IsNullOrEmpty(objeto.Contraseña) ? "" : objeto.Contraseña;
            ViewData["Sexo"] = objeto.sexo != 0 ? objeto.sexo : 0;


            resultado = new CN_Cliente().Registrar(objeto, out mensaje);

            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }

        }


        [HttpPost]
        public ActionResult Recuperar(string correo)
        {
            Clientes oCliente = new Clientes();
            oCliente = new CN_Cliente().ListarCliente().Where(item => item.Correo == correo).FirstOrDefault();
            if (oCliente == null)
            {
                ViewBag.Error = "No se encontro un Cliente relacionado con el correo";
                return View();
            }
            string mensaje = string.Empty;
            bool repuesta = new CN_Cliente().ReestablecerClave(oCliente.id, correo, out mensaje);
            if (repuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string IdCliente, string clave, string nuevaclave, string comfirmarclave)
        {
            Clientes oCliente = new Clientes();

            oCliente = new CN_Cliente().ListarCliente().Where(u => u.id == int.Parse(IdCliente)).FirstOrDefault();

            if (oCliente.Contraseña != CN_Recurso.ConverttirSha256(clave))
            {
                TempData["IdCliente"] = IdCliente;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es la correcta ";
                return View();
            }
            else if (nuevaclave != comfirmarclave)
            {
                TempData["IdCliente"] = IdCliente;
                ViewData["vclave"] = clave;
                ViewBag.Error = "Las contraseñas no coninciden ";
                return View();
            }
            ViewData["vclave"] = "";

            nuevaclave = CN_Recurso.ConverttirSha256(nuevaclave);
            string mensaje = string.Empty;

            bool respuesta = new CN_Cliente().CambiarClave(int.Parse(IdCliente), nuevaclave, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["IdCliente"] = IdCliente;
                ViewBag.Error = mensaje;
                return View();
            }
        }


    }
}