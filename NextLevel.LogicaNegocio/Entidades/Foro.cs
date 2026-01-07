using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.InterfacesEntidades;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Foro 
    {
        public int Id { get; set; }
        public int ConversacionId { get; set; }
        public Conversacion Conversacion { get; set; }

        public Foro() { }

        public Foro(Conversacion conversacion)
        {
            Conversacion = conversacion;
        }

    }
}
