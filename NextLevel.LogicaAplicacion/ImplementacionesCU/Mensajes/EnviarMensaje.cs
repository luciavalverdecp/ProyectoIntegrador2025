using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;
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
        private readonly IRepositorioDocente repositorioDocente;
        private readonly IRepositorioCurso repositorioCurso;
        public EnviarMensaje(IRepositorioMensaje repositorioMensaje, 
            IRepositorioConversacion repositorioConversacion,
            IRepositorioUsuario repositorioUsuario,
            IRepositorioParticiapanteConversacion repositorioParticiapanteConversacion,
            IRepositorioDocente repositorioDocente,
            IRepositorioCurso repositorioCurso)
        {
            this.repositorioMensaje = repositorioMensaje;
            this.repositorioConversacion = repositorioConversacion;
            this.repositorioUsuario = repositorioUsuario;
            this.repositorioParticiapanteConversacion = repositorioParticiapanteConversacion;
            this.repositorioDocente = repositorioDocente;
            this.repositorioCurso = repositorioCurso;
        }

        public int Ejecutar(int idConversacion, UsuarioEmailDTO usuarioDTO, string Contenido, CursoDTO cursoDTO)
        {
            if (idConversacion == -1) 
            {
                Curso curso = repositorioCurso.FindByNombre(cursoDTO.Nombre);
                Conversacion nuevaConversacion = new Conversacion(TipoConversacion.Privada, curso.Id);
                repositorioConversacion.Add(nuevaConversacion);
                idConversacion = nuevaConversacion.Id;
            }
            if (Contenido == null || Contenido == "") return idConversacion;
            Conversacion conversacion = repositorioConversacion.FindById(idConversacion);
            if (conversacion == null) throw new Exception("No se pudo obtener la conversacion");
            Usuario usuario = repositorioUsuario.FindByEmail(usuarioDTO.email);
            if (usuario == null) throw new Exception("No se puedo encontrar un usuario con ese email");
            ParticipanteConversacion pc = repositorioParticiapanteConversacion.GetPartConv(conversacion, usuario);

            if (pc == null)
            {
                pc = new ParticipanteConversacion(conversacion.Id, usuario.Id);
                repositorioParticiapanteConversacion.Add(pc);
                if(conversacion.TipoConversacion == TipoConversacion.Privada)
                {
                    Docente docente = repositorioDocente.FindByNomreYCurso(cursoDTO.Nombre, cursoDTO.DocenteNombreDTO.NombreCompleto);
                    pc = new ParticipanteConversacion(conversacion.Id, docente.Id);
                    repositorioParticiapanteConversacion.Add(pc);
                }
            }

            Mensaje mensajeNuevo = new Mensaje(conversacion, usuario, Contenido);
            repositorioMensaje.Add(mensajeNuevo);
            return idConversacion;
        }
    }
}
