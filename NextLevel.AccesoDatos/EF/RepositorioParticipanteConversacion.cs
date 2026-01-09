using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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

        public ParticipanteConversacion GetPartConvEstudianteCurso(Curso curso, Usuario usuario)
        {
            try
            {
                return _db.ParticipanteConversaciones.Where(pc => pc.Conversacion.CursoId == curso.Id
                                                            && pc.UsuarioId == usuario.Id 
                                                            && pc.Conversacion.TipoConversacion == TipoConversacion.Privada)
                                                    .Include(pc => pc.Conversacion)
                                                    .FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener al participante de una conversacion.");
            }
        }

        public IEnumerable<ParticipanteConversacion> GetPartConvCurso(Curso curso, Usuario usuario)
        {
            try
            {
                var conversaciones = _db.Conversaciones.Where(c => c.Curso.Nombre == curso.Nombre && c.TipoConversacion == TipoConversacion.Privada);
                return _db.ParticipanteConversaciones.Where(pc => conversaciones.Contains(pc.Conversacion) &&  pc.UsuarioId != usuario.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo obtener el listado de participantes de una conversacion, de un docente.");
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
