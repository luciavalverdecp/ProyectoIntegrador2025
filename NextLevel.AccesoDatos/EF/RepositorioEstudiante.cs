using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioEstudiante : IRepositorioEstudiante
    {
        private NextLevelContext _db;
        public RepositorioEstudiante(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Estudiante obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Estudiante> FindAll()
        {
            throw new NotImplementedException();
        }

        public Estudiante FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Estudiante obj)
        {
            throw new NotImplementedException();
        }
    }
}
