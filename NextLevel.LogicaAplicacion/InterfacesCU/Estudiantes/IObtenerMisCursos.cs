using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes
{
    public interface IObtenerMisCursos
    {
        IEnumerable<CursoVistaPreviaDTO> Ejecutar(string email);
    }
}
