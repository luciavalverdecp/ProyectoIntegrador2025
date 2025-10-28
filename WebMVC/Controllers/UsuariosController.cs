using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiante;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;

namespace WebMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IRegistroEstudiante _registroEstudiante;
        public UsuariosController(IRegistroEstudiante registroEstudiante)
        {
            _registroEstudiante = registroEstudiante;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EstudianteRegistroDTO estudianteRegistroDTO)
        {
            try
            {
                _registroEstudiante.Ejecutar(estudianteRegistroDTO, Token.GenerarToken(estudianteRegistroDTO.Email));
                ViewBag.ErrorRegistroBool = false;
                return View();
            }
            catch (UsuarioEmailException ex)
            {
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View();
            }
            catch (UsuarioPasswordException ex)
            {
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View();
            }
            catch (UsuarioNombreCompletoException ex)
            {
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View();
            }
            catch (UsuarioTelefonoException ex)
            {
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View();
            }
            catch (EstudianteCedulaException ex)
            {
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View();
            }
            catch (EstudianteException ex)
            {
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View();
            }
        }
        public ActionResult VerificarEmail(string token, string emailDestino)
        {
            try
            {
                _registroEstudiante.VerificarEmail(token);
                ViewBag.ErrorVerificarEmailBool = false;
                ViewBag.MensajeVerificarEmail = "Usuario verificado exitosamente.";
                return View();
            }
            catch (UsuarioTokenVencimientoException ex)
            {
                ViewBag.ErrorVerificarEmailBool = true;
                ViewBag.MensajeVerificarEmail = ex.Message;
                ViewBag.EmailDestino = emailDestino;
                return View("VerificarEmail");
            }
            catch (UsuarioException ex)
            {
                ViewBag.ErrorVerificarEmailBool = true;
                ViewBag.MensajeVerificarEmail = ex.Message;
                return View();
            }
        }

        public ActionResult CancelarVerificacion(string token)
        {
            try
            {
                _registroEstudiante.CancelarVerificacion(token); 
                ViewBag.Error = false;
                ViewBag.Mensaje = "Usuario cancelado exitosamente.";
                return View("VerificarEmail");
            }
            catch (UsuarioException ex)
            {
                ViewBag.Error = true;
                ViewBag.Mensaje = ex.Message;
                return View("VerificarEmail");
            }
        }

        public ActionResult ReenviarVerificacion(string emailDestino)
        {
            try
            {
                _registroEstudiante.ActualizarVerificacion(emailDestino, Token.GenerarToken(emailDestino));
                ViewBag.Error = false;
                ViewBag.Mensaje = "Se ha reenviado un nuevo enlace de verificacion.";
                return View("VerificarEmail");
            }
            catch (UsuarioException ex)
            {
                ViewBag.Error = true;
                ViewBag.Mensaje = ex.Message;
                return View("VerificarEmail");
            }
        }
    }
}
