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
            try
            {
                _db.Conversaciones.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear una conversacion");
            }
        }

        public int Agregar(Conversacion conversacion)
        {
            try
            {
                _db.Conversaciones.Add(conversacion);
                _db.SaveChanges();
                return conversacion.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear una conversacion");
            }
        }

        public IEnumerable<Conversacion> FindAll()
        {
            throw new NotImplementedException();
        }

        public Conversacion FindById(int id)
        {
            return _db.Conversaciones.Where(c => c.Id == id).Include(c => c.Curso).Include(c => c.Participantes).Include(c => c.Mensajes).FirstOrDefault();
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
