using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Estudiantes;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes
{
    public interface ICursosTerminados
    {
        List<CursoNombreDTO> Ejecutar(EstudianteInfoDTO estudiante);
    }
}
