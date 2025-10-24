using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Usuarios
{
    public class LoginUsuario : ILoginUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public LoginUsuario(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public Usuario Ejecutar(string email, string pwd)
        {
            Usuario usu = _repositorioUsuario.FindByEmail(email);
            if (usu != null && usu.Password == pwd)
            {
                return usu;
            }
            else
            {
                throw new UsuarioException("Usuario invalido.");
            }
        }
    }
}
