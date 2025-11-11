using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Docente
{
    public class DocenteException : Exception
    {
        public DocenteException()
        {

        }
        public DocenteException(string? message) : base(message)
        {

        }
        public DocenteException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
