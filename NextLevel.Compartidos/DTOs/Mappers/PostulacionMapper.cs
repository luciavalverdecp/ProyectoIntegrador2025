using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Postulaciones;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class PostulacionMapper
    {
        public static IEnumerable<PostulacionDTO> ToListPostulacionDTO(List<Postulacion> postulaciones)
        {
            if (postulaciones == null) return new List<PostulacionDTO>();
            return postulaciones.Select(p => new PostulacionDTO(p.Id, CambioRolesMapper.ToCambioRolDTO(p.CambioRol), AltaCursoMapper.ToAltaCursoDTO(p.AltaCurso), p.Estado));
        }
    }
}
