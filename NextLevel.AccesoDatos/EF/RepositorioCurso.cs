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
                             .Include(c => c.Semanas)
                             .Include(c => c.Temarios);
                return cursos;
            }
            catch (Exception ex)
            {
                throw new CursoException("Error al obtener los cursos", ex);
            }
        }

        public IEnumerable<Curso> FindWithFilter(string filtro)
        {
            try
            {
                filtro = filtro.ToLower();

                var query = _db.Cursos
                    .Include(c => c.Docente)
                    .Include(c => c.Estudiantes)
                    .Include(c => c.Semanas)
                    .Include(c => c.Temarios)
                    .Where(c =>
                        (c.Nombre != null && c.Nombre.ToLower().Contains(filtro))
                        || (c.Descripcion != null && c.Descripcion.ToLower().Contains(filtro))
                        || (c.Temarios != null && c.Temarios.Any(t =>
                                t.Tema != null && t.Tema.ToLower().Contains(filtro)
                           ))
                    );

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new CursoException("Error al obtener los cursos filtrados", ex);
            }
        }

        public IEnumerable<Curso> FindWithFilterAndCategory(string filtro, string categoria)
        {
            try
            {
                filtro = filtro.ToLower();

                var query = _db.Cursos
                    .Include(c => c.Docente)
                    .Include(c => c.Estudiantes)
                    .Include(c => c.Semanas)
                    .Include(c => c.Temarios)
                    .Where(c =>
                        (c.Nombre != null && c.Nombre.ToLower().Contains(filtro))
                        || (c.Descripcion != null && c.Descripcion.ToLower().Contains(filtro))
                        || (c.Temarios != null && c.Temarios.Any(t =>
                                t.Tema != null && t.Tema.ToLower().Contains(filtro)
                           ))
                    );

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new CursoException("Error al obtener los cursos filtrados", ex);
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
    }
}
