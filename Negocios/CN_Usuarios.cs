using Datos;
using Entidad_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class CN_Usuarios
    {

        private CD_Usuarios OBJCapaDato = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return OBJCapaDato.ListarUsuarios();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.apellido) || string.IsNullOrWhiteSpace(obj.apellido))
            {
                Mensaje = "El apellido del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                Mensaje = "El correo del usuario no puede ser vacio";
            }
            else if (obj.id_roles ==  0 )
            {
                Mensaje = "Seleccione un rol";
            }
            else
            {
                // Aquí hacemos la validación para verificar si el correo ya está registrado
                List<Usuario> usuarios = OBJCapaDato.ListarUsuarios();
                bool correoRegistrado = usuarios.Any(u => u.correo == obj.correo);

                if (correoRegistrado)
                {
                    Mensaje = "El correo ya está registrado para otro usuario";
                }
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                string clase = CN_Recurso.GnerarClave();
                string asunto = "Creacion de Cuenta";
                string Mensajecorreo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es : !clave!</p>";
                Mensajecorreo = Mensajecorreo.Replace("!clave!", clase);
                bool respuesta = CN_Recurso.EnviarCorreo(obj.correo, asunto, Mensajecorreo);

                if (respuesta)
                {
                    obj.contraseña = CN_Recurso.ConverttirSha256(clase);
                    return OBJCapaDato.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
           

        }


        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.apellido) || string.IsNullOrWhiteSpace(obj.apellido))
            {
                Mensaje = "El apellido del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                Mensaje = "El correo del usuario no puede ser vacio";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {

                return OBJCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return OBJCapaDato.Eliminar(id, out Mensaje);
        }


        public bool CambiarClave(int IdUsuario, string Nuevaclave, out string Mensaje)
        {
            return OBJCapaDato.CambiarClave(IdUsuario, Nuevaclave, out Mensaje);
        }


        public bool EnvioSMS(int IdUsuario, string Nuevaclave, out string Mensaje)
        {
            return OBJCapaDato.ReestablecerClave(IdUsuario, Nuevaclave, out Mensaje);
        }


        public bool ReestablecerClave(int IdUsuario, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuvaclase = CN_Recurso.GnerarClave();
            bool resultado = OBJCapaDato.ReestablecerClave(IdUsuario, CN_Recurso.ConverttirSha256(nuvaclase), out Mensaje);

            if (resultado)
            {
                string asunto = "Restablecimiento de Contraseña";
                string Mensajecorreo = "<h3>Su cuenta fue restablecida correctamente</h3></br><p>Su contraseña para acceder ahoora es : !clave!</p>";
                Mensajecorreo = Mensajecorreo.Replace("!clave!", nuvaclase);
                bool respuesta = CN_Recurso.EnviarCorreo(correo, asunto, Mensajecorreo);
                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                return false;
            }


        }


    }
}
