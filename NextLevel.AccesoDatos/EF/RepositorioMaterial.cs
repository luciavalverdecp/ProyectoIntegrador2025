using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioMaterial : IRepositorioMaterial
    {
        private NextLevelContext _db;
        public RepositorioMaterial(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Material obj)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Material material)
        {
            try
            {
                _db.Materiales.Remove(material);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("No fue posible eliminar el material");
            }
        }

        public IEnumerable<Material> FindAll()
        {
            throw new NotImplementedException();
        }

        public Material FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Material FindByRuta(string rutaArchivo)
        {
            try
            {
                return _db.Materiales.Where(m => m.RutaArchivo == rutaArchivo).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el material.");
            }
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Material obj)
        {
            throw new NotImplementedException();
        }
    }
}
