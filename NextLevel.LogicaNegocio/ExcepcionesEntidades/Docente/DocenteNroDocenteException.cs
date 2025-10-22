using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Docente
{
    public class DocenteNroDocenteException : DocenteException
    {
        public DocenteNroDocenteException()
        {

        }
        public DocenteNroDocenteException(string? message) : base(message)
        {

        }
        public DocenteNroDocenteException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
