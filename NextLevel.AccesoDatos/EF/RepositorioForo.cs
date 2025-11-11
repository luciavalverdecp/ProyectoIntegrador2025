using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioForo : IRepositorioForo
    {
        private NextLevelContext _db;
        public RepositorioForo(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Foro obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Foro> FindAll()
        {
            throw new NotImplementedException();
        }

        public Foro FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Foro obj)
        {
            throw new NotImplementedException();
        }
    }
}
