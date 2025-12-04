using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.Compartidos.DTOs.Pruebas;

namespace NextLevel.Compartidos.DTOs.Calificaciones
{
    public record CalificacionDTO(double Puntaje, EstudianteEmailDTO Estudiante);
}
