using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioMaterial : IRepositorioMaterial
    {
        private NextLevelContext _db;
        public RepositorioMaterial(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Material obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Material> FindAll()
        {
            throw new NotImplementedException();
        }

        public Material FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Material obj)
        {
            throw new NotImplementedException();
        }
    }
}
