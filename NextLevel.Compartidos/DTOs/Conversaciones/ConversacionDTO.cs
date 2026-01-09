using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Conversaciones
{
    public record ConversacionDTO(int Id, CursoNombreDTO CursoNombre, TipoConversacion TipoConversacion, DateTime FechaCreacion);
}
