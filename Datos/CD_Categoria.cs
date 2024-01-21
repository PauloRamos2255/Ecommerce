using Entidad;
using Entidad_;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CD_Categoria
    {

        public List<Categorias> ListarCategoris()
        {
            try
            {
                List<Categorias> Lista = new List<Categorias>();
                TADIAdminEntities Tadi = new TADIAdminEntities();
                var query = Tadi.Categoria.OrderBy(micarrito => micarrito.id);
                foreach (var miObejto in query)
                {
                    Categorias obj = new Categorias();
                    obj.id= miObejto.id;
                    obj.descripcion = miObejto.Descripcion;
                    obj.Activo = Convert.ToBoolean(miObejto.Activo);
                    Lista.Add(obj); // Agregar objeto Cliente a la lista
                }
                return Lista;
            }
            catch
            {
                List<Categorias> error = new List<Categorias>();
                return error;
            }
        }


        public int Registrar(Categorias obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (var db = new TADIAdminEntities())
                {
                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(int));

                    db.sp_RegistrarCategoria(obj.descripcion, obj.Activo, mensajeParam, resultadoParam);

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


        public bool Editar(Categorias obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (var db = new TADIAdminEntities())
                {
                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(int));

                    db.sp_EditarCategoria(obj.id,obj.descripcion, obj.Activo, mensajeParam, resultadoParam);

                    Mensaje = mensajeParam.Value.ToString();
                    resultado = (bool)resultadoParam.Value;

                    //... código para utilizar los valores de mensaje y resultado aquí ...
                }
            }
            catch (Exception x)
            {
                resultado = false;
                Mensaje = x.Message;
            }

            return resultado;
        }


        public bool Eliminar(int idCategoria, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (var tadi = new TADIAdminEntities())
                {

                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(bool));

                    tadi.sp_EliminarCategoria(idCategoria, mensajeParam, resultadoParam);

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
