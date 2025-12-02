using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso
{
    public class CursoDescripcionException : CursoException
    {
        public CursoDescripcionException()
        {

        }
        public CursoDescripcionException(string? message) : base(message)
        {

        }
        public CursoDescripcionException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
