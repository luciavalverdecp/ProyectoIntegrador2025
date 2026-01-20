using Microsoft.AspNetCore.Mvc;
using NextLevel.LogicaAplicacion.InterfacesCU.Postulaciones;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;

namespace WebMVC.Controllers
{
    public class PostulacionesController : Controller
    {
        private IObtenerPostulacionesAdmin _postulacionesAdmin;
        public PostulacionesController(IObtenerPostulacionesAdmin postulacionesAdmin) 
        { 
            _postulacionesAdmin = postulacionesAdmin;
        }

        public IActionResult PostulacionesAdministrador()
        {
            var postulaciones = _postulacionesAdmin.Ejecutar(new UsuarioEmailDTO(HttpContext.Session.GetString("emailLogueado")));
            return View(postulaciones);
        }
    }
}
