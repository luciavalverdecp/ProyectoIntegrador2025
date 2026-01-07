using Microsoft.AspNetCore.Mvc;
using NextLevel.LogicaAplicacion.InterfacesCU.Mensajes;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;

namespace WebMVC.Controllers
{
    public class MensajesController : Controller
    {
        private readonly IEnviarMensaje _enviarMensaje;
        public MensajesController(IEnviarMensaje enviarMensaje)
        {
            _enviarMensaje = enviarMensaje;
        }

        [HttpPost]
        public IActionResult EnviarMensaje(int idConversacion, string Contenido, string nombreCurso)
        {
            _enviarMensaje.Ejecutar(
                idConversacion,
                new UsuarioEmailDTO(HttpContext.Session.GetString("emailLogueado")),
                Contenido
            );

            return Redirect("/Cursos/VisualizarCurso?nombreCurso=" + nombreCurso + "#foro");
        }
    }
}
