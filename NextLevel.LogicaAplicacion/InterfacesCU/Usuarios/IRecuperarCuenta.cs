using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Usuarios
{
    public interface IRecuperarCuenta
    {
        void Ejecutar(string email);
        void CambiarContrasena(string email, string password);
        void ValidarVencimientoLink(string email);
    }
}
