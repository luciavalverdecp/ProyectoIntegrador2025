using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Cursos
{
    public interface IAgregarClase
    {
        void Ejecutar(AgendarClaseDTO claseAgregada);
    }
}
