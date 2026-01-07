using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioConversacion : IRepositorioConversacion
    {
        private NextLevelContext _db;
        public RepositorioConversacion(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Conversacion obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Conversacion> FindAll()
        {
            throw new NotImplementedException();
        }

        public Conversacion FindById(int id)
        {
            return _db.Conversaciones.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Conversacion obj)
        {
            throw new NotImplementedException();
        }
    }
}
