using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.AltasCurso;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.Compartidos.DTOs.Mappers
{
    public class AltaCursoMapper
    {
        public static AltaCursoDTO ToAltaCursoDTO(AltaCurso alta)
        {
            if (alta == null) return null;
            return new AltaCursoDTO(CursoMapper.ToCursoAltaDTO(alta.Curso), alta.Archivos);
        }
    }
}
