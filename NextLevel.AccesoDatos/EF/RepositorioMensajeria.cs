using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioMensajeria : IRepositorioMensajeria
    {
        private NextLevelContext _db;
        public RepositorioMensajeria(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Mensajeria obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mensajeria> FindAll()
        {
            throw new NotImplementedException();
        }

        public Mensajeria FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Mensajeria obj)
        {
            throw new NotImplementedException();
        }
    }
}
