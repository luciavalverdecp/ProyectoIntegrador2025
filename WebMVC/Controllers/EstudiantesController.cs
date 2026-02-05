using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.CambioRoles;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.LogicaAplicacion.InterfacesCU.CambiosDeRol;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.AltaCurso;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.CambioRol;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;

namespace WebMVC.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly IObtenerEstudiante obtenerEstudiante;
        private readonly ICursosTerminados cursosTerminados;
        private readonly ICambioDeRol cambioDeRol;
        private readonly IModificarEstudiante modificarEstudiante;
        private readonly IObtenerCurso obtenerCurso;

        public EstudiantesController(IObtenerEstudiante obtenerEstudiante,
            ICursosTerminados terminoCurso,
            ICambioDeRol cambioDeRol,
            IModificarEstudiante modificarEstudiante,
            IObtenerCurso obtenerCurso
            )
        {
            this.obtenerEstudiante = obtenerEstudiante;
            this.cursosTerminados = terminoCurso;
            this.cambioDeRol = cambioDeRol;
            this.modificarEstudiante = modificarEstudiante;
            this.obtenerCurso = obtenerCurso;
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
                catch (EstudianteNoEncontradoException ex)
                {
                    ViewBag.Error = ex.Message;
                }
                catch (EstudianteException ex)
                {
                    ViewBag.Error = ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View(null);
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
                    TempData["TabActivo"] = "cambioRol";
                    TempData["MensajeCambioRol"] = "Solicitud de cambio de rol enviada exitosamenete.";
                    TempData["ErrorCambioRol"] = false;
                    return RedirectToAction("Perfil", "Estudiantes");
                }
                catch (CambioRolExistenteException ex)
                {
                    TempData["TabActivo"] = "cambioRol";
                    TempData["MensajeCambioRol"] = ex.Message;
                    TempData["ErrorCambioRol"] = true;
                    return RedirectToAction("Perfil", "Estudiantes");
                }
                catch (CambioRolDocenteExistenteException ex)
                {
                    TempData["TabActivo"] = "cambioRol";
                    TempData["MensajeCambioRol"] = ex.Message;
                    TempData["ErrorCambioRol"] = true;
                    return RedirectToAction("Perfil", "Estudiantes");
                }
                catch (AltaCursoArchivosException ex)
                {
                    TempData["TabActivo"] = "cambioRol";
                    TempData["MensajeCambioRol"] = ex.Message;
                    TempData["ErrorCambioRol"] = true;
                    return RedirectToAction("Perfil", "Estudiantes");
                }
                catch (CambioRolException ex)
                {
                    TempData["TabActivo"] = "cambioRol";
                    TempData["MensajeCambioRol"] = ex.Message;
                    TempData["ErrorCambioRol"] = true;
                    return RedirectToAction("Perfil", "Estudiantes");
                }
            }
            return RedirectToAction("Login", "Usuarios");
        }

        [HttpPost]
        public IActionResult ActualizarDatos(EstudianteInfoDTO estudianteInfoDTO)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Estudiante")
            {
                try
                {
                    modificarEstudiante.Ejecutar(estudianteInfoDTO);
                    TempData["MensajeDatosPersonales"] = "Datos personales actualizados correctamente";
                    TempData["ErrorDatosPersonales"] = false;
                    return RedirectToAction("Perfil");
                }
                catch (UsuarioNombreCompletoException ex)
                {
                    TempData["MensajeDatosPersonales"] = ex.Message;
                    TempData["ErrorDatosPersonales"] = true;
                    return RedirectToAction("Perfil");
                }
                catch (UsuarioTelefonoException ex)
                {
                    TempData["MensajeDatosPersonales"] = ex.Message;
                    TempData["ErrorDatosPersonales"] = true;
                    return RedirectToAction("Perfil");
                }
                catch (EstudianteException ex)
                {
                    TempData["MensajeDatosPersonales"] = ex.Message;
                    TempData["ErrorDatosPersonales"] = true;
                    return RedirectToAction("Perfil");
                }
                catch (Exception ex)
                {
                    TempData["MensajeDatosPersonales"] = ex.Message;
                    TempData["ErrorDatosPersonales"] = true;
                    return RedirectToAction("Perfil");
                }
            }
            return RedirectToAction("Login", "Usuarios");
        }
    }
}
