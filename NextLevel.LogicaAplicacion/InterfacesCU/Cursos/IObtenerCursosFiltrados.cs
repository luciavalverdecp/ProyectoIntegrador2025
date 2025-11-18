using NextLevel.Compartidos.DTOs.Cursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Cursos
{
    public interface IObtenerCursosFiltrados
    {
        IEnumerable<CursoVistaPreviaDTO> Ejecutar(string filtro);
    }
}
