using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Calificaciones;

namespace NextLevel.Compartidos.DTOs.Pruebas
{
    public record PruebaDTO(string Nombre, DateTime Fecha, IEnumerable<CalificacionDTO> Calificaciones);
}
