using Microsoft.AspNetCore.Mvc;
using NextLevel.Compartidos.DTOs.Materiales;
using NextLevel.Compartidos.DTOs.Semanas;
using NextLevel.LogicaAplicacion.InterfacesCU.Materiales;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Material;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Semana;

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
                catch (CursoNoEncontradoException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("VisualizarCurso");
                }
                catch (SemanaException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("VisualizarCurso");
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
                catch (CursoNoEncontradoException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("VisualizarCurso");
                }
                catch (MaterialNoEncontradoException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
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
