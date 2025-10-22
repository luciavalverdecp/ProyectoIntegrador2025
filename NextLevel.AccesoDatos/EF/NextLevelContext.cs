using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.AccesoDatos.EF
{
    public class NextLevelContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set;}
        public DbSet<Foro> Foros { get; set; }
        public DbSet<Material> Materiales { get; set; }  
        public DbSet<Mensaje> Mensajes { get; set;}
        public DbSet<Mensajeria> Mensajerias { get; set; }
        public DbSet<Semana> Semanas { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Docente>().Property(d => d.NroDocente).HasConversion(nro => nro.NroDeDocente, nro => new LogicaNegocio.ValueObject.Docente.NroDocente(nro)).HasColumnName("NroDocente_NroDeDocente");
            mb.Entity<Docente>().HasMany(d => d.Cursos).WithOne();
            mb.Entity<Estudiante>().HasMany(e => e.Cursos).WithMany();
            base.OnModelCreating(mb);
        }
    }
}
