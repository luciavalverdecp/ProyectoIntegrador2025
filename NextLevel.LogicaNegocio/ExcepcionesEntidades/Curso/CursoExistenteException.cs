using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso
{
    public class CursoExistenteException : CursoException
    {
        public CursoExistenteException()
        {

        }
        public CursoExistenteException(string? message) : base(message)
        {

        }
        public CursoExistenteException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
