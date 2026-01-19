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
    public class RepositorioPostulacion : IRepositorioPostulacion
    {
        private NextLevelContext _db;
        public RepositorioPostulacion(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Postulacion obj)
        {
            try
            {
                _db.Postulaciones.Add(obj);
                _db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("Error al dar de alta una postulacion");
            }
        }

        public IEnumerable<Postulacion> FindAll()
        {
            throw new NotImplementedException();
        }

        public Postulacion FindById(int id)
        {
            return _db.Postulaciones.Where(p => p.Id == id)
                     .Include(p => p.CambioRol)
                     .Include(p => p.CambioRol)
                        .ThenInclude(cr => cr.Estudiante)
                     .Include(p => p.CambioRol)
                        .ThenInclude(cr => cr.Archivos)
                     .Include(p => p.AltaCurso)
                        .ThenInclude(a => a.Curso)
                     .Include(p => p.AltaCurso)
                        .ThenInclude(a => a.Curso)
                            .ThenInclude(c => c.Docente)
                     .Include(p => p.AltaCurso)
                        .ThenInclude(a => a.Curso)
                            .ThenInclude(c => c.Temarios)
                     .Include(p => p.AltaCurso)
                        .ThenInclude(a => a.Archivos)
                 .FirstOrDefault();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Postulacion obj)
        {
            var post = _db.Postulaciones.Find(obj.Id);
            try
            {
                post.Estado = obj.Estado;
                _db.Postulaciones.Update(post);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new CursoException("Error al actualizar la postulacion");
            }
        }
    }
}
