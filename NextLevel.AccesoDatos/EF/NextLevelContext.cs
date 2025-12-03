using System.Reflection.Emit;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.AccesoDatos.EF
{
    public class NextLevelContext : DbContext
    {
        public NextLevelContext(DbContextOptions<NextLevelContext> options)
            : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Foro> Foros { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<Mensajeria> Mensajerias { get; set; }
        public DbSet<Semana> Semanas { get; set; }
        public DbSet<AltaCurso> AltaCursos { get; set; }
        public DbSet<CambioRol> CambiosDeRol {  get; set; } 
        //public DbSet<Prueba> Pruebas { get; set; }
        //public DbSet<Calificacion> Calificaciones { get; set; }
        //public DbSet<CambioRol> CambiosRol { get; set; }
        //public DbSet<Temario> Temarios { get; set; } //TODO ???

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // 🔹 Clave primaria de CambioRol
            mb.Entity<CambioRol>()
                .HasKey(c => c.Id);

            // 🔹 Clave primaria de Foro
            mb.Entity<Foro>()
                .HasKey(f => f.Id);

            // ⚠️ Ignorar la lista de Mensajes dentro de Foro (no tiene FK)
            mb.Entity<Foro>()
                .Ignore(f => f.Mensajes);

            // 🔹 Conversión de NroDocente (Value Object)
            mb.Entity<Docente>()
                .Property(d => d.NroDocente)
                .HasConversion(
                    nro => nro.NroDeDocente,
                    nro => new LogicaNegocio.ValueObject.Docente.NroDocente(nro)
                )
                .HasColumnName("NroDocente_NroDeDocente");

            // 🔹 Relación Curso ↔ Docente
            mb.Entity<Curso>()
                .HasOne(c => c.Docente)
                .WithMany(d => d.Cursos)
                .HasForeignKey(c => c.DocenteId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Relación Curso ↔ Estudiante (many-to-many)
            mb.Entity<Curso>()
                .HasMany(c => c.Estudiantes)
                .WithMany(e => e.Cursos)
                .UsingEntity<Dictionary<string, object>>(
                    "CursoEstudiante",
                    j => j
                        .HasOne<Estudiante>()
                        .WithMany()
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Curso>()
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict)
                );

            // 🔹 Relación Estudiante ↔ CambioRol (uno a uno)
            mb.Entity<Estudiante>()
                .HasOne(e => e.CambioRol)
                .WithOne(c => c.Estudiante)
                .HasForeignKey<CambioRol>(c => c.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);


            // 🔹 Relación Mensajeria ↔ Emisor / Receptor
            mb.Entity<Mensajeria>()
                .HasOne(m => m.Emisor)
                .WithMany()
                .HasForeignKey(m => m.EmisorId)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Mensajeria>()
                .HasOne(m => m.Receptor)
                .WithMany()
                .HasForeignKey(m => m.ReceptorId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Relación Mensajeria ↔ Curso
            mb.Entity<Mensajeria>()
                .HasOne(m => m.Curso)
                .WithMany()
                .HasForeignKey(m => m.CursoId)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Temario>()
                .HasOne(t => t.Curso)
                .WithMany(c => c.Temarios)
                .HasForeignKey(t => t.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            // 👇 Siempre al final
            base.OnModelCreating(mb);
        }

    }
}