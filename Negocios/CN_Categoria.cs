using Datos;
using Entidad_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class CN_Categoria
    {

        private CD_Categoria OBJCapaDato = new CD_Categoria();

        public List<Categorias> Listar()
        {
            return OBJCapaDato.ListarCategoris();
        }

        public int Registrar(Categorias obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
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


        public bool Editar(Categorias obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
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

    }
}
