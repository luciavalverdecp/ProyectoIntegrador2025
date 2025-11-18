using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioCurso : IRepositorioCurso
    {
        private NextLevelContext _db;
        public RepositorioCurso(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Curso obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Curso> FindAll()
        {
            try
            {
                var cursos = _db.Cursos
                             .Include(c => c.Docente)
                             .Include(c => c.Estudiantes)
                             .Include(c => c.Semanas);
                return cursos;
            }
            catch (Exception ex)
            {
                throw new CursoException("Error al obtener los cursos", ex);
            }
        }

        public Curso FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Curso obj)
        {
            throw new NotImplementedException();
        }

        public Curso FindByNombre(string nombre)
        {
            return _db.Cursos.Where(c => c.Nombre == nombre).FirstOrDefault();
        }
    }
}
