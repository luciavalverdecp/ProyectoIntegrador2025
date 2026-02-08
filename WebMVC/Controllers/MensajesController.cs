using System.Net;
using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Mensajes;
using NextLevel.Compartidos.DTOs.ParticipantesConversacion;
using NextLevel.LogicaAplicacion.InterfacesCU.Conversaciones;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Mensajes;
using NextLevel.LogicaAplicacion.InterfacesCU.ParticipantesConversacion;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.Entidades;

namespace WebMVC.Controllers
{
    public class MensajesController : Controller
    {
        private readonly IEnviarMensaje _enviarMensaje;
        private readonly IObtenerCurso _obtenerCurso;
        private readonly IObtenerMensajes _obtenerMensajes;
        private readonly IObtenerConversacion _obtenerConversacion;
        private readonly IObtenerPartiConversaciones _obtpartiConversaciones;
        public MensajesController(IEnviarMensaje enviarMensaje, 
            IObtenerCurso obtenerCurso, 
            IObtenerMensajes obtenerMensajes, 
            IObtenerConversacion obtenerConversacion,
            IObtenerPartiConversaciones obtpartiConversaciones)
        {
            _enviarMensaje = enviarMensaje;
            _obtenerCurso = obtenerCurso;
            _obtenerMensajes = obtenerMensajes;
            _obtenerConversacion = obtenerConversacion;
            _obtpartiConversaciones = obtpartiConversaciones;
        }

            [HttpPost]
            public IActionResult EnviarMensaje(int conversacionId, string Contenido, string nombreCurso)
            {
                var nombreCursoLimpio = WebUtility.HtmlDecode(nombreCurso);
                var cursoDTO = _obtenerCurso.Ejecturar(nombreCursoLimpio.ToString());
                var usuario = new UsuarioEmailDTO(HttpContext.Session.GetString("emailLogueado"));
                int idNuevaConversacion = _enviarMensaje.Ejecutar(
                    conversacionId,
                    usuario,
                    Contenido,
                    cursoDTO
                );
                var conversacion = _obtenerConversacion.Ejecutar(idNuevaConversacion);
                TempData["ConversacionActiva"] = idNuevaConversacion;
                TempData["TabActivo"] = "contacto";

                var nombreCursoEncoded = Uri.EscapeDataString(nombreCursoLimpio);
                if (conversacion.TipoConversacion == TipoConversacion.Foro)
                        return Redirect("/Cursos/VisualizarCurso?nombreCurso=" + nombreCursoEncoded + "#foro");
                    return Redirect("/Cursos/VisualizarCurso?nombreCurso=" + nombreCursoEncoded + "#contacto");
            }

        [HttpGet]
        public IActionResult ObtenerMensajesConversacion(string nombreCurso, int conversacionId)
        {
            if (conversacionId == 0)
            {
                ViewBag.CoversacionId = -1;
                ViewBag.NombreCurso = nombreCurso;
                return PartialView("~/Views/Cursos/_ChatConversacion.cshtml", new List<MensajeDTO>());
            }
            var conversacion = _obtenerConversacion.Ejecutar(conversacionId);
            var mensajes = _obtenerMensajes.Ejecutar(conversacion);
            ViewBag.CoversacionId = conversacion.Id;
            ViewBag.NombreCurso = conversacion.CursoNombre.Nombre;
            return PartialView("~/Views/Cursos/_ChatConversacion.cshtml", mensajes);
        }
    }
}
