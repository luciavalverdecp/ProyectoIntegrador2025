using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.CambioRol
{
    public class CambioRolDocenteExistenteException : CambioRolException
    {
        public CambioRolDocenteExistenteException()
        {

        }
        public CambioRolDocenteExistenteException(string? message) : base(message)
        {

        }
        public CambioRolDocenteExistenteException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
