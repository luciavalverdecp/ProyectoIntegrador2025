using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;

namespace WebMVC.Controllers
{
    public class CursosController : Controller
    {
        private readonly IObtenerCursosFiltrados _obtenerCursosFiltrados;
        private readonly IObtenerCursosDocente _obtenerCursosDocente;
        private readonly IObtenerCurso _obtenerCurso;
        public CursosController(IObtenerCursosFiltrados obtenerCursosFiltrados, 
            IObtenerCursosDocente obtenerCursosDocente,
             IObtenerCurso obtenerCurso)
        {
            _obtenerCursosFiltrados = obtenerCursosFiltrados;
            _obtenerCursosDocente = obtenerCursosDocente;
            _obtenerCurso = obtenerCurso;
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
            try
            {
                var cursosDTO = _obtenerCurso.Ejecturar(nombreCurso);
                return View(cursosDTO);
            }
            catch(CursoException ex)
            {
                ViewBag.Error = ex.Message;
            }catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
    }
}
