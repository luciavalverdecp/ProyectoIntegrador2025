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
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsuarioLoginDTO dto)
        {
            try
            {
                ViewBag.ErrorLoginBool = false;
                var usuario = _loginUsuario.Ejecutar(dto.email, dto.password);
                HttpContext.Session.SetString("rolLogueado", usuario.Rol.ToString());
                HttpContext.Session.SetString("emailLogueado", usuario.Email);
                if (usuario is Estudiante)
                {
                    return RedirectToAction("Index", "Home"); //TODO
                }else
                {
                    return RedirectToAction("Privacy", "Home");//TODO
                }
            }
            catch (UsuarioException ex)
            {
                ViewBag.ErrorLoginBool = true;
                ViewBag.ErrorLogin = ex.Message;
                return View();
            }
        }
    }
}
