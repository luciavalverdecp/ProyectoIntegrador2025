using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioCurso : IRepositorioCurso
    {
        private NextLevelContext _db;
        public RepositorioCurso(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Curso obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Curso> FindAll()
        {
            throw new NotImplementedException();
        }

        public Curso FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Curso obj)
        {
            throw new NotImplementedException();
        }
    }
}
