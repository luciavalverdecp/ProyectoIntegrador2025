using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario
{
    public class UsuarioTelefonoException : UsuarioException
    {
        public UsuarioTelefonoException()
        {

        }
        public UsuarioTelefonoException(string? message) : base(message)
        {

        }
        public UsuarioTelefonoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
