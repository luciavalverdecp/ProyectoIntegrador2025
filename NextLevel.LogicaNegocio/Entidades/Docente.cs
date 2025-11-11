using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Docente;
using NextLevel.LogicaNegocio.InterfacesEntidades;
using NextLevel.LogicaNegocio.ValueObject.Docente;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Docente : Usuario, IEntity, IComparable<Docente>
    {
        [Column("NroDocente_NroDeDocente")]
        public NroDocente NroDocente {  get; set; }
        public List<Curso> Cursos {  get; set; }

        public Docente() { }
        public Docente(string email, string password, string nombreCompleto, string telefono, int nroDocente) : base(email, password, nombreCompleto, telefono)
        {
            NroDocente = new NroDocente(nroDocente);
            this.Validar();
        }
        #region Validaciones
        public override void Validar()
        {
            base.Validar();
            validarNroDocente();
        }

        private void validarNroDocente()
        {
            if (NroDocente == null) throw new DocenteNroDocenteException("El numero de docente no puede ser nulo");
            if (int.IsNegative(NroDocente.NroDeDocente)) throw new DocenteNroDocenteException("El numero de docente no puede ser negativo");
        }
        #endregion

        #region Equals - CompareTo
        public override bool Equals(object? obj)
        {
            return obj is Docente e && e.NroDocente == this.NroDocente;
        }

        public int CompareTo(Docente? other)
        {
            return this.NroDocente.NroDeDocente.CompareTo(other.NroDocente.NroDeDocente);
        }
        #endregion
    }
}
