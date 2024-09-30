using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleERP.Clases
{
    public class Contratista : Empleado
    {
        public decimal TarifaPorProyecto { get;  set; }
        public int ProyectosCompletados { get;  set; }
        // Constructor sin parámetros requerido por EF Core
        public Contratista() { }
        public Contratista(int empleadoId, string dpi, string nombre, string apellido, decimal tarifaPorProyecto, int proyectosCompletados)
            : base(empleadoId, dpi, nombre, apellido, TipoEmpleado.Contratista)
        {
            TarifaPorProyecto = tarifaPorProyecto;
            ProyectosCompletados = proyectosCompletados;
        }

        public override decimal Salario()
        {
            return TarifaPorProyecto * ProyectosCompletados;
        }
    }
}
