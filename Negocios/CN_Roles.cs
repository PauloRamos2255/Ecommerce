using Datos;
using Entidad;
using Entidad_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class CN_Roles
    {

        CD_Roles objRoles = new CD_Roles();

        public List<Rol> ListarRol()
        {
            return objRoles.ListarRol();
        }


    }
}
