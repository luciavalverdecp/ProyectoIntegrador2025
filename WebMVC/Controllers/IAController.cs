using Microsoft.AspNetCore.Mvc;
using NextLevel.LogicaAplicacion.InterfacesCU.IA;
using NextLevel.Compartidos.DTOs.IA;

public class IAController : Controller
{
    private readonly IConsultarConIA _consultarConIA;

    public IAController(IConsultarConIA consultarConIA)
    {
        _consultarConIA = consultarConIA;
    }

    [HttpPost]
    public async Task<IActionResult> Consultar([FromBody] ConsultaIADTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Mensaje))
            return BadRequest("Mensaje vacío");

        var respuesta = await _consultarConIA.Ejecutar(dto.Mensaje);

        return Json(new { respuesta });
    }
}