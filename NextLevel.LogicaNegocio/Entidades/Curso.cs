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
        public Foro Foro { get; set; }
        [NotMapped]
        public IFormFile Archivo { get; set; }//TODO Creo que esto no hace falta
        public string RutaArchivo { get; set; }
        public DateTime FechaInicio { get; set; }
        [AllowNull]
        public DateTime FechaFin { get; set; }
        public double Calificacion { get; set; }
        public string Descripcion { get; set; }
        public IEnumerable<Temario> Temarios { get; set; }
        public IEnumerable<Prueba> Pruebas { get; set; }

        public Curso() { }
        public Curso(Docente docente, string rutaArchivo, string descripcion)
        {
            this.Docente = docente;
            Estudiantes = new List<Estudiante>();
            Semanas = new List<Semana>();
            Foro = new Foro();
            FechaInicio = DateTime.Now;
            RutaArchivo = rutaArchivo;
            Calificacion = 0;
            Descripcion = descripcion;
            Pruebas = new List<Prueba>();
        }

        public Curso(Docente docente, string rutaArchivo, string descripcion, IEnumerable<Temario> temarios)
        {
            this.Docente = docente;
            Estudiantes = new List<Estudiante>();
            Semanas = new List<Semana>();
            Foro = new Foro();
            FechaInicio = DateTime.Now;
            RutaArchivo = rutaArchivo;
            Calificacion = 0;
            Descripcion = descripcion;
            Pruebas = new List<Prueba>();
            Temarios = temarios;
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
                    FechaInicio = fechaInicioNueva
                });
            }
        }
        #endregion


    }
}