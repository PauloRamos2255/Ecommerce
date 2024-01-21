using Datos;
using Entidad_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class CN_Marca
    {

        private CD_Marca OBJCapaDato = new CD_Marca();

        public List<Marcas> Listar()
        {
            return OBJCapaDato.Listar();
        }

        public int Registrar(Marcas obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.dexcripcion) || string.IsNullOrWhiteSpace(obj.dexcripcion))
            {
                Mensaje = "La Descripcion de la Categoria no puede estar vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return OBJCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }

        }


        public bool Editar(Marcas obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.dexcripcion) || string.IsNullOrWhiteSpace(obj.dexcripcion))
            {
                Mensaje = "La Descripcion de la Categoria no puede estar vacio";
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

        //public List<Marca> ListarMarcaProducto(int IdCategoria)
        //{
        //    return OBJCapaDato.ListarMarcaProducto(IdCategoria);
        //}

    }
}
