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
    public class RepositorioCambioRol : IRepositorioCambioRol
    {
        private NextLevelContext _db;
        public RepositorioCambioRol(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(CambioRol obj)
        {
			try
			{
				_db.CambiosDeRol.Add(obj);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new CursoException("Error al dar de alta el curso");
			}
		}

        public IEnumerable<CambioRol> FindAll()
        {
            throw new NotImplementedException();
        }

        public CambioRol FindByEmail(string email)
        {
            return _db.CambiosDeRol.Where(c => c.Estudiante.Email == email).FirstOrDefault();
        }

        public CambioRol FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CambioRol obj)
        {
            throw new NotImplementedException();
        }
    }
}
