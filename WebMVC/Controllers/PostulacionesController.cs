using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Postulaciones;
using NextLevel.LogicaAplicacion.InterfacesCU.Postulaciones;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Postulacion;

namespace WebMVC.Controllers
{
    public class PostulacionesController : Controller
    {
        private readonly IObtenerPostulacionesAdmin _postulacionesAdmin;
        private readonly IObtenerPostulacion _obtenerPostulacion;
        private readonly IResolverPostulacion _resolverPostulacion;
        public PostulacionesController(IObtenerPostulacionesAdmin postulacionesAdmin,
            IObtenerPostulacion obtenerPostulacion,
            IResolverPostulacion resolverPostulacion) 
        { 
            _postulacionesAdmin = postulacionesAdmin;
            _obtenerPostulacion = obtenerPostulacion;
            _resolverPostulacion = resolverPostulacion;
        }

        public IActionResult PostulacionesAdministrador()
        {
            if (HttpContext.Session.GetString("rolLogueado") != "Administrador") return Redirect("/Usuarios/Login");
            var postulaciones = _postulacionesAdmin.Ejecutar(new UsuarioEmailDTO(HttpContext.Session.GetString("emailLogueado")));
            return View(postulaciones);
        }

        [HttpGet]
        public IActionResult Detalles(int id)
        {
            if(HttpContext.Session.GetString("rolLogueado") != "Administrador") return Redirect("/Usuarios/Login");
            try
            {
                var postulacionDTO = _obtenerPostulacion.Ejecutar(id);
                ViewBag.Postulacion = postulacionDTO;
            }
            catch (PostulacionNoEncontradaException ex)
            {
                ViewBag.Error = ex.Message;
            }
            catch (PostulacionException ex)
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
        public IActionResult ResolverPostulacion(int id, string accion, string motivo)
        {
            if (HttpContext.Session.GetString("rolLogueado") != "Administrador")
                return Redirect("/Usuarios/Login");

            if (string.IsNullOrWhiteSpace(motivo))
            {
                TempData["Error"] = "Debe ingresar un motivo.";
                return RedirectToAction("Detalles", new { id });
            }
             _resolverPostulacion.Ejecutar(id, motivo, accion);

            return RedirectToAction("PostulacionesAdministrador");
        }
    }
}
