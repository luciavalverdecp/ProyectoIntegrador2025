using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NextLevel.Compartidos.DTOs.Usuarios;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.Compartidos.DTOs.Estudiantes;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiante;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;

namespace WebMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILoginUsuario _loginUsuario;
        private readonly IRegistroEstudiante _registroEstudiante;
        private readonly IRecuperarCuenta _recuperarCuenta;

        public UsuariosController(ILoginUsuario loginUsuario, 
            IRegistroEstudiante registroEstudiante,
            IRecuperarCuenta recuperarCuenta)
        {
            _loginUsuario = loginUsuario;
            _registroEstudiante = registroEstudiante;
            _recuperarCuenta = recuperarCuenta;
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
            catch (UsuarioException ex)
            {
                ViewBag.ErrorLoginBool = true;
                ViewBag.ErrorLogin = ex.Message;
                return View("Login-Registro");
            }
        }
        
        
        [HttpPost]
        public IActionResult Create(EstudianteRegistroDTO estudianteRegistroDTO)
        {
            try
            {
                _registroEstudiante.Ejecutar(estudianteRegistroDTO, Token.GenerarToken(estudianteRegistroDTO.Email));
                TempData["MensajeRegistro"] = "Usuario creado exitosamente";
                TempData["ErrorRegistro"] = false;
                return RedirectToAction("Login");
            }
            catch (UsuarioEmailException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                return RedirectToAction("Login");
            }
            catch (UsuarioPasswordException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                return RedirectToAction("Login");
            }
            catch (UsuarioNombreCompletoException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                return RedirectToAction("Login");
            }
            catch (UsuarioTelefonoException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                return RedirectToAction("Login");
            }
            catch (EstudianteCedulaException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                return RedirectToAction("Login");
            }
            catch (EstudianteException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                return RedirectToAction("Login");
            }
        }
        
        public IActionResult VerificarEmail(string token, string emailDestino)
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

        public IActionResult CancelarVerificacion(string token)
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

        public IActionResult ReenviarVerificacion(string emailDestino)
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

        public IActionResult RecuperarCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarCuenta(string email)
        {
            try
            {
                _recuperarCuenta.Ejecutar(email);
                ViewBag.MensajeExito = "Se le ha enviado un correo para continuar con la recuperación.";
            }
            catch (UsuarioException ex)
            {
                ViewBag.MensajeError = ex.Message;
            }
            return View();
        }

        public IActionResult ReiniciarContrasena(string email)
        {
            try
            {
                _recuperarCuenta.ValidarVencimientoLink(email);

                if (TempData["MensajeCambio"] != null)
                {
                    ViewBag.MensajeCambio = TempData["MensajeCambio"];
                    ViewBag.ErrorReset = TempData["ErrorReset"];
                }

                return View(model: email);
            }
            catch (UsuarioException)
            {
                return View("EnlaceExpirado");
            }
        }

        [HttpPost]
        public IActionResult ReiniciarContrasena(string email, string Password, string ConfirmarPassword)
        {
            try
            {
                if (Password != ConfirmarPassword) throw new UsuarioException("Las contraseñas no coinciden, intente nuevamente");
                _recuperarCuenta.CambiarContrasena(email, Password);
                TempData["MensajeCambio"] = "Se realizo el cambio de contraseña exitosamente";
                TempData["ErrorReset"] = false;
            }
            catch (UsuarioException ex)
            {
                TempData["MensajeCambio"] = ex.Message;
                TempData["ErrorReset"] = true;
            }
            return RedirectToAction("ReiniciarContrasena", new { email = email });
        }
    }
}
