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
                ecommerce2024Entities Tadi = new ecommerce2024Entities();
                var query = Tadi.CLIENTE.OrderBy(micarrito => micarrito.IdCliente);
                foreach (var miObejto in query)
                {
                    Clientes objCliente = new Clientes();
                    objCliente.id = Convert.ToInt16(miObejto.IdCliente);
                    objCliente.Nombre = miObejto.Nombres;
                    objCliente.Apellido = miObejto.Apellidos;
                    objCliente.Correo = miObejto.Correo;
                    objCliente.Contraseña = miObejto.Clave;
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
                using (var db = new ecommerce2024Entities())
                {
                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(int));

                    db.sp_RegistarCliente(obj.Nombre, obj.Apellido, obj.Correo, obj.Contraseña, mensajeParam, resultadoParam);

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

                using (ecommerce2024Entities carrito = new ecommerce2024Entities())
                {
                    CLIENTE Cliente = carrito.CLIENTE.FirstOrDefault(u => u.IdCliente == IdCliente);
                    if (Cliente == null)
                    {
                        throw new ArgumentException($"No se encontró ningún Cliente con el Id {IdCliente}");
                    }

                    Cliente.Clave = clave;
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

                using (ecommerce2024Entities carrito = new ecommerce2024Entities())
                {
                    CLIENTE Cliente = carrito.CLIENTE.FirstOrDefault(u => u.IdCliente == IdCliente);
                    if (Cliente == null)
                    {
                        throw new ArgumentException($"No se encontró ningún Cliente con el Id {IdCliente}");
                    }

                    Cliente.Clave = Nuevaclave;
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
