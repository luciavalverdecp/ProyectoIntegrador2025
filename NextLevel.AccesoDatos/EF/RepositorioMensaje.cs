using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioMensaje : IRepositorioMensaje
    {
        private NextLevelContext _db;
        public RepositorioMensaje(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Mensaje obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mensaje> FindAll()
        {
            throw new NotImplementedException();
        }

        public Mensaje FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Mensaje obj)
        {
            throw new NotImplementedException();
        }
    }
}
