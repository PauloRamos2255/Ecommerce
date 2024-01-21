using Entidad_;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CD_Producto
    {

        public List<Producto> Listar()
        {
            try
            {
                List<Producto> objLista = new List<Producto>();
                TADIAdminEntities tadi = new TADIAdminEntities();

                var query = tadi.Productos.OrderBy(micarrito => micarrito.id);

                foreach (var miObjeto in query)
                {
                    Producto objProducto = new Producto();
                    objProducto.IdProducto = miObjeto.id;
                    objProducto.Nombre = miObjeto.Nombre;
                    objProducto.Descripcion = miObjeto.Descripcion;
                    objProducto.IdMarca =Convert.ToInt16(miObjeto.id_Mar);
                    objProducto.IdCategoria = Convert.ToInt16(miObjeto.id_Categoria);
                    objProducto.Precio = Convert.ToDecimal(miObjeto.Precio);
                    objProducto.Stock = Convert.ToInt16(miObjeto.Stock);
                    objProducto.RutaImagen =miObjeto.RutaImagen;
                    objProducto.NombreImagen = miObjeto.NombreImagen;
                    objProducto.Activo = Convert.ToBoolean(miObjeto.Activo);

                    objLista.Add(objProducto);
                }

                return objLista;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public int Registrar(Producto obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (var db = new TADIAdminEntities())
                {


                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(bool));

                    db.sp_RegistrarProducto(obj.Nombre, obj.Descripcion,obj.IdMarca,obj.IdCategoria, obj.Precio, obj.Stock, obj.Activo, mensajeParam, resultadoParam);

                    idautogenerado = (int)resultadoParam.Value;
                    Mensaje = mensajeParam.Value.ToString();


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



        TADIAdminEntities tadi = new TADIAdminEntities();

        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                // Buscar el producto en la base de datos por su IdProducto
                Productos objProductos = tadi.Productos.FirstOrDefault(miProductos => miProductos.id == obj.IdProducto);

                if (objProductos != null)
                {
                    // Actualizar los datos del producto con los valores proporcionados
                    objProductos.NombreImagen = obj.NombreImagen;
                    objProductos.RutaImagen = obj.RutaImagen;

                    // Guardar los cambios en la base de datos
                    if (tadi.SaveChanges() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo actualizar la imagen";
                    }
                }
                else
                {
                    Mensaje = "El producto con el Id especificado no se encontró en la base de datos";
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = "Error al guardar los datos de la imagen: " + ex.Message;
            }

            return resultado;

        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (var db = new TADIAdminEntities())
                {
                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(int));

                    db.usp_EditarProducto(obj.IdProducto, obj.Nombre, obj.Descripcion, obj.IdMarca, obj.IdCategoria, obj.Precio, obj.Stock, obj.Activo, mensajeParam, resultadoParam);

                    Mensaje = mensajeParam.Value.ToString();
                    resultado = (int)(resultadoParam.Value) == 1;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }





        public bool Eliminar(int idProductos, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (var carrito = new TADIAdminEntities())
                {

                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(bool));

                    carrito.sp_EliminarProducto(idProductos, mensajeParam, resultadoParam);

                    mensaje = mensajeParam.Value.ToString();
                    resultado = (bool)resultadoParam.Value;
                }

            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Ocurrió un error al eliminar el usuario: " + ex.Message;
            }

            return resultado;
        }
    }

}

