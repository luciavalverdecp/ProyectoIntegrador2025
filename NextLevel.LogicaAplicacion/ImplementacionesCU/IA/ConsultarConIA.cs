using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaAplicacion.InterfacesCU.IA;
using NextLevel.LogicaAplicacion.InterfacesServicios;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
namespace NextLevel.LogicaAplicacion.ImplementacionesCU.IA
{
    public class ConsultarConIA : IConsultarConIA
    {
        private readonly IOpenAIService _openAI;
        private readonly IRepositorioCurso _cursoRepo;

        public ConsultarConIA(
            IOpenAIService openAI,
            IRepositorioCurso cursoRepo)
        {
            _openAI = openAI;
            _cursoRepo = cursoRepo;
        }

        public async Task<string> Ejecutar(string mensaje)
        {
            var criterios = await _openAI.InterpretarPregunta(mensaje);

            var cursos = _cursoRepo.Buscar(
                criterios.Categoria,
                criterios.Dificultad,
                criterios.DuracionMax
            ).ToList();

            return await _openAI.GenerarRespuesta(mensaje, cursos);
        }
    }
}
