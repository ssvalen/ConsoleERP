namespace ConsoleERP.Clases
{
    public abstract class Empleado
    {
        // Propiedades públicas con getters y setters
        public string DPI { get; set; }
        public int EmpleadoId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public TipoEmpleado TipoEmpleado { get; set; }

        // Constructor sin parámetros requerido por EF Core
        protected Empleado() { }

        protected Empleado(int empleadoId, string dpi, string nombre, string apellido, TipoEmpleado tipoEmpleado)
        {
            EmpleadoId = empleadoId;
            DPI = dpi;
            Nombre = nombre;
            Apellido = apellido;
            TipoEmpleado = tipoEmpleado;
        }
        public decimal ObtenerSalario() => Salario();

        public abstract decimal Salario();

        public void MostrarDetalle()
        {
            string tipoEmpleadoStr = TipoEmpleado switch
            {
                TipoEmpleado.TiempoParcial => "Tiempo Parcial",
                TipoEmpleado.TiempoCompleto => "Tiempo Completo",
                TipoEmpleado.Contratista => "Contratista",
                TipoEmpleado.Destajo => "Destajo",
                _ => "Desconocido"
            };

            Console.WriteLine($"ID: {EmpleadoId}");
            Console.WriteLine($"DPI: {DPI}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Apellido: {Apellido}");
            Console.WriteLine($"Tipo de Empleado: {tipoEmpleadoStr}");
            Console.WriteLine($"Salario: {ObtenerSalario():C}");
        }
    }

    public enum TipoEmpleado
    {
        TiempoParcial,
        TiempoCompleto,
        Contratista,
        Destajo
    }
}
