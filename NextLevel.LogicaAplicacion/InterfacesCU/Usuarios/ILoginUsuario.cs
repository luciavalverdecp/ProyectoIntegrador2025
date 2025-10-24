using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Usuarios
{
    public interface ILoginUsuario
    {
        Usuario Ejecutar(string email, string pwd);
    }
}
