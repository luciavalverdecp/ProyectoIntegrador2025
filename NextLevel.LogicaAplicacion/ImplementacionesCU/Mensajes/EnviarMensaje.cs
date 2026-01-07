using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaAplicacion.InterfacesCU.Mensajes;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Mensajes
{
    public class EnviarMensaje : IEnviarMensaje
    {
        private readonly IRepositorioMensaje repositorioMensaje;
        private readonly IRepositorioConversacion repositorioConversacion;
        private readonly IRepositorioUsuario repositorioUsuario;
        private readonly IRepositorioParticiapanteConversacion repositorioParticiapanteConversacion;
        public EnviarMensaje(IRepositorioMensaje repositorioMensaje, 
            IRepositorioConversacion repositorioConversacion,
            IRepositorioUsuario repositorioUsuario,
            IRepositorioParticiapanteConversacion repositorioParticiapanteConversacion)
        {
            this.repositorioMensaje = repositorioMensaje;
            this.repositorioConversacion = repositorioConversacion;
            this.repositorioUsuario = repositorioUsuario;
            this.repositorioParticiapanteConversacion = repositorioParticiapanteConversacion;
        }

        public void Ejecutar(int idConversacion, UsuarioEmailDTO usuarioDTO, string Contenido)
        {
            if (idConversacion == null) throw new Exception("No se pudo obtener la conversacion");
            Conversacion conversacion = repositorioConversacion.FindById(idConversacion);
            if (conversacion == null) throw new Exception("No se pudo obtener la conversacion");
            Usuario usuario = repositorioUsuario.FindByEmail(usuarioDTO.email);
            if (usuario == null) throw new Exception("No se puedo encontrar un usuario con ese email");
            ParticipanteConversacion pc = repositorioParticiapanteConversacion.GetPartConv(conversacion, usuario);

            if (pc == null)
            {
                pc = new ParticipanteConversacion(conversacion.Id, usuario.Id);
                repositorioParticiapanteConversacion.Add(pc);
            }

            Mensaje mensajeNuevo = new Mensaje(conversacion, usuario, Contenido);
            repositorioMensaje.Add(mensajeNuevo);
        }
    }
}
