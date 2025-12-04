using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioAltaCurso : IRepositorioAltaCurso
    {
        private NextLevelContext _db;
        public RepositorioAltaCurso(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(AltaCurso obj)
        {
            try
            {
                _db.AltaCursos.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new CursoException("Error al dar de alta el curso");//
            }
        }

        public IEnumerable<AltaCurso> FindAll()
        {
            throw new NotImplementedException();
        }

        public AltaCurso FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(AltaCurso obj)
        {
            throw new NotImplementedException();
        }
    }
}
