using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.InterfacesEntidades;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Estudiante: Usuario, IEntity, IComparable<Estudiante>
    {
        public string Cedula {  get; set; }
        public List<Curso> Cursos {  get; set; }
        [AllowNull]
        public CambioRol CambioRol { get; set; } = null;

        public Estudiante() { }
        public Estudiante(string email, string password, string nombreCompleto, string telefono, string cedula) : base(email, password, nombreCompleto,telefono)
        {
            Cedula = cedula;
            this.Validar();
        }
        #region Validaciones

        public override void Validar()
        {
            base.Validar();
            validarCedula();
        }

        private void validarCedula()
        {
            if (string.IsNullOrEmpty(Cedula)) throw new EstudianteCedulaException("La cedula no puede ser vacia");
            if (!validacionDeCedula()) throw new EstudianteCedulaException("La cedula no tiene un formato correcto");
        }

        public bool validacionDeCedula()
        {
            string cedula = this.Cedula;

            // Verificar si la cédula es nula o está vacía
            if (cedula == null || cedula == "")
            {
                return false;
            }

            int length = cedula.Length;

            // Validar largo permitido (9 o 11 caracteres)
            if (length != 9 && length != 11)
            {
                return false;
            }

            // Validar que el último carácter sea un dígito
            if (!char.IsDigit(cedula.ElementAt(length - 1)))
            {
                return false;
            }

            // Validar que el penúltimo carácter sea un guion
            if (cedula.ElementAt(length - 2) != '-')
            {
                return false;
            }

            // Validar estructura según el largo
            if (length == 9)
            {
                // Esperado: 123.456-7
                return char.IsDigit(cedula.ElementAt(0)) &&
                        char.IsDigit(cedula.ElementAt(1)) &&
                        char.IsDigit(cedula.ElementAt(2)) &&
                        cedula.ElementAt(3) == '.' &&
                        char.IsDigit(cedula.ElementAt(4)) &&
                        char.IsDigit(cedula.ElementAt(5)) &&
                        char.IsDigit(cedula.ElementAt(6)) &&
                        cedula.ElementAt(7) == '-';
            }
            else
            {
                // Esperado: 1.234.567-8
                return char.IsDigit(cedula.ElementAt(0)) &&
                        cedula.ElementAt(1) == '.' &&
                        char.IsDigit(cedula.ElementAt(2)) &&
                        char.IsDigit(cedula.ElementAt(3)) &&
                        char.IsDigit(cedula.ElementAt(4)) &&
                        cedula.ElementAt(5) == '.' &&
                        char.IsDigit(cedula.ElementAt(6)) &&
                        char.IsDigit(cedula.ElementAt(7)) &&
                        char.IsDigit(cedula.ElementAt(8)) &&
                        cedula.ElementAt(9) == '-';
            }
        }
        #endregion

        #region Equals - CompareTo
        public override bool Equals(object? obj)
        {
            return obj is Estudiante e && e.Cedula == this.Cedula;
        }

        public int CompareTo(Estudiante? other)
        {
            return this.Cedula.CompareTo(other.Cedula);
        }
        #endregion


    }
}
