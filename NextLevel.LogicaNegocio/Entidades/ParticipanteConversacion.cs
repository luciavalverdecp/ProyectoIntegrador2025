using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class ParticipanteConversacion
    {
        public int ConversacionId { get; set; }
        public Conversacion Conversacion { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaIngreso { get; set; }

        protected ParticipanteConversacion() { }

        public ParticipanteConversacion(int conversacionId, int usuarioId)
        {
            ConversacionId = conversacionId;
            UsuarioId = usuarioId;
            FechaIngreso = DateTime.Now;
        }
    }
}
