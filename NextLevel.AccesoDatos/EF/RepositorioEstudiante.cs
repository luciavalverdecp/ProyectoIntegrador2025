using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
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
            var estudianteOriginal = _db.Estudiantes.Find(obj.Id);
            try
            {
                estudianteOriginal.NombreCompleto = obj.NombreCompleto;
                estudianteOriginal.Telefono = obj.Telefono;
                estudianteOriginal.Cursos = obj.Cursos;
                _db.Estudiantes.Update(estudianteOriginal);
                _db.SaveChanges();
            }
            catch (EstudianteException ex)
            {
                throw new EstudianteException("Error al actualizar el estudiante", ex);
            }
        }

        public Estudiante FindByEmail(string email)
        {
            return _db.Estudiantes.Where(e => e.Email == email).Include(e => e.Cursos).ThenInclude(c => c.Docente).FirstOrDefault();
        }

        public bool TerminoCurso(Curso curso, Estudiante estudiante)
        {
            var cursoObtenido = _db.Cursos
                .Include(c => c.Pruebas)
                .ThenInclude(p => p.Calificaciones)
                .First(c => c.Id == curso.Id);

            if (!cursoObtenido.Pruebas.Any())
                return false;

            return cursoObtenido.Pruebas.All(p =>
                p.Calificaciones.Any(c => c.EstudianteId == estudiante.Id)); //TODO verificar que todas las calificaciones sean superior a 60
        }
    }
}
