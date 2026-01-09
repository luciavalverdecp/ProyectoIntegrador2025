using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Conversaciones;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class ConversacionMapper
    {
        public static Conversacion FromConversacionDTO(ConversacionDTO conversacion)
        {
            return new Conversacion()
            {
                Id = conversacion.Id, 
                TipoConversacion = conversacion.TipoConversacion
            };
        }

        public static ConversacionDTO ToConversacionDTO(Conversacion conversacion)
        {
            return new ConversacionDTO(conversacion.Id, CursoMapper.ToCursoNombreDTO(conversacion.Curso), conversacion.TipoConversacion, conversacion.FechaCreacion);
        }
    }
}
