using Microsoft.EntityFrameworkCore;
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
        public DbSet<Semana> Semanas { get; set; }
        public DbSet<AltaCurso> AltaCursos { get; set; }
        public DbSet<CambioRol> CambiosDeRol { get; set; }
        public DbSet<Postulacion> Postulaciones { get; set; }
        public DbSet<Conversacion> Conversaciones { get; set; }
        public DbSet<ParticipanteConversacion> ParticipanteConversaciones { get; set; }

        //public DbSet<Prueba> Pruebas { get; set; }
        //public DbSet<Calificacion> Calificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // 🔹 Clave primaria de CambioRol
            mb.Entity<CambioRol>()
                .HasKey(c => c.Id);

            // 🔹 Clave primaria de Foro
            mb.Entity<Foro>()
                .HasKey(f => f.Id);

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


            mb.Entity<Temario>()
                .HasOne(t => t.Curso)
                .WithMany(c => c.Temarios)
                .HasForeignKey(t => t.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Administrador>(entity =>
            {
                entity.HasMany(a => a.Postulaciones)
                      .WithOne(p => p.Administrador)
                      .HasForeignKey(p => p.AdministradorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            mb.Entity<Conversacion>()
            .HasKey(c => c.Id);

            mb.Entity<Conversacion>()
                .Property(c => c.FechaCreacion)
                .IsRequired();

            mb.Entity<ParticipanteConversacion>()
            .HasKey(pc => new { pc.ConversacionId, pc.UsuarioId });

            mb.Entity<ParticipanteConversacion>()
                .HasOne(pc => pc.Conversacion)
                .WithMany()
                .HasForeignKey(pc => pc.ConversacionId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<ParticipanteConversacion>()
                .HasOne(pc => pc.Usuario)
                .WithMany()
                .HasForeignKey(pc => pc.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Mensaje>()
            .HasKey(m => m.Id);

            mb.Entity<Mensaje>()
                .Property(m => m.Contenido)
                .IsRequired();

            mb.Entity<Mensaje>()
                .Property(m => m.FechaEnvio)
                .IsRequired();

            mb.Entity<Mensaje>()
                .HasOne(m => m.Conversacion)
                .WithMany()
                .HasForeignKey(m => m.ConversacionId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Mensaje>()
                .HasOne(m => m.Usuario)
                .WithMany()
                .HasForeignKey(m => m.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Foro>()
            .HasKey(f => f.Id);

            mb.Entity<Foro>()
                .HasOne(f => f.Conversacion)
                .WithMany()
                .HasForeignKey(f => f.ConversacionId)
                .OnDelete(DeleteBehavior.Cascade);


            // 👇 Siempre al final
            base.OnModelCreating(mb);
        }

    }
}