using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using NextLevel.LogicaNegocio.ValueObject.Docente;

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

        public Usuario FindByEmail(string email)
        {
            return _db.Docentes.Where(e => e.Email == email).Include(e => e.Cursos).FirstOrDefault();
        }

        public Docente FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Docente GetDocenteByNroDocente(int nroDocente)
        {
            var vo = new NroDocente(nroDocente);

            return _db.Docentes.FirstOrDefault(u => u.NroDocente == vo);
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
