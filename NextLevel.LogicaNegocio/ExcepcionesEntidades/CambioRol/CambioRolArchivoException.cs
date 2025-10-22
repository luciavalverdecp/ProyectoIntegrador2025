using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.CambioRol
{
    public class CambioRolArchivoException : CambioRolException
    {
        public CambioRolArchivoException()
        {

        }
        public CambioRolArchivoException(string? message) : base(message)
        {

        }
        public CambioRolArchivoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
