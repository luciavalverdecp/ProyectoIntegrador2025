using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;

namespace NextLevel.LogicaAplicacion.InterfacesServicios
{
    public interface IOpenAIService
    {
        Task<CriteriosBusqueda> InterpretarPregunta(string pregunta);
        Task<string> GenerarRespuesta(string pregunta, List<Curso> cursos);
    }
}
