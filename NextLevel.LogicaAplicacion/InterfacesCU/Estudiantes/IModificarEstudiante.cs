using NextLevel.Compartidos.DTOs.Estudiantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes
{
    public interface IModificarEstudiante
    {
        void Ejecutar(EstudianteInfoDTO estudiante);
    }
}
