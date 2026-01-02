using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Estudiantes;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.AltaCurso;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Docentes;
using NextLevel.LogicaAplicacion.InterfacesCU.Docentes;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Docente;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;

namespace WebMVC.Controllers
{
    public class CursosController : Controller
    {
        private readonly IObtenerCursosFiltrados _obtenerCursosFiltrados;
        private readonly IObtenerCursosDocente _obtenerCursosDocente;
        private readonly IObtenerCurso _obtenerCurso;
        private readonly IObtenerMisCursos obtenerMisCursos;
        private readonly IAltaCurso _altaCurso;
        private readonly IAgregarClase _agregarClase;
        private readonly IObtenerDocente _obtenerDocente;
        private readonly IObtenerEstudiante _obtenerEstudiante;
        public CursosController(IObtenerCursosFiltrados obtenerCursosFiltrados,
            IObtenerCursosDocente obtenerCursosDocente,
             IObtenerCurso obtenerCurso,
             IObtenerMisCursos obtenerMisCursos,
             IAltaCurso altaCurso,
             IAgregarClase agregarClase,
             IObtenerDocente obtenerDocente,
             IObtenerEstudiante obtenerEstudiante)
        {
            _obtenerCursosFiltrados = obtenerCursosFiltrados;
            _obtenerCursosDocente = obtenerCursosDocente;
            _obtenerCurso = obtenerCurso;
            this.obtenerMisCursos = obtenerMisCursos;
            _altaCurso = altaCurso;
            _agregarClase = agregarClase;
            _obtenerDocente = obtenerDocente;
            _obtenerEstudiante = obtenerEstudiante;
        }
        public IActionResult ListadoCursos(string? filtro, string? opcionMenu, string? alfabetico, int? calificacion, string? docente)
        {
            var cursos = _obtenerCursosFiltrados.Ejecutar(filtro, opcionMenu, alfabetico, calificacion, docente);

            return View(cursos);
        }

        public IActionResult ListadoCursosDocente()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                var cursosDelDocente = _obtenerCursosDocente.Ejecutar(HttpContext.Session.GetString("emailLogueado"));
                return View(cursosDelDocente);
            }
            return Redirect("/Usuarios/Login");
        }

        public IActionResult VisualizarCurso(string nombreCurso)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Estudiante" ||
                HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                try
                {
                    var cursosDTO = _obtenerCurso.Ejecturar(nombreCurso);
                    ViewBag.ClasesAgendadas = cursosDTO.FechasClases
                        .Select(c => new { Fecha = c.ToString("yyyy-MM-dd"), Hora = c.ToString("HH:mm") }).ToList();
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
        public IActionResult AltaCurso()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                return View();
            }
            return RedirectToAction("Login", "Usuarios");
        }
        [HttpPost]
        public async Task<IActionResult> AltaCurso(CursoAltaDTO curso, List<IFormFile> archivos, IFormFile imagen)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                try
                {
                    await _altaCurso.Ejecutar(curso, archivos, HttpContext.Session.GetString("emailLogueado"), imagen);
                    TempData["MensajeAlta"] = "Curso dado de alta exitosamente.";
                    TempData["ErrorAlta"] = false;
                    return View(curso);
                }
                catch (CursoNombreException ex)
                {
                    TempData["MensajeAlta"] = ex.Message;
                    TempData["ErrorAlta"] = true;
                    return View(curso);
                }
                catch (CursoDescripcionException ex)
                {
                    TempData["MensajeAlta"] = ex.Message;
                    TempData["ErrorAlta"] = true;
                    return View(curso);
                }
                catch (CursoFechaException ex)
                {
                    TempData["MensajeAlta"] = ex.Message;
                    TempData["ErrorAlta"] = true;
                    return View(curso);
                }
                catch (CursoPrecioException ex)
                {
                    TempData["MensajeAlta"] = ex.Message;
                    TempData["ErrorAlta"] = true;
                    return View(curso);
                }
                catch (CursoDificultadException ex)
                {
                    TempData["MensajeAlta"] = ex.Message;
                    TempData["ErrorAlta"] = true;
                    return View(curso);
                }
                catch (CursoTemarioException ex)
                {
                    TempData["MensajeAlta"] = ex.Message;
                    TempData["ErrorAlta"] = true;
                    return View(curso);
                }
                catch (AltaCursoArchivosException ex)
                {
                    TempData["MensajeAlta"] = ex.Message;
                    TempData["ErrorAlta"] = true;
                    return View(curso);
                }
                catch (CursoException ex)
                {
                    TempData["MensajeAlta"] = ex.Message;
                    TempData["ErrorAlta"] = true;
                    return View(curso);
                }
                catch (AltaCursoException ex)
                {
                    TempData["MensajeAlta"] = ex.Message;
                    TempData["ErrorAlta"] = true;
                    return View(curso);
                }
            }
            return RedirectToAction("Login", "Usuarios");
        }

        public IActionResult DetallesDeUnCurso(string nombreCurso)
        {
            try
            {
                var cursoDTO = _obtenerCurso.Ejecturar(nombreCurso);
                return View(cursoDTO);
            }catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        
        [HttpPost]
        public IActionResult AgregarClase(AgendarClaseDTO claseAgendada)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                try
                {
                    _agregarClase.Ejecutar(claseAgendada);
                    TempData["MensajeAgendaClase"] = "Se agrego correctamente la clase";
                    TempData["ErrorAgendarClase"] = false;
                    return RedirectToAction("VisualizarCurso", "Cursos", new { nombreCurso = claseAgendada.CursoNombre });
                }
                catch (CursoFechaException ex)
                {
                    TempData["MensajeAgendaClase"] = ex.Message;
                    TempData["ErrorAgendarClase"] = true;
                    return RedirectToAction("VisualizarCurso", "Cursos", new { nombreCurso = claseAgendada.CursoNombre });
                }
                catch (CursoException ex)
                {
                    TempData["MensajeAgendaClase"] = ex.Message;
                    TempData["ErrorAgendarClase"] = true;
                    return RedirectToAction("VisualizarCurso", "Cursos", new { nombreCurso = claseAgendada.CursoNombre });
                }
                catch (Exception ex)
                {
                    TempData["MensajeAgendaClase"] = ex.Message;
                    TempData["ErrorAgendarClase"] = true;
                    return RedirectToAction("VisualizarCurso", "Cursos", new { nombreCurso = claseAgendada.CursoNombre });
                }
            }
            return RedirectToAction("Login", "Usuarios");
        }

        public IActionResult ClasesEnVivo(string nombreCurso)
        {
            try
            {
                string? rol = HttpContext.Session.GetString("rolLogueado");
                string? email = HttpContext.Session.GetString("emailLogueado");

                if (rol != "Docente" && rol != "Estudiante")
                    return RedirectToAction("Login", "Usuarios");

                var curso = _obtenerCurso.Ejecturar(nombreCurso);

                if (rol == "Docente")
                {
                    var docente = _obtenerDocente.Ejecutar(email);
                    if (curso.DocenteNombreDTO.NroDocente != docente.NroDocente.NroDeDocente)
                        return Unauthorized();
                }

                if (rol == "Estudiante" && !curso.Estudiantes.Any(a => a.Email == email))
                    return Unauthorized();

                ViewBag.RoomName = nombreCurso.Replace(" ", "_");
                ViewBag.NombreUsuario = HttpContext.Session.GetString("nombreLogueado") ?? email;
                ViewBag.EsDocente = rol == "Docente";

                return View();
            }
            catch (DocenteException ex)
            {
                return Unauthorized();
            }
            catch (EstudianteException ex)
            {
                return Unauthorized();
            }
        }
    }
}
