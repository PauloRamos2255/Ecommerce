using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad_
{
    public class Usuarios
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int id_roles { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public bool restablecer { get; set; }
        public bool activo { get; set; }
        public string numero { get; set; }

    }
}
