using System;
using ConsoleERP.Interfaces;

namespace ConsoleERP.Clases
{
    public class TiempoCompleto : Empleado, IEmpleadoPrestaciones
    {
        public decimal SalarioMensual { get; set; }
        
        public TiempoCompleto() { }

        public TiempoCompleto(int empleadoId, string dpi, string nombre, string apellido, decimal salarioMensual)
            : base(empleadoId, dpi, nombre, apellido, TipoEmpleado.TiempoCompleto)
        {
            SalarioMensual = salarioMensual;
        }

        public override decimal Salario()
        {
            return SalarioMensual;
        }

        public decimal CalcularBono14()
        {
            // El bono 14 es el salario mensual completo para empleados de tiempo completo
            return SalarioMensual;
        }

        public decimal CalcularAguinaldo()
        {
            // El aguinaldo es el salario mensual completo para empleados de tiempo completo
            return SalarioMensual;
        }

        public decimal CalcularVacaciones()
        {
            // Las vacaciones son 15 días hábiles, calculado con base en el salario diario
            return (SalarioMensual / 30) * 15;
        }
    }
}
