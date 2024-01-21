using Entidad_;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CD_Marca
    {

        public List<Marcas> Listar()
        {
            try
            {
                List<Marcas> Lista = new List<Marcas>();
                TADIAdminEntities tadi = new TADIAdminEntities();
                var query = tadi.Marca.OrderBy(micarrito => micarrito.id);
                foreach (var miObejto in query)
                {
                    Marcas objMarca = new Marcas();
                    objMarca.id = miObejto.id;
                    objMarca.dexcripcion = miObejto.Descripcion;
                    objMarca.Activo = Convert.ToBoolean(miObejto.Activo);

                    Lista.Add(objMarca); // Agregar objeto Usuario a la lista
                }
                return Lista;
            }
            catch
            {
                List<Marcas> error = new List<Marcas>();
                return error;
            }
        }


        public int Registrar(Marcas obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (var db = new TADIAdminEntities())
                {

                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(bool));

                    db.sp_RegistrarMarca(obj.dexcripcion, obj.Activo, mensajeParam, resultadoParam);

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




        public bool Editar(Marcas obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (var db = new TADIAdminEntities())
                {

                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(bool));

                    db.sp_EditarMarca(obj.id, obj.dexcripcion, obj.Activo, mensajeParam, resultadoParam);

                    Mensaje = mensajeParam.Value.ToString();
                    resultado = (bool)resultadoParam.Value;

                    //... código para utilizar los valores de mensaje y resultado aquí ...
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }



        public bool Eliminar(int idMarca, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (var carrito = new TADIAdminEntities())
                {

                    ObjectParameter mensajeParam = new ObjectParameter("Mensaje", typeof(string));
                    ObjectParameter resultadoParam = new ObjectParameter("Resultado", typeof(bool));

                    carrito.sp_EliminarMarca(idMarca, mensajeParam, resultadoParam);
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
