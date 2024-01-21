using Entidad;
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

        public List<Rol> ListarRol()
        {
            try
            {
                List<Rol> Lista = new List<Rol>();
                TADIAdminEntities Tadi = new TADIAdminEntities();
                var query = Tadi.Roles.OrderBy(id => id.id);
                foreach (var miObejto in query)
                {
                   Rol rol = new Rol();
                    rol.id = miObejto.id;
                    rol.descripcion = miObejto.descripcion;
                    Lista.Add(rol);
                }
                return Lista;
            }
            catch
            {
                List<Rol> error = new List<Rol>();
                return error;
            }
        }

    }
}
