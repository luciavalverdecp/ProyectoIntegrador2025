using NextLevel.Compartidos.DTOs.Usuarios;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Usuarios
{
    public class RegistroUsuario : IRegistroUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public RegistroUsuario(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }
        public void Ejecutar(UsuarioRegistroDTO usuarioDTO)
        {

        }
    }
}
