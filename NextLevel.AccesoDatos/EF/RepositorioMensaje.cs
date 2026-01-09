using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioMensaje : IRepositorioMensaje
    {
        private NextLevelContext _db;
        public RepositorioMensaje(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Mensaje obj)
        {
            try
            {
                _db.Mensajes.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar mensaje");
            }
        }

        public IEnumerable<Mensaje> FindAll()
        {
            throw new NotImplementedException();
        }

        public Mensaje FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Mensaje> GetByConversacion(Conversacion conversacion)
        {
            try
            {
                return _db.Mensajes.Where(m => m.Conversacion.Id == conversacion.Id)
                    .Include(m => m.Usuario)
                    .Include(m => m.Conversacion).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener los mensajes desde el servidor");
            }
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Mensaje obj)
        {
            throw new NotImplementedException();
        }
    }
}
