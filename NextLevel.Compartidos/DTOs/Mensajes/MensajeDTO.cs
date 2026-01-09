using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Conversaciones;
using NextLevel.Compartidos.DTOs.Usuarios;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;

namespace NextLevel.Compartidos.DTOs.Mensajes
{
    public record MensajeDTO(ConversacionDTO Conversacion, UsuarioNombreEmailDTO Usuario, string Contenido, DateTime FechaEnvio, bool EsDelEstudiante);
}
