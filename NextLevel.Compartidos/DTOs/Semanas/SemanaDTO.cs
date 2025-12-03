using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Materiales;

namespace NextLevel.Compartidos.DTOs.Semanas
{
    public record SemanaDTO(int Numero, IEnumerable<MaterialBasicoDTO> Materiales, DateTime FechaInicio);
}
