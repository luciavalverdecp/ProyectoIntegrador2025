using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Foro
{
    public class ForoException : Exception
    {
        public ForoException()
        {

        }
        public ForoException(string? message) : base(message)
        {

        }
        public ForoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
