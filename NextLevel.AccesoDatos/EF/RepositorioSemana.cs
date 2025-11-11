using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioSemana : IRepositorioSemana
    {
        private NextLevelContext _db;
        public RepositorioSemana(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Semana obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Semana> FindAll()
        {
            throw new NotImplementedException();
        }

        public Semana FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Semana obj)
        {
            throw new NotImplementedException();
        }
    }
}
