using Microsoft.AspNetCore.Http;
using NextLevel.Compartidos.DTOs.CambioRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.InterfacesCU.CambiosDeRol
{
    public interface ICambioDeRol
    {
        Task Ejecutar(CambioRolDTO cambioRolDTO, List<IFormFile> archivos);
    }
}
