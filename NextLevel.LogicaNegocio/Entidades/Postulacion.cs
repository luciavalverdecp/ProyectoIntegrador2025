using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Postulacion
    {
        public int Id { get; set; }
        public int AdministradorId { get; set; }
        public Administrador Administrador { get; set; }
        [AllowNull]
        public CambioRol? CambioRol { get; set; }
        [AllowNull]
        public AltaCurso? AltaCurso { get; set; }
        public string Estado { get; set; }

        public Postulacion() { }
        public Postulacion(Administrador administrador, AltaCurso altaCurso)
        {
            Administrador = administrador;
            AltaCurso = altaCurso;
            Estado = "Pendiente";
        }
        public Postulacion(Administrador administrador, CambioRol cambioRol)
        {
            Administrador = administrador;
            CambioRol = cambioRol;
            Estado = "Pendiente";
        }
    }
}
