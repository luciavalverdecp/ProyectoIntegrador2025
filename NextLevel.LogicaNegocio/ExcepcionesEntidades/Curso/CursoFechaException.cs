using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso
{
    public class CursoFechaException : CursoException
    {
        public CursoFechaException()
        {

        }
        public CursoFechaException(string? message) : base(message)
        {

        }
        public CursoFechaException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
