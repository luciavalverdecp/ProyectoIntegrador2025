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
using NextLevel.LogicaAplicacion.InterfacesCU.Mensajes;
using NextLevel.LogicaAplicacion.InterfacesCU.ParticipantesConversacion;
using NextLevel.Compartidos.DTOs.Conversaciones;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Cursos;

namespace WebMVC.Controllers
{
    public class CursosController : Controller
    {
        private readonly IObtenerCursosFiltrados _obtenerCursosFiltrados;
        private readonly IObtenerCursosDocente _obtenerCursosDocente;
        private readonly IObtenerCurso _obtenerCurso;
        private readonly IObtenerMisCursos _obtenerMisCursos;
        private readonly IAltaCurso _altaCurso;
        private readonly IAgregarClase _agregarClase;
        private readonly IObtenerDocente _obtenerDocente;
        private readonly IObtenerEstudiante _obtenerEstudiante;
        private readonly IObtenerMensajes _obtenerMensajes;
        private readonly IObtenerPartiConversaciones _obtpartiConversaciones;
        private readonly IAgregarCalificacion _agregarCalificacion;
        public CursosController(IObtenerCursosFiltrados obtenerCursosFiltrados,
            IObtenerCursosDocente obtenerCursosDocente,
             IObtenerCurso obtenerCurso,
             IObtenerMisCursos obtenerMisCursos,
             IAltaCurso altaCurso,
             IAgregarClase agregarClase,
             IObtenerDocente obtenerDocente,
             IObtenerEstudiante obtenerEstudiante, 
             IObtenerMensajes obtenerMensajes,
             IObtenerPartiConversaciones obtenerPartiConversaciones,
             IAgregarCalificacion agregarCalificacion)
        {
            _obtenerCursosFiltrados = obtenerCursosFiltrados;
            _obtenerCursosDocente = obtenerCursosDocente;
            _obtenerCurso = obtenerCurso;
            _obtenerMisCursos = obtenerMisCursos;
            _altaCurso = altaCurso;
            _agregarClase = agregarClase;
            _obtenerDocente = obtenerDocente;
            _obtenerEstudiante = obtenerEstudiante;
            _obtenerMensajes = obtenerMensajes;
            _obtpartiConversaciones = obtenerPartiConversaciones;
            _agregarCalificacion = agregarCalificacion;
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
                    if(!cursosDTO.Estudiantes.Any(e => e.Email == HttpContext.Session.GetString("emailLogueado"))
                        && HttpContext.Session.GetString("nroDocenteLogueado") == null)
                    {
                        return Redirect("/Usuarios/Login");
                    }
                    ViewBag.ClasesAgendadas = cursosDTO.FechasClases
                        .Select(c => new { Fecha = c.ToString("yyyy-MM-dd"), Hora = c.ToString("HH:mm") }).ToList();
                    ViewBag.MensajesForo = _obtenerMensajes.Ejecutar(cursosDTO.Foro.Conversacion);
                    var nroDocente = HttpContext.Session.GetString("nroDocenteLogueado");
                    if(nroDocente != null)
                    {
                        int.TryParse(nroDocente, out int numero);
                        ViewBag.MensajesDocente = _obtpartiConversaciones.Ejecutar(nombreCurso, nroDocente);
                    }
                    else
                    {
                        var partiConversacion = _obtpartiConversaciones.EjecutarEstudiante(nombreCurso, HttpContext.Session.GetString("emailLogueado"));
                        ConversacionDTO conversacion = null;
                        if(partiConversacion != null)
                        {
                            conversacion = partiConversacion.Conversacion;
                            ViewBag.ConversacionEstudiante = conversacion.Id;
                        }
                        else
                        {
                            ViewBag.ConversacionEstudiante = null;
                        }
                    }
                    return View(cursosDTO);
                }
                catch (CursoNombreException ex)
                {
                    ViewBag.Error = ex.Message;
                }
                catch (CursoNoEncontradoException ex)
                {
                    ViewBag.Error = ex.Message;
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
            return Redirect("/Usuarios/Login");
        }


        public IActionResult ListadoCursosEstudiante()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Estudiante")
            {
                var misCursos = _obtenerMisCursos.Ejecutar(HttpContext.Session.GetString("emailLogueado"));
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
                catch (CursoExistenteException ex)
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
            }
            catch (CursoNombreException ex)
            {
                ViewBag.Error = ex.Message;
            }
            catch (CursoNoEncontradoException ex)
            {
                ViewBag.Error = ex.Message;
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
                catch (CursoNoEncontradoException ex)
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
            catch (CursoNombreException ex)
            {
                return Unauthorized();
            }
            catch (CursoNoEncontradoException ex)
            {
                return Unauthorized();
            }
            catch (CursoException ex)
            {
                return Unauthorized();
            }
            catch (DocenteNoEncontradoException ex)
            {
                return Unauthorized();
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

        [HttpPost]
        public IActionResult Calificar(string nombreCurso, double puntaje)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Estudiante")
            {
                try
                {
                    var cursosDTO = _obtenerCurso.Ejecturar(nombreCurso);
                    _agregarCalificacion.Ejecutar(cursosDTO, puntaje);
                    TempData["MensajeCalificar"] = "Se califico correctamente el curso";
                    TempData["ErrorCalificar"] = false;
                    return RedirectToAction("Perfil", "Estudiantes", new { tabActivo = "cursos" });
                }
                catch (CursoNombreException ex)
                {
                    TempData["MensajeCalificar"] = ex.Message;
                    TempData["ErrorCalificar"] = true;
                    return RedirectToAction("Perfil", "Estudiantes", new { tabActivo = "cursos" });
                }
                catch (CursoNoEncontradoException ex)
                {
                    TempData["MensajeCalificar"] = ex.Message;
                    TempData["ErrorCalificar"] = true;
                    return RedirectToAction("Perfil", "Estudiantes", new { tabActivo = "cursos" });
                }
                catch (CursoException ex)
                {
                    TempData["MensajeCalificar"] = ex.Message;
                    TempData["ErrorCalificar"] = true;
                    return RedirectToAction("Perfil", "Estudiantes", new { tabActivo = "cursos" });
                }
            }
            return RedirectToAction("Login", "Usuarios");
        }
    }
}
