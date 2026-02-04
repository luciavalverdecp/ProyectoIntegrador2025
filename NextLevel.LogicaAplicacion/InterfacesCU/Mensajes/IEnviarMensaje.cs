using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Mensajes
{
    public interface IEnviarMensaje
    {
        int Ejecutar(int idConversacion, UsuarioEmailDTO usuarioDTO, string Contenido, CursoDTO cursoDTO);
    }
}
