using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante
{
    public class EstudianteNoEncontradoException : EstudianteException
    {
        public EstudianteNoEncontradoException()
        {

        }
        public EstudianteNoEncontradoException(string? message) : base(message)
        {

        }
        public EstudianteNoEncontradoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
