using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Clientes
    {

       public int id { get; set; }
       public string Nombre { get; set; }
       public string Apellido { get; set; }
       public int sexo { get; set; }
       public string Correo { get; set; }
       public string Contraseña { get; set; }
       public string ConfirmarClave { get; set; }
        public bool Restablecer { get; set; }

    }
}
