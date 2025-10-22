using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario
{
    public class UsuarioPasswordException : UsuarioException
    {
        public UsuarioPasswordException()
        {

        }
        public UsuarioPasswordException(string? message) : base(message)
        {

        }
        public UsuarioPasswordException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
