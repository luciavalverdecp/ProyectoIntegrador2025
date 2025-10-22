using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante
{
    public class EstudianteException : Exception
    {
        public EstudianteException()
        {

        }
        public EstudianteException(string? message) : base(message)
        {

        }
        public EstudianteException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
