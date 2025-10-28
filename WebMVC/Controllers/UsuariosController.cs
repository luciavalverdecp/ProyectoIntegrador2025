using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NextLevel.Compartidos.DTOs.Usuarios;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;

namespace WebMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILoginUsuario _loginUsuario;

        public UsuariosController(ILoginUsuario loginUsuario)
        {
            _loginUsuario = loginUsuario;
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
    }
}
