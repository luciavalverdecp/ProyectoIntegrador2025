using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Material
{
    public class MaterialNombreException : MaterialException
    {
        public MaterialNombreException()
        {

        }
        public MaterialNombreException(string? message) : base(message)
        {

        }
        public MaterialNombreException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
