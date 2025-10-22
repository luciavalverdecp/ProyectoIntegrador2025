using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario
{
    public class UsuarioNombreCompletoException : UsuarioException
    {
        public UsuarioNombreCompletoException()
        {

        }
        public UsuarioNombreCompletoException(string? message) : base(message)
        {

        }
        public UsuarioNombreCompletoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
