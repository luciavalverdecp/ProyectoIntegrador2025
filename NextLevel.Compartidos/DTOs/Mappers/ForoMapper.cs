using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.Compartidos.DTOs.Foros;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class ForoMapper
    {
        public static ForoDTO ToForoDTO(Foro foro)
        {
            return new ForoDTO(MensajeMapper.ToListMensajesDTO(foro.Mensajes));
        }
        public static Foro FromForoDTO(ForoDTO foro, IEnumerable<EstudianteEmailDTO> estudiantesEmailDTO)
        {
            return new Foro()
            {
                Mensajes = MensajeMapper.FromListMensajesDTO(foro.Mensajes, estudiantesEmailDTO).ToList()
            };
        }
    }
}
