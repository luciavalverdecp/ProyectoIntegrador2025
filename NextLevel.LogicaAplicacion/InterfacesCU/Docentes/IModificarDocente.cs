using NextLevel.Compartidos.DTOs.Docentes;
using NextLevel.Compartidos.DTOs.Estudiantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Docentes
{
    public interface IModificarDocente
    {
        void Ejecutar(DocenteInfoDTO docente);
    }
}
