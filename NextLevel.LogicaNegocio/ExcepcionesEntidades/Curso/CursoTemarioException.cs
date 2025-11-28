using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso
{
    public class CursoTemarioException : CursoException
    {
        public CursoTemarioException()
        {

        }
        public CursoTemarioException(string? message) : base(message)
        {

        }
        public CursoTemarioException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
