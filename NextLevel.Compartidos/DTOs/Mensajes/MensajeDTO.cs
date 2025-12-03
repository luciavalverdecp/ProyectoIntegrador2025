using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Usuarios;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;

namespace NextLevel.Compartidos.DTOs.Mensajes
{
    public record MensajeDTO(UsuarioEmailDTO Usuario, string mensaje, bool EsDelEstudiante);
}
