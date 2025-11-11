using Microsoft.AspNetCore.Http;
using NextLevel.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class CambioRol : IValidable
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public Estudiante Estudiante {  get; set; }
        [NotMapped]
        public List<IFormFile> Archivos { get; set; }
        public List<string> NombresArchivos { get; set; } = new List<string>();

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
