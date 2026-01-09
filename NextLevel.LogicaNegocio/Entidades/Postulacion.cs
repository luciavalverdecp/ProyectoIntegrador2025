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
        public CambioRol CambioRol { get; set; }
        [AllowNull]
        public AltaCurso AltaCurso { get; set; }

        public Postulacion() { }
        public Postulacion(int id, Administrador administrador, AltaCurso altaCurso)
        {
            Id = id;
            Administrador = administrador;
            AltaCurso = altaCurso;
        }
        public Postulacion(int id, Administrador administrador, CambioRol cambioRol)
        {
            Id = id;
            Administrador = administrador;
            CambioRol = cambioRol;
        }
    }
}
