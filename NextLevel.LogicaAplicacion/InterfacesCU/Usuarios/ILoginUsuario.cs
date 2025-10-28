using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Usuarios;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Usuarios
{
    public interface ILoginUsuario
    {
        UsuarioLoginVerificacionDTO Ejecutar(string email, string pwd);
    }
}
