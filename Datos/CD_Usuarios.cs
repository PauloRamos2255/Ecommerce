using Entidad_;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CD_Usuarios
    {

        public List<Usuarios> ListarUsuarios()
        {
            try
            {
                List<Usuarios> Lista = new List<Usuarios>();
                ecommerce2024Entities Tadi = new ecommerce2024Entities();
                var query = Tadi.USUARIO.OrderBy(micarrito => micarrito.IdUsuario);
                foreach (var miObejto in query)
                {
                    Usuarios obj = new Usuarios();
                    obj.id = miObejto.IdUsuario;
                    obj.nombre = miObejto.Nombres;
                    obj.apellido = miObejto.Apellidos;
                    obj.id_roles = Convert.ToInt32(miObejto.idRol);
                    obj.correo = miObejto.Correo;
                    obj.contraseña = miObejto.Clave;
                    obj.numero = miObejto.numero;
                    obj.restablecer = Convert.ToBoolean(miObejto.Reestablecer);
                    obj.activo= Convert.ToBoolean(miObejto.Activo);
       
                    Lista.Add(obj);
                }
                return Lista;
            }
            catch
            {
                List<Usuarios> error = new List<Usuarios>();
                return error;
            }
        }


        public int Registrar(Usuarios obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (var db = new ecommerce2024Entities())
                {
                    var resultado = new ObjectParameter("Resultado", typeof(int));
                    var mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.sp_RegistrarUsuario(
                         obj.nombre, obj.apellido, obj.correo, obj.contraseña, obj.activo, obj.id_roles, obj.numero, mensaje, resultado );
                    idautogenerado = (int)resultado.Value;
                    Mensaje = (string)mensaje.Value;
                }
            }
            catch (Exception x)
            {
                idautogenerado = 0;
                Mensaje = x.Message;
            }

            return idautogenerado;
        }

        public bool Editar(Usuarios obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (var db = new ecommerce2024Entities())
                {
                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(bool));

                    db.sp_EditarUsuario(obj.id, obj.nombre, obj.apellido, obj.correo,  obj.activo, obj.id_roles ,obj.numero, mensajeParam, resultadoParam);

                    Mensaje = mensajeParam.Value.ToString();
                    resultado = (bool)resultadoParam.Value;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }


        public bool Eliminar(int idUsuario, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (var db = new ecommerce2024Entities())
                {
                    var usuarioAEliminar = db.USUARIO.FirstOrDefault(u => u.IdUsuario == idUsuario);
                    if (usuarioAEliminar != null)
                    {
                        db.USUARIO.Remove(usuarioAEliminar);
                        db.SaveChanges();
                        resultado = true;
                        mensaje = "Usuario eliminado correctamente";
                    }
                    else
                    {
                        mensaje = "No se encontró el usuario a eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Ocurrió un error al eliminar el usuario: " + ex.Message;
            }

            return resultado;
        }




        public bool CambiarClave(int IdUsuario, string Nuevaclave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(Nuevaclave))
                {
                    throw new ArgumentException("La nueva clave no puede estar vacía");
                }

                using (ecommerce2024Entities carrito = new ecommerce2024Entities())
                {
                    USUARIO Usuario = carrito.USUARIO.FirstOrDefault(u => u.IdUsuario == IdUsuario);
                    if (Usuario == null)
                    {
                        throw new ArgumentException($"No se encontró ningún usuario con el Id {IdUsuario}");
                    }

                    Usuario.Clave = Nuevaclave;
                    Usuario.Reestablecer = false;

                    if (carrito.SaveChanges() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo actualizar la clave del usuario";
                    }
                }
            }
            catch (Exception x)
            {
                resultado = false;
                Mensaje = x.Message;
            }
            return resultado;
        }


        public bool ReestablecerClave(int IdUsuario, string clave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    throw new ArgumentException("La nueva clave no puede estar vacía");
                }

                using (ecommerce2024Entities carrito = new ecommerce2024Entities())
                {
                    USUARIO Usuario = carrito.USUARIO.FirstOrDefault(u => u.IdUsuario == IdUsuario);
                    if (Usuario == null)
                    {
                        throw new ArgumentException($"No se encontró ningún usuario con el Id {IdUsuario}");
                    }

                    Usuario.Clave = clave;
                    Usuario.Reestablecer = true;

                    if (carrito.SaveChanges() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo actualizar la clave del usuario";
                    }
                }
            }
            catch (Exception x)
            {
                resultado = false;
                Mensaje = x.Message;
            }
            return resultado;
        }

    }
}
