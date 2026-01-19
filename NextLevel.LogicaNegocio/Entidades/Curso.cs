using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NextLevel.LogicaNegocio.InterfacesEntidades;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Curso : IEntity, IComparable<Curso>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int DocenteId { get; set; }
        public Docente Docente { get; set; }
        public List<Estudiante> Estudiantes { get; set; }
        public List<Semana> Semanas { get; set; }
        public Foro? Foro { get; set; }
        [NotMapped]
        public IFormFile Archivo { get; set; }//TODO Creo que esto no hace falta
        public string Imagen { get; set; }
        public DateTime FechaInicio { get; set; }
        [AllowNull]
        public DateTime FechaFin { get; set; }
        public double Calificacion { get; set; }
        public List<double> TotalCalificaciones { get; set; }
        public string Descripcion { get; set; }
        public List<Temario> Temarios { get; set; }
        public List<Prueba> Pruebas { get; set; }
        public List<DateTime> FechasClases { get; set; }
        public int Duracion { get; set; }
        public double Precio { get; set; }
        public Dificultad Dificultad { get; set; }

        public Curso() { }
        public Curso(Docente docente, string imagen, string descripcion)
        {
            this.Docente = docente;
            Estudiantes = new List<Estudiante>();
            Semanas = new List<Semana>();
            FechaInicio = DateTime.Now;
            Imagen = imagen;
            Calificacion = 0;
            Descripcion = descripcion;
            Pruebas = new List<Prueba>();
            FechasClases = new List<DateTime>();
            TotalCalificaciones = new List<double>();
        }

        public Curso(Docente docente, string imagen, string descripcion, IEnumerable<Temario> temarios)
        {
            this.Docente = docente;
            Estudiantes = new List<Estudiante>();
            Semanas = new List<Semana>();
            FechaInicio = DateTime.Now;
            Imagen = imagen;
            Calificacion = 0;
            Descripcion = descripcion;
            Pruebas = new List<Prueba>();
            Temarios = temarios.ToList();
            FechasClases = new List<DateTime>();
            TotalCalificaciones = new List<double>();
        }

        public Curso(string nombre, Docente docente, string imagen, DateTime fechaInicio, DateTime fachaFin, string descripcion, IEnumerable<Temario> temarios, double precio, Dificultad dificultad)
        {
            Nombre = nombre;
            this.Docente = docente;
            Estudiantes = new List<Estudiante>();
            Semanas = new List<Semana>();
            FechaInicio = fechaInicio;
            FechaFin = fachaFin;
            Imagen = imagen;
            Calificacion = 0;
            Descripcion = descripcion;
            Pruebas = new List<Prueba>();
            Temarios = temarios.ToList();
            Precio = precio;
            Dificultad = dificultad;
            Duracion = CalcularDuracion();
            FechasClases = new List<DateTime>();
            TotalCalificaciones = new List<double>();
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
            if (Semanas == null)
                Semanas = new List<Semana>();

            DateTime hoy = DateTime.Today;

            if (FechaInicio > hoy)
                return;

            int semanasTranscurridas = (int)((hoy - FechaInicio.Date).TotalDays / 7) + 1;

            if (Semanas.Count == 0)
            {
                Semanas.Add(new Semana
                {
                    Numero = 1,
                    FechaInicio = FechaInicio.Date
                });
            }

            while (Semanas.Count < semanasTranscurridas)
            {
                int numeroNueva = Semanas.Count + 1;
                DateTime fechaInicioNueva = FechaInicio.AddDays((numeroNueva - 1) * 7).Date;

                Semanas.Add(new Semana
                {
                    Numero = numeroNueva,
                    FechaInicio = fechaInicioNueva
                });
            }
        }
        private int CalcularDuracion()
        {
            int meses = (FechaFin.Year - FechaInicio.Year) * 12
                + (FechaFin.Month - FechaInicio.Month);

            int dias = FechaFin.Day - FechaInicio.Day;

            if (dias < 0)
            {
                meses--;
                dias += DateTime.DaysInMonth(FechaInicio.Year, FechaInicio.Month);
            }

            if (dias >= 15)
                meses++;

            return Math.Max(meses, 0);
        }
        #endregion
    }
}