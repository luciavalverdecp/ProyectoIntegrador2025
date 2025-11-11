using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.ExcepcionesEntidades.Mensajeria
{
    public class MensajeriaException : Exception
    {
        public MensajeriaException()
        {

        }
        public MensajeriaException(string? message) : base(message)
        {

        }
        public MensajeriaException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
