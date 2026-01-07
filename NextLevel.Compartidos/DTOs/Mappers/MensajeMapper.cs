using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Mensajes;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class MensajeMapper
    {
        public static IEnumerable<MensajeDTO> ToListMensajesDTO(IEnumerable<Mensaje> mensajes)
        {
            if (mensajes == null || mensajes.Count() == 0) return new List<MensajeDTO>();
            return mensajes.Select(m => new MensajeDTO(ConversacionMapper.ToConversacionDTO(m.Conversacion), UsuarioMapper.ToUsuarioEmailDTO(m.Usuario), m.Contenido, m.FechaEnvio, m.EsDelEstudiante));
        }
    }
}
