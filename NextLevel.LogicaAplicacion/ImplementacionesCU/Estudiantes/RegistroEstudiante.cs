using NextLevel.AccesoDatos.EF;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiante;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using NextLevel.LogicaNegocio.SistemaAutenticacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Estudiantes
{
    public class RegistroEstudiante : IRegistroEstudiante
    {
        private readonly IRepositorioEstudiante _repositorioEstudiante;
        private readonly IRepositorioUsuario _repositorioUsuario;
        public RegistroEstudiante(IRepositorioEstudiante repositorioEstudiante, IRepositorioUsuario repositorioUsuario)
        {
            _repositorioEstudiante = repositorioEstudiante;
            _repositorioUsuario = repositorioUsuario;
        }

        public void Ejecutar(EstudianteRegistroDTO estudianteDTO, string token)
        {
            Usuario usuExistente = _repositorioUsuario.FindByEmail(estudianteDTO.Email);
            if (usuExistente == null && estudianteDTO.Password.Length >= 8)
            {
                Estudiante estudiante = EstudianteMapper.FromEstudianteRegistroDTO(estudianteDTO);
                VerificacionEmail enviarmail =  new VerificacionEmail();
                estudiante.TokenVerificacion = token;
                enviarmail.EnviarCorreoVerificacion(estudiante.Email, estudiante.TokenVerificacion);
                _repositorioEstudiante.Add(estudiante);
            }
            else if (estudianteDTO.Password.Length < 8)
            {
                throw new UsuarioPasswordException("La contraseña debe contener al menos 8 caracteres.");
            }
            else
            {
                throw new UsuarioEmailException("El usuario ya existe.");
            }
        }

        public void VerificarEmail(string token)
        {
            Usuario usuExistente = _repositorioUsuario.FindByToken(token);
            if (usuExistente != null && usuExistente.TokenVencimiento > DateTime.UtcNow)
            {
                _repositorioUsuario.UpdateUserAuthentication(usuExistente);
            }
            else if(usuExistente != null && usuExistente.TokenVencimiento < DateTime.UtcNow)
            {
                throw new UsuarioTokenVencimientoException("El enlace de verificación ya caducó.");
            }
            else
            {
                throw new UsuarioException("Error al verificar el email.");//TODO Crear excepcion por token
            }
        }

        public void CancelarVerificacion(string token)
        {
            Usuario usuExistente = _repositorioUsuario.FindByToken(token);
            if (usuExistente != null)
            {
                _repositorioUsuario.Remove(usuExistente.Id);
            }
            else
            {
                throw new UsuarioException("El correo ya se encuentra verificado");
            }
        }

        public void ActualizarVerificacion(string emailDestino, string token)
        {
            Usuario usuExistente = _repositorioUsuario.FindByEmail(emailDestino);
            if (usuExistente != null && !usuExistente.EstaVerificado)
            {
                VerificacionEmail enviarmail = new VerificacionEmail();
                usuExistente.TokenVerificacion = token;
                usuExistente.TokenVencimiento = DateTime.UtcNow.AddHours(24);
                enviarmail.EnviarCorreoVerificacion(usuExistente.Email, usuExistente.TokenVerificacion);
                _repositorioUsuario.Update(usuExistente);
            }
            else
            {
                throw new UsuarioException("El correo ya se encuentra verificado");
            }
        }
    }
}
