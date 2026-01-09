using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.ParticipantesConversacion;

namespace NextLevel.LogicaAplicacion.InterfacesCU.ParticipantesConversacion
{
    public interface IObtenerPartiConversaciones
    {
        IEnumerable<ParticipanteConversacionDTO> Ejecutar(string nombreCurso, string emailLogueado);
        ParticipanteConversacionDTO Ejecutar2(string nombreCurso, string emailLogueado);
    }
}
