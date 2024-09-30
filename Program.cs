using System;
using System.Linq;
using ConsoleERP.Data;
using ConsoleERP.Clases;
using ConsoleERP.Interfaces;

namespace ConsoleERP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Gestión de Empleados");
                    Console.WriteLine("1. Crear Empleado");
                    Console.WriteLine("2. Leer Empleados");
                    Console.WriteLine("3. Actualizar Empleado");
                    Console.WriteLine("4. Eliminar Empleado");
                    Console.WriteLine("5. Calcular Prestaciones");
                    Console.WriteLine("6. Salir");

                    var opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            CrearEmpleado(db);
                            break;
                        case "2":
                            LeerEmpleados(db);
                            break;
                        case "3":
                            ActualizarEmpleado(db);
                            break;
                        case "4":
                            EliminarEmpleado(db);
                            break;
                        case "5":
                            CalcularPrestaciones(db);
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
            }
        }

        private static void CrearEmpleado(AppDbContext context)
{
    Console.WriteLine("Ingrese el DPI:");
    var dpi = Console.ReadLine();

    if (string.IsNullOrEmpty(dpi))
    {
        Console.WriteLine("El DPI no puede estar vacío.");
        return;
    }

    // Verificar si el DPI ya existe en la base de datos
    if (context.Empleados.Any(e => e.DPI == dpi))
    {
        Console.WriteLine("El DPI ya está registrado.");
        return;
    }

    Console.WriteLine("Ingrese el Nombre:");
    var nombre = Console.ReadLine();
    Console.WriteLine("Ingrese el Apellido:");
    var apellido = Console.ReadLine();

    Console.WriteLine("Seleccione el Tipo de Empleado:");
    Console.WriteLine("1. TiempoParcial");
    Console.WriteLine("2. TiempoCompleto");
    Console.WriteLine("3. Contratista");
    Console.WriteLine("4. Destajo");
    var tipoEmpleadoStr = Console.ReadLine();

    if (!int.TryParse(tipoEmpleadoStr, out int tipoEmpleadoNumero) || tipoEmpleadoNumero < 1 || tipoEmpleadoNumero > 4)
    {
        Console.WriteLine("Tipo de empleado no válido.");

        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();

        return;
    }

    // Convertir el número del tipo de empleado a la enumeración correspondiente
    TipoEmpleado tipoEmpleado = (TipoEmpleado)(tipoEmpleadoNumero - 1);

    // Crear el empleado basado en el tipo seleccionado
    Empleado empleado = tipoEmpleado switch
    {
        TipoEmpleado.TiempoParcial => CrearTiempoParcial(dpi, nombre, apellido),
        TipoEmpleado.TiempoCompleto => CrearTiempoCompleto(dpi, nombre, apellido),
        TipoEmpleado.Contratista => CrearContratista(dpi, nombre, apellido),
        TipoEmpleado.Destajo => CrearDestajo(dpi, nombre, apellido),
        _ => throw new ArgumentOutOfRangeException(nameof(tipoEmpleado), "Tipo de empleado no manejado.")
    };

    context.Empleados.Add(empleado);
    context.SaveChanges();

    Console.WriteLine("Empleado creado con éxito.");
}

        private static TiempoParcial CrearTiempoParcial(string dpi, string nombre, string apellido)
        {
            Console.WriteLine("Ingrese la Tarifa por Hora:");
            var tarifaPorHora = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese las Horas Trabajadas:");
            var horasTrabajadas = int.Parse(Console.ReadLine());

            return new TiempoParcial
            {
                DPI = dpi,
                Nombre = nombre,
                Apellido = apellido,
                TarifaPorHora = tarifaPorHora,
                HorasTrabajadas = horasTrabajadas
            };
        }

        private static TiempoCompleto CrearTiempoCompleto(string dpi, string nombre, string apellido)
        {
            Console.WriteLine("Ingrese el Salario Mensual:");
            var salarioMensual = decimal.Parse(Console.ReadLine());

            return new TiempoCompleto
            {
                DPI = dpi,
                Nombre = nombre,
                Apellido = apellido,
                SalarioMensual = salarioMensual
            };
        }

        private static Contratista CrearContratista(string dpi, string nombre, string apellido)
        {
            Console.WriteLine("Ingrese la Tarifa por Proyecto:");
            var tarifaPorProyecto = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la Cantidad de Proyectos Completados:");
            var proyectosCompletados = int.Parse(Console.ReadLine());

            return new Contratista
            {
                DPI = dpi,
                Nombre = nombre,
                Apellido = apellido,
                TarifaPorProyecto = tarifaPorProyecto,
                ProyectosCompletados = proyectosCompletados
            };
        }

        private static Destajo CrearDestajo(string dpi, string nombre, string apellido)
        {
            Console.WriteLine("Ingrese la Tarifa por Unidad:");
            var tarifaPorUnidad = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la Cantidad de Unidades Producidas:");
            var unidadesProducidas = int.Parse(Console.ReadLine());

            return new Destajo
            {
                DPI = dpi,
                Nombre = nombre,
                Apellido = apellido,
                TarifaPorUnidad = tarifaPorUnidad,
                UnidadesProducidas = unidadesProducidas
            };
        }

        private static void LeerEmpleados(AppDbContext context)
        {
            var empleados = context.Empleados.ToList();
            if (empleados.Count == 0)
            {
                Console.WriteLine("No hay empleados registrados.");
            }
            else
            {
                foreach (var empleado in empleados)
                {
                    Console.WriteLine($"ID: {empleado.EmpleadoId}");
                    Console.WriteLine($"DPI: {empleado.DPI}");
                    Console.WriteLine($"Nombre: {empleado.Nombre}");
                    Console.WriteLine($"Apellido: {empleado.Apellido}");

                    // Mostrar el tipo de empleado y el salario
                    switch (empleado)
                    {
                        case TiempoParcial tiempoParcial:
                            Console.WriteLine("Tipo de Empleado: Tiempo Parcial");
                            Console.WriteLine($"Salario: {tiempoParcial.Salario():C}");
                            break;

                        case TiempoCompleto tiempoCompleto:
                            Console.WriteLine("Tipo de Empleado: Tiempo Completo");
                            Console.WriteLine($"Salario: {tiempoCompleto.Salario():C}");
                            break;

                        case Contratista contratista:
                            Console.WriteLine("Tipo de Empleado: Contratista");
                            Console.WriteLine($"Salario: {contratista.Salario():C}");
                            break;

                        case Destajo destajo:
                            Console.WriteLine("Tipo de Empleado: Destajo");
                            Console.WriteLine($"Salario: {destajo.Salario():C}");
                            break;

                        default:
                            Console.WriteLine("Tipo de Empleado: Desconocido");
                            break;
                    }

                    Console.WriteLine(); 
                }
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void ActualizarEmpleado(AppDbContext context)
        {
            Console.WriteLine("Ingrese el ID del empleado a actualizar:");
            var id = int.Parse(Console.ReadLine());

            var empleado = context.Empleados.Find(id);
            if (empleado == null)
            {
                Console.WriteLine("Empleado no encontrado.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Datos actuales del empleado:");
            empleado.MostrarDetalle();

            Console.WriteLine("Ingrese nuevo nombre (deje en blanco para no modificar):");
            var nuevoNombre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                empleado.Nombre = nuevoNombre;
            }

            Console.WriteLine("Ingrese nuevo apellido (deje en blanco para no modificar):");
            var nuevoApellido = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoApellido))
            {
                empleado.Apellido = nuevoApellido;
            }

            // Dependiendo del tipo de empleado, actualizamos campos adicionales
            switch (empleado)
            {
                case TiempoParcial tiempoParcial:
                    Console.WriteLine("Ingrese nueva Tarifa por Hora (deje en blanco para no modificar):");
                    var nuevaTarifaPorHoraStr = Console.ReadLine();
                    if (decimal.TryParse(nuevaTarifaPorHoraStr, out var nuevaTarifaPorHora))
                    {
                        tiempoParcial.TarifaPorHora = nuevaTarifaPorHora;
                    }

                    Console.WriteLine("Ingrese nuevas Horas Trabajadas (deje en blanco para no modificar):");
                    var nuevasHorasTrabajadasStr = Console.ReadLine();
                    if (int.TryParse(nuevasHorasTrabajadasStr, out var nuevasHorasTrabajadas))
                    {
                        tiempoParcial.HorasTrabajadas = nuevasHorasTrabajadas;
                    }
                    break;

                case TiempoCompleto tiempoCompleto:
                    Console.WriteLine("Ingrese nuevo Salario Mensual (deje en blanco para no modificar):");
                    var nuevoSalarioMensualStr = Console.ReadLine();
                    if (decimal.TryParse(nuevoSalarioMensualStr, out var nuevoSalarioMensual))
                    {
                        tiempoCompleto.SalarioMensual = nuevoSalarioMensual;
                    }
                    break;

                case Contratista contratista:
                    Console.WriteLine("Ingrese nueva Tarifa por Proyecto (deje en blanco para no modificar):");
                    var nuevaTarifaPorProyectoStr = Console.ReadLine();
                    if (decimal.TryParse(nuevaTarifaPorProyectoStr, out var nuevaTarifaPorProyecto))
                    {
                        contratista.TarifaPorProyecto = nuevaTarifaPorProyecto;
                    }

                    Console.WriteLine("Ingrese nueva Cantidad de Proyectos Completados (deje en blanco para no modificar):");
                    var nuevaCantidadProyectosCompletadosStr = Console.ReadLine();
                    if (int.TryParse(nuevaCantidadProyectosCompletadosStr, out var nuevaCantidadProyectosCompletados))
                    {
                        contratista.ProyectosCompletados = nuevaCantidadProyectosCompletados;
                    }
                    break;

                case Destajo destajo:
                    Console.WriteLine("Ingrese nueva Tarifa por Unidad (deje en blanco para no modificar):");
                    var nuevaTarifaPorUnidadStr = Console.ReadLine();
                    if (decimal.TryParse(nuevaTarifaPorUnidadStr, out var nuevaTarifaPorUnidad))
                    {
                        destajo.TarifaPorUnidad = nuevaTarifaPorUnidad;
                    }

                    Console.WriteLine("Ingrese nueva Cantidad de Unidades Producidas (deje en blanco para no modificar):");
                    var nuevaCantidadUnidadesProducidasStr = Console.ReadLine();
                    if (int.TryParse(nuevaCantidadUnidadesProducidasStr, out var nuevaCantidadUnidadesProducidas))
                    {
                        destajo.UnidadesProducidas = nuevaCantidadUnidadesProducidas;
                    }
                    break;

                default:
                    Console.WriteLine("Tipo de empleado desconocido.");
                    break;
            }

            context.SaveChanges();
            Console.WriteLine("Empleado actualizado con éxito.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void EliminarEmpleado(AppDbContext context)
        {
            Console.WriteLine("Ingrese el ID del empleado a eliminar:");
            var id = int.Parse(Console.ReadLine());

            var empleado = context.Empleados.Find(id);
            if (empleado == null)
            {
                Console.WriteLine("Empleado no encontrado.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            context.Empleados.Remove(empleado);
            context.SaveChanges();
            Console.WriteLine("Empleado eliminado con éxito.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void CalcularPrestaciones(AppDbContext context)
        {
            Console.WriteLine("Ingrese el ID del empleado para calcular las prestaciones:");
            var id = int.Parse(Console.ReadLine());

            var empleado = context.Empleados.Find(id);
            if (empleado == null)
            {
                Console.WriteLine("Empleado no encontrado.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Datos del empleado:");
            empleado.MostrarDetalle();

            if (empleado is IEmpleadoPrestaciones empleadoPrestaciones)
            {
                Console.WriteLine($"Bono 14: {empleadoPrestaciones.CalcularBono14():C}");
                Console.WriteLine($"Aguinaldo: {empleadoPrestaciones.CalcularAguinaldo():C}");
                Console.WriteLine($"Vacaciones: {empleadoPrestaciones.CalcularVacaciones():C}");
            }
            else
            {
                Console.WriteLine("El empleado no tiene prestaciones calculables.");
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
