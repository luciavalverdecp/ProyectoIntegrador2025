using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioDocente : IRepositorioDocente
    {
        private NextLevelContext _db;
        public RepositorioDocente(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Docente obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Docente> FindAll()
        {
            throw new NotImplementedException();
        }

        public Docente FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Docente obj)
        {
            throw new NotImplementedException();
        }
    }
}
