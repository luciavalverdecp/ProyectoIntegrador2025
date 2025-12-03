using Microsoft.AspNetCore.Mvc;
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
        public DocentesController(IObtenerDocente obtenerDocente,
            IRecuperarCuenta recuperarCuenta)
        {
            this.obtenerDocente = obtenerDocente;
            this.recuperarCuenta = recuperarCuenta;
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
        public IActionResult RecuperarCuenta(string email, string returnTo)
        {
            try
            {
                recuperarCuenta.Ejecutar(email);

                TempData["MensajeRecuperarPassword"] = "Se le ha enviado un correo para continuar con la recuperación.";
                TempData["MensajeErrorRecuperarPassword"] = false;
            }
            catch (UsuarioException ex)
            {
                TempData["MensajeRecuperarPassword"] = ex.Message;
                TempData["MensajeErrorRecuperarPassword"] = true;
            }

            if (returnTo == "Perfil")
            {
                TempData["TabActivo"] = "contra";
                return RedirectToAction("Perfil", "Docentes");
            }

            return View("RecuperarContraseña");
        }
    }
}
