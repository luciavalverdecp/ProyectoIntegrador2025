using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Semana
{
    public class SemanaException : Exception
    {
        public SemanaException()
        {

        }
        public SemanaException(string? message) : base(message)
        {

        }
        public SemanaException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
