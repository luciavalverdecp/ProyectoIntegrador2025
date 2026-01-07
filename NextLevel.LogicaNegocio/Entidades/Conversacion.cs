using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Conversacion
    {
        public int Id {  get; set; }
        public TipoConversacion TipoConversacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Conversacion(TipoConversacion tipoConversacion)
        {
            TipoConversacion = tipoConversacion;
            FechaCreacion = DateTime.Now;
        }

        public Conversacion()
        {
            FechaCreacion = DateTime.Now;
        }
    }
}
