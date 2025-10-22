using Microsoft.AspNetCore.Http;
using NextLevel.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class CambioRol : IValidable
    {
        public Estudiante Estudiante {  get; set; }
        public List<IFormFile> Archivos { get; set; }//???
        public CambioRol() { }
        public CambioRol(Estudiante estudiante, List<IFormFile> archivos)
        {
            Estudiante = estudiante;
            Archivos = archivos;
        }


        #region Validaciones
        public void Validar()
        {
            validarArchivos();
        }

        private void validarArchivos()
        {
            if (Archivos.Count == 0) throw new Exception("Se debe enviar al menos un archivo.");
        }
        #endregion
    }
}
