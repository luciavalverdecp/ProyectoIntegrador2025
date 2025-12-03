using NextLevel.Compartidos.DTOs.Temarios;
using NextLevel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class TemarioMapper
    {
        public static TemarioVistaPreviaDTO ToTemarioVistaPreviaDTO(Temario temario)
        {
            return new TemarioVistaPreviaDTO(temario.Tema, CursoMapper.ToCursoNombreDTO(temario.Curso));
        }
        public static Temario FromTemarioVistaPreviaDTO(TemarioVistaPreviaDTO temarioVistaPreviaDTO)
        {
            return new Temario(temarioVistaPreviaDTO.Tema);
        }
        public static IEnumerable<TemarioVistaPreviaDTO> ToListaDTO(IEnumerable<Temario> temarios) 
        {
            if (temarios == null || temarios.Count() == 0) return new List<TemarioVistaPreviaDTO>();
            return temarios.Select(temario => new TemarioVistaPreviaDTO(temario.Tema, CursoMapper.ToCursoNombreDTO(temario.Curso)));
        } 
        public static IEnumerable<Temario> FromListaDTO(IEnumerable<TemarioVistaPreviaDTO> temarios)
        {
            return temarios.Select(temario => new Temario(temario.Tema, CursoMapper.FromCursoNombreDTO(temario.CursoNombreDTO)));
        }
    }
}
