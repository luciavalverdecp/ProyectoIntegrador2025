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
            bool rolYemail = HttpContext.Session.GetString("emailLogueado") != null && HttpContext.Session.GetString("rolLogueado") != null;
            var rol = HttpContext.Session.GetString("rolLogueado");

            if (rol == "Estudiante")
            {
                return RedirectToAction("ListadoCursos", "Cursos");
            }
            else if (rol == "Docente")
            {
                return RedirectToAction("ListadoCursosDocente", "Cursos");
            }
            else if(rol == "Administrador")
            {
                return RedirectToAction("PostulacionesAdministrador", "Postulaciones");
            }
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
                HttpContext.Session.SetString("emailLogueado", usuario.Email); 
                if (usuario.Rol == Rol.Estudiante)
                {
                    return RedirectToAction("ListadoCursos", "Cursos");
                }
                else if(usuario.Rol == Rol.Docente)
                {
                    HttpContext.Session.SetString("nroDocenteLogueado", dto.Email); //Email en este caso es el nroDocente
                    return RedirectToAction("ListadoCursosDocente", "Cursos");
                }
                else 
                {
                    return RedirectToAction("PostulacionesAdministrador", "Postulaciones");//TODO redireccionar a la pagina principal de administradores
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
        public IActionResult Create(EstudianteRegistroDTO estudianteRegistroDTO)
        {
            try
            {
                _registroEstudiante.Ejecutar(estudianteRegistroDTO, Token.GenerarToken(estudianteRegistroDTO.Email));
                TempData["MensajeRegistro"] = "Usuario creado exitosamente";
                ViewBag.EmailPendiente = estudianteRegistroDTO.Email;
                TempData["ModalRegistro"] = true;
                TempData["ErrorRegistro"] = false;
                return View("Login-Registro");
            }
            catch (UsuarioEmailException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                TempData["ModalRegistro"] = false;
                return View("Login-Registro");
            }
            catch (UsuarioPasswordException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                TempData["ModalRegistro"] = false;
                return View("Login-Registro");
            }
            catch (UsuarioNombreCompletoException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                TempData["ModalRegistro"] = false;
                return View("Login-Registro");
            }
            catch (UsuarioTelefonoException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                TempData["ModalRegistro"] = false;
                return View("Login-Registro");
            }
            catch (EstudianteCedulaException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                TempData["ModalRegistro"] = false;
                return View("Login-Registro");
            }
            catch (EstudianteException ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                TempData["ModalRegistro"] = false;
                return View("Login-Registro");
            }
            catch (Exception ex)
            {
                TempData["MensajeRegistro"] = ex.Message;
                TempData["ErrorRegistro"] = true;
                TempData["ModalRegistro"] = false;
                return View("Login-Registro");
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
        public IActionResult RecuperarCuenta(string email, string returnTo)
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

            if (returnTo == "Perfil")
            {
                if(HttpContext.Session.GetString("rolLogueado") == "Estudiante")
                {
                    TempData["TabActivo"] = "contra";
                    return RedirectToAction("Perfil", "Estudiantes");
                }
                else if(HttpContext.Session.GetString("rolLogueado") == "Docenta")
                {
                    TempData["TabActivo"] = "contra";
                    return RedirectToAction("Perfil", "Docentes");
                }
            }

            return View();
        }


        public IActionResult ReiniciarContrasena(string email)
        {
            try
            {
                if (TempData["ErrorReset"] == null || (bool)TempData["ErrorReset"]) TempData["ErrorReset"] = true;
                if((bool)TempData["ErrorReset"]) _recuperarCuenta.ValidarVencimientoLink(email);

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
            return ReiniciarContrasena(email);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("rolLogueado");
            HttpContext.Session.Remove("emailLogueado");
            return RedirectToAction("Login");
        }
    }
}
