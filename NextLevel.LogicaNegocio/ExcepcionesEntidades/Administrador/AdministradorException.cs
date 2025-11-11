using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Administrador
{
    public class AdministradorException : Exception
    {
        public AdministradorException()
        {

        }
        public AdministradorException(string? message) : base(message)
        {

        }
        public AdministradorException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
