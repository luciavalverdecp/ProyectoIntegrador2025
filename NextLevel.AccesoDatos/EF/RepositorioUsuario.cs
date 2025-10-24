using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Usuario;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private NextLevelContext _db;
        public RepositorioUsuario(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Usuario obj)
        {
            try
            {
                _db.Usuarios.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new UsuarioException("No se pudo registrar el usuario.");
            }
        }

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public Usuario FindByEmail(string email)
        {
            //try
            //{
                var usuario = _db.Usuarios.Where(u => u.Email == email).FirstOrDefault();
                return usuario;
            //}
            //catch (Exception ex) 
            //{
            //    throw new UsuarioEmailException("No se encontraron usuarios que coincidan con el email ingresado.");
            //}
        }
    }
}
