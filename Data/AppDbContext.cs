
using Microsoft.EntityFrameworkCore;
using ConsoleERP.Clases;
using DotNetEnv;

namespace ConsoleERP.Data
{
    public class AppDbContext : DbContext
    {
        protected string dbHost;
        protected string dbName;
        protected string dbUser;
        protected string dbPass;
        public AppDbContext()
        {
            DotNetEnv.Env.TraversePath().Load();
            dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            dbName = Environment.GetEnvironmentVariable("DB_Name");
            dbUser = Environment.GetEnvironmentVariable("DB_USER");
            dbPass = Environment.GetEnvironmentVariable("DB_PASS");

            // Depuración de variables de entorno
            Console.WriteLine($"DB_HOST: {dbHost}");
            Console.WriteLine($"DB_Name: {dbName}");
            Console.WriteLine($"DB_USER: {dbUser}");
            Console.WriteLine($"DB_PASS: {dbPass}");

            // Verificar si las variables de entorno están configuradas
            if (string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbName) ||
                string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPass))
            {
                throw new Exception("No se pueden obtener las credenciales de la base de datos.");
            }
        }
        public DbSet<Empleado> Empleados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbName) ||
                string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPass))
            {
                throw new Exception("No se pueden obtener las credenciales de la base de datos.");
            }

            optionsBuilder.UseNpgsql($"Host={dbHost};Database={dbName};Username={dbUser};Password={dbPass}");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>()
                .HasDiscriminator<TipoEmpleado>("TipoEmpleado")
                .HasValue<TiempoParcial>(TipoEmpleado.TiempoParcial)
                .HasValue<TiempoCompleto>(TipoEmpleado.TiempoCompleto)
                .HasValue<Contratista>(TipoEmpleado.Contratista)
                .HasValue<Destajo>(TipoEmpleado.Destajo);

            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.DPI)
                .IsUnique();
        }
    }
}
