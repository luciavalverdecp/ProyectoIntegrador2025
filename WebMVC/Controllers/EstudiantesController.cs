using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.CambioRoles;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.CambiosDeRol;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;

namespace WebMVC.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly IObtenerEstudiante obtenerEstudiante;
        private readonly ICursosTerminados cursosTerminados;
        private readonly ICambioDeRol cambioDeRol;

        public EstudiantesController(IObtenerEstudiante obtenerEstudiante, 
            ICursosTerminados terminoCurso, 
            ICambioDeRol cambioDeRol
            )
        {
            this.obtenerEstudiante = obtenerEstudiante;
            this.cursosTerminados = terminoCurso;
            this.cambioDeRol = cambioDeRol;
        }


        public IActionResult Perfil()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Estudiante")
            {
                try
                {
                    var estudiante = obtenerEstudiante.EjecutarEstudianteInfoDTO(HttpContext.Session.GetString("emailLogueado"));
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
        [HttpPost]
        public async Task<IActionResult> SolicitarCambioRol(List<IFormFile> Archivos)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Estudiante")
            {
                try
                {
                    var estudiante = obtenerEstudiante.EjecutarEstudianteEmailDTO(HttpContext.Session.GetString("emailLogueado"));
                    CambioRolDTO cambioRol = new CambioRolDTO(estudiante, new List<Archivo>());
                    await cambioDeRol.Ejecutar(cambioRol, Archivos);
                    return View();
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
