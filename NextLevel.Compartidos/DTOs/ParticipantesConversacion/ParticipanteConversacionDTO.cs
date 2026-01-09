using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Conversaciones;
using NextLevel.Compartidos.DTOs.Usuarios;

namespace NextLevel.Compartidos.DTOs.ParticipantesConversacion
{
    public record ParticipanteConversacionDTO(ConversacionDTO Conversacion, UsuarioNombreEmailDTO Usuario); 
}
