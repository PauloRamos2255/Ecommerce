using Datos;
using Entidad_;
using Negocios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;

namespace Adnistrador.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }


        public ActionResult Reestablecer()
        {
            return View();
        }

        public ActionResult OtraForma()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuarios oUsuario = new Usuarios();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.correo == correo && u.contraseña == CN_Recurso.ConverttirSha256(clave)).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña no correcto";
                return View();
            }
            else if (oUsuario.activo == false)
            {
                ViewBag.Error = "Su usuario esta inactivo";
                return View();
            }
            else
            {
                if (oUsuario.restablecer)
                {
                    TempData["IdUsuario"] = oUsuario.id;
                    return RedirectToAction("CambiarClave");
                }

                FormsAuthentication.SetAuthCookie(oUsuario.correo, false);

                ViewBag.Error = null;
                Session["IdRoles"] = oUsuario.id_roles;
                if(oUsuario.id_roles == 1)
                {
                    return RedirectToAction("Dashboard", "Administrador");
                }
                else
                {
                    return RedirectToAction("Categoria", "Home");
                }
               
            }

        }


        [HttpPost]
        public ActionResult CambiarClave(string IdUsuario, string clave, string nuevaclave, string comfirmarclave)
        {
            Usuarios oUsuario = new Usuarios();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.id == int.Parse(IdUsuario)).FirstOrDefault();

            if (oUsuario.contraseña != CN_Recurso.ConverttirSha256(clave))
            {
                TempData["IdUsuario"] = IdUsuario;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es la correcta ";
                return View();
            }
            else if (nuevaclave != comfirmarclave)
            {
                TempData["IdUsuario"] = IdUsuario;
                ViewData["vclave"] = clave;
                ViewBag.Error = "Las contraseñas no coninciden ";
                return View();
            }
            ViewData["vclave"] = "";

            nuevaclave = CN_Recurso.ConverttirSha256(nuevaclave);
            string mensaje = string.Empty;

            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(IdUsuario), nuevaclave, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                TempData["IdUsuario"] = IdUsuario;
                ViewBag.Error = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Usuarios ousuario = new Usuarios();
            ousuario = new CN_Usuarios().Listar().Where(item => item.correo == correo).FirstOrDefault();
            if (ousuario == null)
            {
                ViewBag.Error = "No se encontro un usuario relacionado con el correo";
                return View();
            }
            string mensaje = string.Empty;
            bool repuesta = new CN_Usuarios().ReestablecerClave(ousuario.id, correo, out mensaje);
            if (repuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }


        private CD_Usuarios OBJCapaDato = new CD_Usuarios();

        [HttpPost]
        public ActionResult OtraForma(string numero) 
        {

            Usuarios ousuario = new Usuarios();
            ousuario = new CN_Usuarios().Listar().Where(item => item.numero == numero).FirstOrDefault();
            if (ousuario == null)
            {
                ViewBag.Error = "No se encontro un usuario relacionado con ese número";
                return View();
            }
            if(ousuario.numero != numero)
            {
                ViewBag.Error = "No se encontro un numero para enviar el mensaje";
            }


            string Mensaje = string.Empty;


            string mensaje = CN_Recurso.GnerarClave();
            string Mensajecorreo = "Supermercado TADI le comunica que su contrseña fue actulizada correctamente para acceder al sistema su nuevo contraseña es : !clave!";
            Mensajecorreo = Mensajecorreo.Replace("!clave!", mensaje);


            // Obtén las claves de API de tu archivo de configuración web.config
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["InfobipApiKey"];

            try
            {
                // URL de la API de Infobip para enviar mensajes de texto
                string apiUrl = "https://api.infobip.com/sms/1/text/single";

                // Crea los datos del mensaje
                string postData = $"{{ \"from\": \"SENDER_ID\", \"to\": \"{"+51"+ numero}\", \"text\": \"{Mensajecorreo}\" }}";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Crea la solicitud HTTP POST
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "App " + apiKey); // Agrega la clave de API en la cabecera de autorización

                // Escribe los datos del mensaje en la solicitud
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                // Obtiene la respuesta de la API de Infobip
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            string result = reader.ReadToEnd();
                            // Puedes realizar el manejo de la respuesta aquí si lo necesitas
                
                            if (result != "")
                            {
                                bool resultado = new CN_Usuarios().EnvioSMS(ousuario.id, CN_Recurso.ConverttirSha256(mensaje), out Mensaje);
                                if (resultado == false)
                                {
                                    ViewBag.Error = mensaje;
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.Resultado = "No se pudo enviar el mensaje de texto.";
                                return View();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Si ocurre un error, muestra el mensaje de error en la vista
                ViewBag.Resultado = "Error: " + ex.Message;
                return View();
            }

            return RedirectToAction("Index", "Acceso");
        }


    }
}