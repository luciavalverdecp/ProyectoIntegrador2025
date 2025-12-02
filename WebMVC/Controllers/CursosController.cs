using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Estudiantes;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;

namespace WebMVC.Controllers
{
    public class CursosController : Controller
    {
        private readonly IObtenerCursosFiltrados _obtenerCursosFiltrados;
        private readonly IObtenerCursosDocente _obtenerCursosDocente;
        private readonly IObtenerCurso _obtenerCurso;
        private readonly IObtenerMisCursos obtenerMisCursos;
        public CursosController(IObtenerCursosFiltrados obtenerCursosFiltrados, 
            IObtenerCursosDocente obtenerCursosDocente,
             IObtenerCurso obtenerCurso, 
             IObtenerMisCursos obtenerMisCursos
             )
        {
            _obtenerCursosFiltrados = obtenerCursosFiltrados;
            _obtenerCursosDocente = obtenerCursosDocente;
            _obtenerCurso = obtenerCurso;
            this.obtenerMisCursos = obtenerMisCursos;
        }
        public IActionResult ListadoCursos(string? filtro, string? opcionMenu, string? alfabetico, int? calificacion, string? docente)
        {
            var cursos = _obtenerCursosFiltrados.Ejecutar(filtro, opcionMenu, alfabetico, calificacion, docente);

            return View(cursos);
        }

        public IActionResult ListadoCursosDocente()
        {
            if(HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                var cursosDelDocente = _obtenerCursosDocente.Ejecutar(HttpContext.Session.GetString("emailLogueado"));
                return View(cursosDelDocente);
            }
            return Redirect("/Usuarios/Login");
        }

        public IActionResult VisualizarCurso(string nombreCurso)
        {
            if(HttpContext.Session.GetString("rolLogueado") == "Estudiante" ||
                HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                try
                {
                    var cursosDTO = _obtenerCurso.Ejecturar(nombreCurso);
                    return View(cursosDTO);
                }
                catch (CursoException ex)
                {
                    ViewBag.Error = ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View();
            }
            return Redirect("/Usuarios/Loguin");
        }


        public IActionResult ListadoCursosEstudiante()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Estudiante")
            {
                var misCursos = obtenerMisCursos.Ejecutar(HttpContext.Session.GetString("emailLogueado"));
                return View(misCursos);
            }
            return RedirectToAction("Login", "Usuarios");
        }
    }
}
