using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleERP.Clases
{
    public class Destajo : Empleado
    {
        public decimal TarifaPorUnidad { get;  set; }
        public int UnidadesProducidas { get;  set; }
        public Destajo() {}
        public Destajo(int empleadoId, string dpi, string nombre, string apellido, decimal tarifaPorUnidad, int unidadesProducidas)
            : base(empleadoId, dpi, nombre, apellido, TipoEmpleado.Destajo)
        {
            TarifaPorUnidad = tarifaPorUnidad;
            UnidadesProducidas = unidadesProducidas;
        }

        public override decimal Salario()
        {
            return TarifaPorUnidad * UnidadesProducidas;
        }
    }
}
