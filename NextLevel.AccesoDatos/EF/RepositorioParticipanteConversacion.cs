using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioParticipanteConversacion : IRepositorioParticiapanteConversacion
    {
        private NextLevelContext _db;
        public RepositorioParticipanteConversacion(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(ParticipanteConversacion obj)
        {
            try
            {
                _db.ParticipanteConversaciones.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar un participante a una conversacion");
            }
        }

        public IEnumerable<ParticipanteConversacion> FindAll()
        {
            throw new NotImplementedException();
        }

        public ParticipanteConversacion FindById(int id)
        {
            throw new NotImplementedException();
        }

        public ParticipanteConversacion GetPartConv(Conversacion conversacion, Usuario usuario)
        {
            try
            {
                return _db.ParticipanteConversaciones.Where(pc => pc.ConversacionId == conversacion.Id && pc.UsuarioId == usuario.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener al participante de una conversacion.");
            }
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ParticipanteConversacion obj)
        {
            throw new NotImplementedException();
        }
    }
}
