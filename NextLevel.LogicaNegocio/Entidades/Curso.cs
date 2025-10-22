using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.InterfacesEntidades;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Curso : IEntity, IComparable<Curso>
    {
        public int Id {  get; set; }
        public string Nombre { get; set; }
        public Docente Docente {  get; set; }
        public List<Estudiante> Estudiantes { get; set; }
        public List<Semana> Semanas { get; set; }
        public Foro Foro { get; set; }
        public DateTime FechaInicio { get; set; }
        [AllowNull]
        public DateTime FechaFin { get; set; }

        public Curso() { }
        public Curso(Docente docente)
        {
            this.Docente = docente;
            Estudiantes = new List<Estudiante>();
            Semanas = new List<Semana>();
            Foro = new Foro();
            FechaInicio = DateTime.Now;
        }

        #region Equals - CompareTo
        public override bool Equals(object? obj)
        {
            return obj is Curso c && c.Id == this.Id;
        }
        public int CompareTo(Curso? other)
        {
            return this.Nombre.CompareTo(other.Nombre);
        }

        #endregion

        #region Metodos
        public void ActualizarSemanas()
        {
            if (FechaInicio > DateTime.Now)
                return;

            int semanasTranscurridas = (int)((DateTime.Now - FechaInicio).TotalDays / 7) + 1;

            while (Semanas.Count < semanasTranscurridas)
            {
                int numeroNueva = Semanas.Count + 1;
                DateTime fechaInicioNueva = FechaInicio.AddDays((numeroNueva - 1) * 7);

                Semanas.Add(new Semana
                {
                    Numero = numeroNueva,
                    FechaInicio = fechaInicioNueva,
                    Curso = this
                });
            }
        }
        #endregion


    }
}
