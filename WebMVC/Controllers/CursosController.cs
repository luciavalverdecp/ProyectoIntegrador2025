using Microsoft.AspNetCore.Mvc;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;

namespace WebMVC.Controllers
{
    public class CursosController : Controller
    {
        private readonly IObtenerCursos _obtenerCursos;
        public CursosController(IObtenerCursos obtenerCursos)
        {
            _obtenerCursos = obtenerCursos;
        }
        public IActionResult ListadoCursos()
        {
            var cursos = _obtenerCursos.Ejecutar();
            return View(cursos);
        }
    }
}
