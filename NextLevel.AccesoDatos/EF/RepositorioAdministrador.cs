using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioAdministrador : IRepositorioAdministrador
    {
        private NextLevelContext _db;
        public RepositorioAdministrador(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Administrador obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Administrador> FindAll()
        {
            throw new NotImplementedException();
        }

        public Administrador FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Administrador obj)
        {
            throw new NotImplementedException();
        }
    }
}
