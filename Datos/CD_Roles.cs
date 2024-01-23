
using Entidad_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CD_Roles
    {

        public List<Entidad_.Rol> ListarRol()
        {
            try
            {
                List<Entidad_.Rol> Lista = new List<Entidad_.Rol>();
                ecommerce2024Entities Tadi = new ecommerce2024Entities();
                var query = Tadi.Rol.OrderBy(id => id.IdRol);
                foreach (var miObejto in query)
                {
                    Entidad_.Rol rol = new Entidad_.Rol();
                    rol.id = miObejto.IdRol;
                    rol.descripcion = miObejto.Descripcion;
                    Lista.Add(rol);
                }
                return Lista;
            }
            catch
            {
                List<Entidad_.Rol> error = new List<Entidad_.Rol>();
                return error;
            }
        }

    }
}
