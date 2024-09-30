using System;
using ConsoleERP.Interfaces;

namespace ConsoleERP.Clases
{
    public class TiempoParcial : Empleado, IEmpleadoPrestaciones
    {
        public decimal TarifaPorHora { get; set; }
        public int HorasTrabajadas { get; set; }

        public TiempoParcial() { }


        public TiempoParcial(int empleadoId, string dpi, string nombre, string apellido, decimal tarifaPorHora, int horasTrabajadas)
            : base(empleadoId, dpi, nombre, apellido, TipoEmpleado.TiempoParcial)
        {
            TarifaPorHora = tarifaPorHora;
            HorasTrabajadas = horasTrabajadas;
        }

        public override decimal Salario() //Polimorfismo
        {
            return TarifaPorHora * HorasTrabajadas;
        }

        public decimal CalcularBono14()
        {
            return Salario() * (HorasTrabajadas / (decimal)(8 * 30)); // Considerando una jornada de 8 horas diarias y 30 días al mes.
        }

        public decimal CalcularAguinaldo()
        {
            return Salario() * (HorasTrabajadas / (decimal)(8 * 30));
        }

        public decimal CalcularVacaciones()
        {
            return (Salario() / 30) * 15 * (HorasTrabajadas / (decimal)(8 * 30)); // Proporción de los 15 días de vacaciones.
        }
    }
}
