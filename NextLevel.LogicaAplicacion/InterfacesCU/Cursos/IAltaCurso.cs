using Microsoft.AspNetCore.Http;
using NextLevel.Compartidos.DTOs.Cursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Cursos
{
    public interface IAltaCurso
    {
        Task Ejecutar(CursoAltaDTO cursoAltaDTO, List<IFormFile> archivos, string email, IFormFile imagen);
    }
}
