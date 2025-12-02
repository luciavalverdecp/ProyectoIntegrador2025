using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;

namespace WebMVC.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly IObtenerEstudiante obtenerEstudiante;
        private readonly ICursosTerminados cursosTerminados;

        public EstudiantesController(IObtenerEstudiante obtenerEstudiante, 
            ICursosTerminados terminoCurso
            )
        {
            this.obtenerEstudiante = obtenerEstudiante;
            this.cursosTerminados = terminoCurso;
        }


        public IActionResult Perfil()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Estudiante")
            {
                try
                {
                    var estudiante = obtenerEstudiante.Ejectuar(HttpContext.Session.GetString("emailLogueado"));
                    ViewBag.CursosFinalizados = cursosTerminados.Ejecutar(estudiante);
                    return View(estudiante);
                }
                catch (EstudianteException ex)
                {
                    return Redirect("Usuarios/Login");
                }
                catch (Exception ex)
                {
                    return Redirect("Usuarios/Login");
                }
            }
            return RedirectToAction("Login", "Usuarios");
            
        }
    }
}
