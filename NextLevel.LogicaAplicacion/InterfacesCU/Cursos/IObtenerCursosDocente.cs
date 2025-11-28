using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Cursos
{
    public interface IObtenerCursosDocente
    {
        IEnumerable<CursoVistaPreviaDTO> Ejecutar(string email);
    }
}
