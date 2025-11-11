using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
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
            throw new NotImplementedException();
        }

        public IEnumerable<CambioRol> FindAll()
        {
            throw new NotImplementedException();
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
