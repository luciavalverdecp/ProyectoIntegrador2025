using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioEstudiante : IRepositorioEstudiante
    {
        private NextLevelContext _db;
        public RepositorioEstudiante(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Estudiante obj)
        {
            try
            {
                _db.Estudiantes.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new UsuarioException("No se pudo registrar el usuario.");
            }
        }

        public IEnumerable<Estudiante> FindAll()
        {
            throw new NotImplementedException();
        }

        public Estudiante FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            var estudiante = _db.Estudiantes.Find(id);
            try
            {
                _db.Estudiantes.Remove(estudiante);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update(Estudiante obj)
        {
            throw new NotImplementedException();
        }

        public Estudiante FindByEmail(string email)
        {
            return _db.Estudiantes.Where(e => e.Email == email).Include(e => e.Cursos).ThenInclude(c => c.Docente).FirstOrDefault();
        }
    }
}
