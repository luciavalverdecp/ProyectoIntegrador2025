using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.CambioRol
{
    public class CambioRolException : Exception
    {
        public CambioRolException()
        {

        }
        public CambioRolException(string? message) : base(message)
        {

        }
        public CambioRolException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
