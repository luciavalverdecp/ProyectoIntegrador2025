using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Materiales;
using NextLevel.Compartidos.DTOs.Semanas;

namespace NextLevel.LogicaAplicacion.InterfacesCU.Materiales
{
    public interface ICRUDMaterial
    {
        Task Agregar(MaterialDTO material, int semana, string nombreCurso);
        Task Eliminar(MaterialBasicoDTO material, int semana, string nombreCurso);
    }
}
