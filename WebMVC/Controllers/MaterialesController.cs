using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Materiales;
using NextLevel.Compartidos.DTOs.Semanas;
using NextLevel.LogicaAplicacion.InterfacesCU.Materiales;

namespace WebMVC.Controllers
{
    public class MaterialesController : Controller
    {
        private readonly ICRUDMaterial crudMaterial;
        public MaterialesController(ICRUDMaterial crudMaterial)
        {
            this.crudMaterial = crudMaterial;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarMaterial(string CursoNombre, int SemanaNumero, MaterialDTO material)
        {
            if(HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                try
                {
                    await crudMaterial.Agregar(material, SemanaNumero, CursoNombre);
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

        [HttpPost]
        public async Task<IActionResult> EliminarMaterial(string CursoNombre, int SemanaNumero, MaterialBasicoDTO material)
        {
            if (HttpContext.Session.GetString("rolLogueado") == "Docente")
            {
                try
                {
                    await crudMaterial.Eliminar(material, SemanaNumero, CursoNombre);
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
