using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.Compartidos.DTOs.Mensajes;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class MensajeMapper
    {
        public static IEnumerable<MensajeDTO> ToListMensajesDTO(IEnumerable<Mensaje> mensajes)
        {
            if (mensajes == null || mensajes.Count() == 0) return new List<MensajeDTO>();
            return mensajes.Select(m => new MensajeDTO(ConversacionMapper.ToConversacionDTO(m.Conversacion), UsuarioMapper.ToUsuarioNombreEmailDTO(m.Usuario), m.Contenido, m.FechaEnvio, m.EsDelEstudiante));
        }
        public static IEnumerable<Mensaje> FromListMensajesDTO(IEnumerable<MensajeDTO> mensajes, IEnumerable<EstudianteEmailDTO> estudiantesEmailDTO)
        {
            if (mensajes == null || mensajes.Count() == 0) return new List<Mensaje>();
            return mensajes.Select(m => new Mensaje(UsuarioMapper.FromUsuarioEmailDTO(m.Usuario, estudiantesEmailDTO), m.mensaje));
        }
    }
}
