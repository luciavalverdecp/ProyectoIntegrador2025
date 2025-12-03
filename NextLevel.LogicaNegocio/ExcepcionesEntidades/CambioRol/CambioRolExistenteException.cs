using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.CambioRol
{
    public class CambioRolExistenteException : CambioRolException
    {
        public CambioRolExistenteException()
        {

        }
        public CambioRolExistenteException(string? message) : base(message)
        {

        }
        public CambioRolExistenteException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
