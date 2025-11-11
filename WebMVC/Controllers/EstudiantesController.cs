using Microsoft.AspNetCore.Mvc;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;

namespace WebMVC.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly IObtenerCursos _obtenerCursos;
        public EstudiantesController(IObtenerCursos obtenerCursos)
        {
            _obtenerCursos = obtenerCursos;
        }
        public IActionResult Explorar()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Estudiante")
            {
                var cursos = _obtenerCursos.Ejecutar();
                return View("~/Views/Cursos/ListadoCursos.cshtml", cursos);
            }
            return RedirectToAction("Login", "Usuarios");
        }
        public IActionResult MisCursos()
        {
            if(HttpContext.Session.GetString("rolLogueado") == "Estudiante")
            {
                return View();
            }
            return RedirectToAction("Login", "Usuarios");
        }
    }
}
