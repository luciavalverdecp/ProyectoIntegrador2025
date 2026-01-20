using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.Compartidos.DTOs.Usuarios;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using Olimpiadas.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Usuarios
{
    public class LoginUsuario : ILoginUsuario
    {
        private readonly IRepositorioEstudiante _repositorioEstudiante;
        private readonly IRepositorioDocente _repositorioDocente;
        private readonly IRepositorioAdministrador _repositorioAdmin;

        public LoginUsuario(IRepositorioEstudiante repositorioEstudiante, 
            IRepositorioDocente repositorioDocente,
            IRepositorioAdministrador repositorioAdmin)
        {
            _repositorioEstudiante = repositorioEstudiante;
            _repositorioDocente = repositorioDocente;
            _repositorioAdmin = repositorioAdmin;
        }

        public UsuarioLoginVerificacionDTO Ejecutar(string email, string pwd)
        {
            int.TryParse(email, out int nroDocente);
            Usuario usu = null;
            if (nroDocente != 0)
            {
                usu = _repositorioDocente.GetDocenteByNroDocente(nroDocente);
            }
            else
            {
                usu = _repositorioEstudiante.FindByEmail(email);
                if(usu == null) usu = _repositorioAdmin.FindByEmail(email);
            }
            
            if (usu != null && usu.Password == pwd)
            {
                if (!usu.EstaVerificado) throw new UsuarioEstaVerificadoException("El usuario no se encuentra verificado.");
                var usuarioDto = UsuarioMapper.ToUsuarioLoginVerificacion(usu);
                return usuarioDto;
            }
            else
            {
                throw new UsuarioException("Usuario invalido.");
            }
        }
    }
}
