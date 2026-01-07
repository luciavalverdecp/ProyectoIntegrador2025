using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Conversaciones
{
    public record ConversacionDTO(int Id, TipoConversacion TipoConversacion, DateTime FechaCreacion);
}
