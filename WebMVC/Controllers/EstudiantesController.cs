using Microsoft.AspNetCore.Mvc;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;

namespace WebMVC.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly IObtenerMisCursos obtenerMisCursos;

        public EstudiantesController(IObtenerMisCursos obtenerMisCursos)
        {
            this.obtenerMisCursos = obtenerMisCursos;
        }

        public IActionResult MisCursos()
        {
            if(HttpContext.Session.GetString("rolLogueado") == "Estudiante")
            {
                var misCursos = obtenerMisCursos.Ejecutar(HttpContext.Session.GetString("emailLogueado"));
                return View(misCursos);
            }
            return RedirectToAction("Login", "Usuarios");    
        }
    }
}
