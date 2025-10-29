using NextLevel.LogicaNegocio.Entidades;
using Olimpiadas.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        Usuario FindByEmail(string email);
        Usuario FindByToken(string token);
        void UpdateContrasena(Usuario usuario);
        void UpdateUserAuthentication(Usuario usuario);
    }
}
