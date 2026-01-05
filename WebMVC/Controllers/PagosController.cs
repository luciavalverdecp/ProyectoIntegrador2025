using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Pagos;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using NextLevel.LogicaAplicacion.InterfacesCU.Pagos;
using NextLevel.LogicaNegocio.Entidades;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class PagosController : Controller
    {
        private readonly IRealizarPago realizarPago;
        private readonly IAgregarCurso agregarCurso;
        public PagosController(IRealizarPago realizarPago, IAgregarCurso agregarCurso)
        {
            this.realizarPago = realizarPago;
            this.agregarCurso = agregarCurso;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(CrearPagoViewModel vm)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Estudiante")
            {
                try
                {
                    var email = HttpContext.Session.GetString("emailLogueado");

                    var dto = new CrearPagoDTO(
                        email,
                        vm.CursoNombre,
                        MetodoPago.Tarjeta,
                        new DatosTarjetaDTO(
                            vm.Tarjeta.NumeroTarjeta,
                            vm.Tarjeta.NombreTitular,
                            vm.Tarjeta.MesVencimiento,
                            vm.Tarjeta.AnioVencimiento,
                            vm.Tarjeta.Cvv
                        )
                    );
                    agregarCurso.Ejecutar(email, vm.CursoNombre);

                    var pagoId = realizarPago.ProcesarPagoSandbox(dto);

                    return Redirect("/Cursos/ListadoCursosEstudiante");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return RedirectToAction("DetallesDeUnCurso", "Cursos", new { nombre = vm.CursoNombre });
                }
            }
            else if (HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                return Redirect("/Cursos/ListadoCursosDocente");
            }
            return Redirect("/Usuarios/Login");
        }
    }
}
