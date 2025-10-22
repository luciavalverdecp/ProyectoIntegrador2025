using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante
{
    public class EstudianteCedulaException : EstudianteException
    {
        public EstudianteCedulaException()
        {

        }
        public EstudianteCedulaException(string? message) : base(message)
        {

        }
        public EstudianteCedulaException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
