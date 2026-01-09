using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Conversaciones;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Conversaciones
{
    public interface IObtenerConversacion
    {
        ConversacionDTO Ejecutar(int conversacionId);
    }
}
