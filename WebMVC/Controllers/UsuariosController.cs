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
                _registroEstudiante.Ejecutar(estudianteRegistroDTO);
                ViewBag.Error = false;
                return View();
            }
            catch (UsuarioEmailException ex)
            {
                ViewBag.Error = true;
                ViewBag.RegistroError = ex.Message;
                return View();
            }
            catch (UsuarioPasswordException ex)
            {
                ViewBag.Error = true;
                ViewBag.RegistroError = ex.Message;
                return View();
            }
            catch (UsuarioNombreCompletoException ex)
            {
                ViewBag.Error = true;
                ViewBag.RegistroError = ex.Message;
                return View();
            }
            catch (UsuarioTelefonoException ex)
            {
                ViewBag.Error = true;
                ViewBag.RegistroError = ex.Message;
                return View();
            }
            catch (EstudianteCedulaException ex)
            {
                ViewBag.Error = true;
                ViewBag.RegistroError = ex.Message;
                return View();
            }
            catch (EstudianteException ex)
            {
                ViewBag.Error = true;
                ViewBag.RegistroError = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = true;
                ViewBag.RegistroError = ex.Message;
                return View();
            }
        }
    }
}
