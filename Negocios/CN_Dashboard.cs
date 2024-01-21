using Datos;
using Entidad_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class CN_Dashboard
    {

        private CD_Dashboard objCapaDato = new CD_Dashboard();

        public Dashboard Listar()
        {
            return objCapaDato.Listar();
        }

        public List<Reporte> ListarReporte(string fechainicio, string fechafin, string idTransaccion)
        {
            return objCapaDato.ListarReporte(fechainicio, fechafin, idTransaccion);
        }

    }
}
