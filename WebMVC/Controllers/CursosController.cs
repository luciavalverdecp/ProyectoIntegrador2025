using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;

namespace WebMVC.Controllers
{
    public class CursosController : Controller
    {
        private readonly IObtenerCursosFiltrados _obtenerCursosFiltrados;
        public CursosController(IObtenerCursosFiltrados obtenerCursosFiltrados)
        {
            _obtenerCursosFiltrados = obtenerCursosFiltrados;
        }
        public IActionResult ListadoCursos(string? filtro, string? opcionMenu, string? alfabetico, int? calificacion, string? docente)
        {
            var cursos = _obtenerCursosFiltrados.Ejecutar(filtro, opcionMenu, alfabetico, calificacion, docente);

            return View(cursos);
        }
    }
}
