using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;

namespace WebMVC.Controllers
{
    public class CursosController : Controller
    {
        private readonly IObtenerCursos _obtenerCursos;
        private readonly IObtenerCursosFiltrados _obtenerCursosFiltrados;
        public CursosController(IObtenerCursos obtenerCursos, IObtenerCursosFiltrados obtenerCursosFiltrados)
        {
            _obtenerCursos = obtenerCursos;
            _obtenerCursosFiltrados = obtenerCursosFiltrados;
        }
        public IActionResult ListadoCursos(string? filtro, string? opcionMenu)
        {
            IEnumerable<CursoVistaPreviaDTO> cursos;

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                cursos = _obtenerCursosFiltrados.Ejecutar(filtro);
            }
            else
            {
                cursos = _obtenerCursos.Ejecutar();
            }

            ViewBag.OpcionMenu = opcionMenu; // para usar en la vista si querés resaltar la opción
            return View(cursos);
        }
    }
}
