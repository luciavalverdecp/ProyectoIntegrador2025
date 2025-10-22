using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario
{
    public class UsuarioEmailException : UsuarioException
    {
        public UsuarioEmailException()
        {

        }
        public UsuarioEmailException(string? message) : base(message)
        {

        }
        public UsuarioEmailException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
