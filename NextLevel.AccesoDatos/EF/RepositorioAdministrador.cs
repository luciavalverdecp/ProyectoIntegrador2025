using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioAdministrador : IRepositorioAdministrador
    {
        private NextLevelContext _db;
        public RepositorioAdministrador(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Administrador obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Administrador> FindAll()
        {
            throw new NotImplementedException();
        }

        public Administrador FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Administrador FindByEmail(string email)
        {
            return _db.Administradores.Where(a => a.Email == email)
                 .Include(a => a.Postulaciones)
                     .ThenInclude(p => p.CambioRol)
                 .Include(a => a.Postulaciones)
                     .ThenInclude(p => p.CambioRol)
                        .ThenInclude(cr => cr.Estudiante)
                 .Include(a => a.Postulaciones)
                     .ThenInclude(p => p.CambioRol)
                        .ThenInclude(cr => cr.Archivos)
                 .Include(a => a.Postulaciones)
                     .ThenInclude(p => p.AltaCurso)
                        .ThenInclude(a => a.Curso)
                 .Include(a => a.Postulaciones)
                     .ThenInclude(p => p.AltaCurso)
                        .ThenInclude(a => a.Curso)
                            .ThenInclude(c => c.Docente)
                 .Include(a => a.Postulaciones)
                     .ThenInclude(p => p.AltaCurso)
                        .ThenInclude(a => a.Archivos)
                 .FirstOrDefault();
        }

        public Administrador ObtenerAdminMenosPostu()
        {
            return _db.Administradores
                    .Include(a => a.Postulaciones)
                    .OrderBy(a => a.Postulaciones.Count)
                    .FirstOrDefault();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Administrador obj)
        {
            throw new NotImplementedException();
        }
    }
}
