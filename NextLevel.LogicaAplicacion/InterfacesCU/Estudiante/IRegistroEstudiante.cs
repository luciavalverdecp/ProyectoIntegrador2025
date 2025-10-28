using NextLevel.Compartidos.DTOs.Estudiantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Estudiante
{
    public interface IRegistroEstudiante
    {
        void Ejecutar(EstudianteRegistroDTO estudianteDTO, string token);
        void VerificarEmail(string token);
        void CancelarVerificacion(string token);
    }
}
