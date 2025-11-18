using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Calificacion
    {
        public int Id {  get; set; }
        public double Puntaje { get; set; }
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }
        public Prueba Prueba { get; set; }

        public Calificacion() { }
        public Calificacion(double puntaje, Estudiante estudiante, Prueba prueba)
        {
            Puntaje = puntaje;
            Estudiante = estudiante;
            Prueba = prueba;
        }

    }
}
