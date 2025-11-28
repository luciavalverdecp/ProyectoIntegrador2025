using Microsoft.AspNetCore.Mvc;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;

namespace WebMVC.Controllers
{
    public class CursosController : Controller
    {
        private readonly IObtenerCursosFiltrados _obtenerCursosFiltrados;
        private readonly IObtenerCursosDocente _obtenerCursosDocente;
        public CursosController(IObtenerCursosFiltrados obtenerCursosFiltrados, IObtenerCursosDocente obtenerCursosDocente)
        {
            _obtenerCursosFiltrados = obtenerCursosFiltrados;
            _obtenerCursosDocente = obtenerCursosDocente;
        }
        public IActionResult ListadoCursos(string? filtro, string? opcionMenu, string? alfabetico, int? calificacion, string? docente)
        {
            var cursos = _obtenerCursosFiltrados.Ejecutar(filtro, opcionMenu, alfabetico, calificacion, docente);

            return View(cursos);
        }

        public IActionResult ListadoCursosDocente()
        {
            var cursosDelDocente = _obtenerCursosDocente.Ejecutar(HttpContext.Session.GetString("emailLogueado"));
            return View(cursosDelDocente);
        }
    }
}
