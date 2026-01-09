using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.ParticipantesConversacion;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class ParticipanteConversacionMapper
    {
        public static IEnumerable<ParticipanteConversacionDTO> ToListParticipanteConversacionDTO(IEnumerable<ParticipanteConversacion> listado)
        {
            return listado.Select(pc => new ParticipanteConversacionDTO(ConversacionMapper.ToConversacionDTO(pc.Conversacion), UsuarioMapper.ToUsuarioNombreEmailDTO(pc.Usuario))).ToList();
        }

        public static ParticipanteConversacionDTO ToParticipanteConversacionDTO(ParticipanteConversacion pc)
        {
            return new ParticipanteConversacionDTO(ConversacionMapper.ToConversacionDTO(pc.Conversacion), UsuarioMapper.ToUsuarioNombreEmailDTO(pc.Usuario));
        }
    }
}
