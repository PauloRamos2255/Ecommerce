using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class CN_Cliente
    {
        CD_Cliente objCliente = new CD_Cliente();

        public List<Clientes> ListarCliente()
        {
            return objCliente.ListarCliente();
        }

        public int Registrar(Clientes obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del Cliente no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                Mensaje = "El apellido del Cliente no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo del Cliente no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Contraseña) || string.IsNullOrWhiteSpace(obj.Contraseña))
            {
                Mensaje = "Ingrese una nueva contrseña";
            }
            else if (string.IsNullOrEmpty(obj.ConfirmarClave) || string.IsNullOrWhiteSpace(obj.ConfirmarClave))
            {
                Mensaje = "Vuelva a colocar su contrseña";
            }
            else if (obj.sexo == 0)
            {
                Mensaje = "Seleccione su sexo";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                //string clase = CN_Recursos.GnerarClave();
                string asunto = "Creacion de Cuenta";
                string Mensajecorreo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es : !clave!</p>";
                Mensajecorreo = Mensajecorreo.Replace("!clave!", obj.Contraseña);
                bool respuesta = CN_Recurso.EnviarCorreo(obj.Correo, asunto, Mensajecorreo);

                if (respuesta)
                {
                    obj.Contraseña = CN_Recurso.ConverttirSha256(obj.Contraseña);
                    return objCliente.Registrar(obj, out Mensaje);
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

        public bool ReestablecerClave(int IdCliente, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuvaclase = CN_Recurso.GnerarClave();
            bool resultado = objCliente.ReestablecerClave(IdCliente, CN_Recurso.ConverttirSha256(nuvaclase), out Mensaje);

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

        public bool CambiarClave(int IdCliente, string Nuevaclave, out string Mensaje)
        {
            return objCliente.CambiarClave(IdCliente, Nuevaclave, out Mensaje);
        }

    }
}
