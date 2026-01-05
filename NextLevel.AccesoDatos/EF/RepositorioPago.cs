using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Estudiante;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioPago : IRepositorioPago
    {
        private NextLevelContext _db;
        public RepositorioPago(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Pago obj)
        {
            try
            {
                _db.Pagos.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar el pago.");//TODO Crear excepciones para pago
            }
        }

        public IEnumerable<Pago> FindAll()
        {
            throw new NotImplementedException();
        }

        public Pago FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Pago obj)
        {
            var pagoOriginal = _db.Pagos.Find(obj.Id);
            try
            {
                pagoOriginal.EstadoPago = obj.EstadoPago;
                _db.Pagos.Update(pagoOriginal);
                _db.SaveChanges();
            }
            catch(Exception ex) 
            {
                throw new Exception("Error al actualizar el pago", ex);
            }
        }
    }
}
