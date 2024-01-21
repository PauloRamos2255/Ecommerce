using Entidad;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CD_Cliente
    {

        public List<Clientes> ListarCliente()
        {
            try
            {
                List<Clientes> Lista = new List<Clientes>();
                TADIAdminEntities Tadi = new TADIAdminEntities();
                var query = Tadi.Cliente.OrderBy(micarrito => micarrito.id);
                foreach (var miObejto in query)
                {
                    Clientes objCliente = new Clientes();
                    objCliente.id = Convert.ToInt16(miObejto.id);
                    objCliente.Nombre = miObejto.Nombre;
                    objCliente.Apellido = miObejto.Apellido;
                    objCliente.Correo = miObejto.Correo;
                    objCliente.Contraseña = miObejto.Contraseña;
                    objCliente.Restablecer = Convert.ToBoolean(miObejto.Restablecer);
                    Lista.Add(objCliente); // Agregar objeto Cliente a la lista
                }
                return Lista;
            }
            catch
            {
                List<Clientes> error = new List<Clientes>();
                return error;
            }
        }


        public int Registrar(Clientes obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (var db = new TADIAdminEntities())
                {
                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(int));

                    db.sp_RegistarCliente(obj.Nombre, obj.Apellido, obj.Correo, obj.Contraseña,obj.sexo, mensajeParam, resultadoParam);

                    Mensaje = mensajeParam.Value.ToString();
                    idautogenerado = (int)resultadoParam.Value;

                    //... código para utilizar los valores de mensaje y resultado aquí ...
                }
            }
            catch (Exception x)
            {
                idautogenerado = 0;
                Mensaje = x.Message;
            }

            return idautogenerado;
        }


        public bool ReestablecerClave(int IdCliente, string clave, out string Mensaje)
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
                    Cliente Cliente = carrito.Cliente.FirstOrDefault(u => u.id == IdCliente);
                    if (Cliente == null)
                    {
                        throw new ArgumentException($"No se encontró ningún Cliente con el Id {IdCliente}");
                    }

                    Cliente.Contraseña = clave;
                    Cliente.Restablecer = true;

                    if (carrito.SaveChanges() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo actualizar la clave del Cliente";
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

        
        public bool CambiarClave(int IdCliente, string Nuevaclave, out string Mensaje)
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
                    Cliente Cliente = carrito.Cliente.FirstOrDefault(u => u.id == IdCliente);
                    if (Cliente == null)
                    {
                        throw new ArgumentException($"No se encontró ningún Cliente con el Id {IdCliente}");
                    }

                    Cliente.Contraseña = Nuevaclave;
                    Cliente.Restablecer = false;

                    if (carrito.SaveChanges() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo actualizar la clave del Cliente";
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
