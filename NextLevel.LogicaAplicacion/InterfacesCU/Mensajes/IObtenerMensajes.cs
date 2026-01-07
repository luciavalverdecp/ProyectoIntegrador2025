using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Conversaciones;
using NextLevel.Compartidos.DTOs.Mensajes;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Mensajes
{
    public interface IObtenerMensajes
    {
        IEnumerable<MensajeDTO> Ejecutar(ConversacionDTO conversacion);
    }
}
