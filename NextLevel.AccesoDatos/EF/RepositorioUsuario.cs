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
            var usuario = _db.Usuarios.Find(id);
            try
            {
                _db.Usuarios.Remove(usuario);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update(Usuario obj)
        {
            var usuarioOriginal = _db.Usuarios.Find(obj.Id);
            try
            {
                usuarioOriginal.Email = obj.Email;
                usuarioOriginal.EstaVerificado = obj.EstaVerificado;
                usuarioOriginal.TokenVerificacion = obj.TokenVerificacion;
                usuarioOriginal.TokenVencimiento = obj.TokenVencimiento;
                usuarioOriginal.NombreCompleto = obj.NombreCompleto;
                usuarioOriginal.Password = obj.Password;
                usuarioOriginal.Telefono = obj.Telefono;
                _db.Usuarios.Update(usuarioOriginal);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Usuario FindByEmail(string email)
        {
            return _db.Usuarios.Where(u => u.Email == email).FirstOrDefault();
        }

        public Usuario FindByToken(string token)
        {
            return _db.Usuarios.Where(u => u.TokenVerificacion == token).FirstOrDefault();
        }

        public void UpdateUserAuthentication(Usuario obj)
        {
            var usuarioOriginal = _db.Usuarios.Find(obj.Id);
            try
            {
                usuarioOriginal.EstaVerificado = true;
                usuarioOriginal.TokenVerificacion = "";
                usuarioOriginal.TokenVencimiento = new DateTime();
                _db.Usuarios.Update(usuarioOriginal);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
