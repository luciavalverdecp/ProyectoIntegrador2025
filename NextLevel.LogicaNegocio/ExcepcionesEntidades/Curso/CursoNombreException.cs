using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso
{
    public class CursoNombreException : CursoException
    {
        public CursoNombreException()
        {

        }
        public CursoNombreException(string? message) : base(message)
        {

        }
        public CursoNombreException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
