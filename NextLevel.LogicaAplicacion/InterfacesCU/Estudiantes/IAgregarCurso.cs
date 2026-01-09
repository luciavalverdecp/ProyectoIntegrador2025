using NextLevel.Compartidos.DTOs.Cursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes
{
    public interface IAgregarCurso
    {
        void Ejecutar(string email, string cursoNombre);
    }
}
