using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Docente;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Docentes
{
    public class DocenteNoEncontradoException :DocenteException
    {
        public DocenteNoEncontradoException()
        {

        }
        public DocenteNoEncontradoException(string? message) : base(message)
        {

        }
        public DocenteNoEncontradoException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
