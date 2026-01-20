using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
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
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Postulacion obj)
        {
            throw new NotImplementedException();
        }
    }
}
