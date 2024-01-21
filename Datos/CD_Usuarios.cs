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

        public List<Usuario> ListarUsuarios()
        {
            try
            {
                List<Usuario> Lista = new List<Usuario>();
                TADIAdminEntities Tadi = new TADIAdminEntities();
                var query = Tadi.Usuarios.OrderBy(micarrito => micarrito.id);
                foreach (var miObejto in query)
                {
                    Usuario obj = new Usuario();
                    obj.id = miObejto.id;
                    obj.nombre = miObejto.Nombres;
                    obj.apellido = miObejto.Apellidos;
                    obj.id_roles = Convert.ToInt32(miObejto.Id_Rol);
                    obj.correo = miObejto.Correo;
                    obj.contraseña = miObejto.Contraseña;
                    obj.numero = miObejto.Numero;
                    obj.restablecer = Convert.ToBoolean(miObejto.Reestablecer);
                    obj.activo= Convert.ToBoolean(miObejto.Activo);
       
                    Lista.Add(obj);
                }
                return Lista;
            }
            catch
            {
                List<Usuario> error = new List<Usuario>();
                return error;
            }
        }


        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (var db = new TADIAdminEntities())
                {
                    var resultado = new ObjectParameter("Resultado", typeof(int));
                    var mensaje = new ObjectParameter("Mensaje", typeof(string));

                    db.sp_Registrarusuario(
                        obj.nombre,
                        obj.apellido,
                        obj.correo,
                        obj.id_roles,
                        obj.contraseña,
                        obj.numero,
                        obj.activo,
                        mensaje,
                        resultado
                    );
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

        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (var db = new TADIAdminEntities())
                {
                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(bool));

                    db.sp_EditarUsuario(obj.id, obj.nombre, obj.apellido, obj.id_roles, obj.correo, obj.numero , obj.activo, mensajeParam, resultadoParam);

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
                using (var db = new TADIAdminEntities())
                {
                    var usuarioAEliminar = db.Usuarios.FirstOrDefault(u => u.id == idUsuario);
                    if (usuarioAEliminar != null)
                    {
                        db.Usuarios.Remove(usuarioAEliminar);
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

                using (TADIAdminEntities carrito = new TADIAdminEntities())
                {
                    Usuarios Usuario = carrito.Usuarios.FirstOrDefault(u => u.id == IdUsuario);
                    if (Usuario == null)
                    {
                        throw new ArgumentException($"No se encontró ningún usuario con el Id {IdUsuario}");
                    }

                    Usuario.Contraseña = Nuevaclave;
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

                using (TADIAdminEntities carrito = new TADIAdminEntities())
                {
                    Usuarios Usuario = carrito.Usuarios.FirstOrDefault(u => u.id == IdUsuario);
                    if (Usuario == null)
                    {
                        throw new ArgumentException($"No se encontró ningún usuario con el Id {IdUsuario}");
                    }

                    Usuario.Contraseña = clave;
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
