using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Materiales;
using NextLevel.Compartidos.DTOs.Semanas;
using NextLevel.LogicaAplicacion.InterfacesCU.Materiales;

namespace WebMVC.Controllers
{
    public class MaterialesController : Controller
    {
        private readonly IAgregarMaterial agregarMaterial;
        public MaterialesController(IAgregarMaterial agregarMaterial)
        {
            this.agregarMaterial = agregarMaterial;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarMaterial(string CursoNombre, int SemanaNumero, MaterialDTO material)
        {
            if(HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                try
                {
                    await agregarMaterial.Ejecutar(material, SemanaNumero, CursoNombre);
                    return RedirectToAction("VisualizarCurso", "Cursos", new { nombreCurso = CursoNombre });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("VisualizarCurso");
                }
            }
            return Redirect("/Usuarios/Login");
        }
    }
}
