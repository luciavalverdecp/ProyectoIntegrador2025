using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario
{
    public class UsuarioTokenVencimientoException : UsuarioException
    {
        public UsuarioTokenVencimientoException()
        {

        }
        public UsuarioTokenVencimientoException(string? message) : base(message)
        {

        }
        public UsuarioTokenVencimientoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
