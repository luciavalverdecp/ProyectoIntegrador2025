using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Conversacion
    {
        public int Id {  get; set; }
        public int? CursoId { get; set; }
        public Curso? Curso { get; set; }
        public TipoConversacion TipoConversacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<ParticipanteConversacion> Participantes { get; set; } = new();
        public List<Mensaje> Mensajes { get; set; } = new();

        public Conversacion(TipoConversacion tipoConversacion, int cursoId)
        {
            TipoConversacion = tipoConversacion;
            FechaCreacion = DateTime.Now;
            CursoId = cursoId;
        }

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
