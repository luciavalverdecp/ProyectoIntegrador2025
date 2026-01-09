using NextLevel.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaNegocio.Entidades
{
    public class Pago : IEntity
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public double Monto { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public EstadoPago EstadoPago { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Pago() { }
        public Pago(Usuario usuario, Curso curso, MetodoPago metodoPago)
        {
            Usuario = usuario;
            Curso = curso;
            Monto = curso.Precio;
            MetodoPago = metodoPago;
            EstadoPago = EstadoPago.Pendiente;
            FechaCreacion = DateTime.UtcNow;
        }
    }
}
