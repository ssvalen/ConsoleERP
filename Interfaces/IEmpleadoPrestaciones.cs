using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleERP.Interfaces
{
    public interface IEmpleadoPrestaciones
    {
        decimal CalcularBono14();
        decimal CalcularAguinaldo();
        decimal CalcularVacaciones();
    }
}
