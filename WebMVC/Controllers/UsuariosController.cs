using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NextLevel.Compartidos.DTOs.Usuarios;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiante;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using Humanizer;

namespace WebMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILoginUsuario _loginUsuario;
        private readonly IRegistroEstudiante _registroEstudiante;

        public UsuariosController(ILoginUsuario loginUsuario, IRegistroEstudiante registroEstudiante)
        {
            _loginUsuario = loginUsuario;
            _registroEstudiante = registroEstudiante;
        }
        
        public IActionResult Login()
        {
            return View("Login-Registro");
        }

        [HttpPost]
        public IActionResult Login(UsuarioLoginDTO dto)
        {
            try
            {
                TempData["Modal"] = false;
                ViewBag.ErrorLoginBool = false;
                var usuario = _loginUsuario.Ejecutar(dto.Email, dto.Password);
                HttpContext.Session.SetString("rolLogueado", usuario.Rol.ToString());
                HttpContext.Session.SetString("emailLogueado", usuario.Email); //TODO hace falta
                if (usuario.Rol.ToString() == "Estudiante")
                {
                    return RedirectToAction("Index", "Home"); //TODO redireccionar a cursos estudiantes
                }else if(usuario.Rol == Rol.Docente)
                {
                    return RedirectToAction("Privacy", "Home");//TODO redireccionar a mis cursos del docente
                }
                else
                {
                    return RedirectToAction("Privacy", "Home");//TODO redireccionar a la pagina principal de administradores
                }
            }
            catch (UsuarioEstaVerificadoException ex)
            {
                TempData["Modal"] = true;
                ViewBag.EmailPendiente = dto.Email; 
                return View("Login-Registro");
            }
            catch (UsuarioException ex)
            {
                TempData["Modal"] = false;
                ViewBag.ErrorLoginBool = true;
                ViewBag.ErrorLogin = ex.Message;
                return View("Login-Registro");
            }
        }
        
        
        [HttpPost]
        public ActionResult Create(EstudianteRegistroDTO estudianteRegistroDTO)
        {
            try
            {
                _registroEstudiante.Ejecutar(estudianteRegistroDTO, Token.GenerarToken(estudianteRegistroDTO.Email));
                TempData["ModalRegistro"] = true;
                ViewBag.EmailPendiente = estudianteRegistroDTO.Email;
                ViewBag.ErrorRegistroBool = false;
                return View("Login-Registro");
            }
            catch (UsuarioEmailException ex)
            {
                TempData["ModalRegistro"] = false;
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View("Login-Registro");
            }
            catch (UsuarioPasswordException ex)
            {
                TempData["ModalRegistro"] = false;
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View("Login-Registro");
            }
            catch (UsuarioNombreCompletoException ex)
            {
                TempData["ModalRegistro"] = false;
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View("Login-Registro");
            }
            catch (UsuarioTelefonoException ex)
            {
                TempData["ModalRegistro"] = false;
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View("Login-Registro");
            }
            catch (EstudianteCedulaException ex)
            {
                TempData["ModalRegistro"] = false;
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View("Login-Registro");
            }
            catch (EstudianteException ex)
            {
                TempData["ModalRegistro"] = false;
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View("Login-Registro");
            }
            catch (Exception ex)
            {
                TempData["ModalRegistro"] = false;
                ViewBag.ErrorRegistroBool = true;
                ViewBag.ErrorRegistroMensaje = ex.Message;
                return View("Login-Registro");
            }
        }
        
        public ActionResult VerificarEmail(string token, string emailDestino)
        {
            try
            {
                _registroEstudiante.VerificarEmail(token);
                ViewBag.ErrorVerificarEmailMensaje = "Usuario verificado exitosamente.";
                return View();
            }
            catch (UsuarioTokenVencimientoException ex)
            {
                ViewBag.ErrorVerificarEmailMensaje = ex.Message;
                ViewBag.EmailDestino = emailDestino;
                return View("VerificarEmail");
            }
            catch (UsuarioException ex)
            {
                ViewBag.ErrorVerificarEmailMensaje = ex.Message;
                return View();
            }
        }

        public ActionResult CancelarVerificacion(string token)
        {
            try
            {
                _registroEstudiante.CancelarVerificacion(token); 
                ViewBag.ErrorCancelarVerificacionMensaje = "Usuario cancelado exitosamente.";
                return View("VerificarEmail");
            }
            catch (UsuarioException ex)
            {
                ViewBag.ErrorCancelarVerificacionMensaje = ex.Message;
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
                return View("Login-Registro");
            }
            catch (UsuarioException ex)
            {
                ViewBag.Error = true;
                ViewBag.Mensaje = ex.Message;
                return View("Login-Registro");
            }
        }
    }
}
