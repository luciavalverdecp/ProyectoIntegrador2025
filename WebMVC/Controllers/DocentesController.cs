using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Docentes;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Estudiantes;
using NextLevel.LogicaAplicacion.InterfacesCU.CambiosDeRol;
using NextLevel.LogicaAplicacion.InterfacesCU.Docentes;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Docente;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;

namespace WebMVC.Controllers
{
    public class DocentesController : Controller
    {
        private readonly IObtenerDocente obtenerDocente;
        private readonly IRecuperarCuenta recuperarCuenta;
        private readonly IModificarDocente modificarDocente;
        public DocentesController(IObtenerDocente obtenerDocente,
            IRecuperarCuenta recuperarCuenta,
            IModificarDocente modificarDocente)
        {
            this.obtenerDocente = obtenerDocente;
            this.recuperarCuenta = recuperarCuenta;
            this.modificarDocente = modificarDocente;
        }

        public IActionResult Perfil()
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                try
                {
                    var docente = obtenerDocente.Ejecutar(HttpContext.Session.GetString("emailLogueado"));

                    return View(docente);
                }
                catch (DocenteException ex)
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
        public IActionResult ActualizarDatos(DocenteInfoDTO docenteInfoDTO)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                try
                {
                    modificarDocente.Ejecutar(docenteInfoDTO);
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
                catch (DocenteException ex)
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
