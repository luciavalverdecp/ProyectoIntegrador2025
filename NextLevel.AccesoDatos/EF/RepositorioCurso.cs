using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.AccesoDatos.EF
{
    public class RepositorioCurso : IRepositorioCurso
    {
        private NextLevelContext _db;
        public RepositorioCurso(NextLevelContext db)
        {
            _db = db;
        }
        public void Add(Curso obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Curso> FindAll()
        {
            try
            {
                var cursos = _db.Cursos
                             .Include(c => c.Docente)
                             .Include(c => c.Estudiantes)
                             .Include(c => c.Semanas)
                             .Include(c => c.Temarios);
                return cursos.ToList();
            }
            catch (Exception ex)
            {
                throw new CursoException("Error al obtener los cursos", ex);
            }
        }

        public IEnumerable<Curso> FindWithFilter(string filtro, IEnumerable<Curso> lista)
        {
            try
            {
                Func<string, string> sinTildes = s =>
                {
                    if (s == null) return null;
                    var formD = s.Normalize(System.Text.NormalizationForm.FormD);
                    return new string(formD
                        .Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c)
                                    != System.Globalization.UnicodeCategory.NonSpacingMark)
                        .ToArray()
                    ).Normalize(System.Text.NormalizationForm.FormC);
                };

                filtro = sinTildes(filtro.ToLower());

                var query = lista.Where(c =>
                    (c.Nombre != null && sinTildes(c.Nombre.ToLower()).Contains(filtro))
                    || (c.Descripcion != null && sinTildes(c.Descripcion.ToLower()).Contains(filtro))
                    || (c.Temarios != null && c.Temarios.Any(t =>
                            t.Tema != null && sinTildes(t.Tema.ToLower()).Contains(filtro)
                       ))
                );

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new CursoException("Error al obtener los cursos filtrados", ex);
            }
        }


        public IEnumerable<Curso> FindWithCategory(string categoria, string? alfabetico, int? calificacion, string? docente, IEnumerable<Curso> lista)
        {
            try
            {
                var query = lista;

                if (categoria == "Nombre" && !string.IsNullOrEmpty(alfabetico))
                {
                    if (alfabetico.ToLower() == "asc")
                    {
                        query = lista.OrderBy(c => c.Nombre);
                    }
                    else if (alfabetico.ToLower() == "desc")
                    {
                        query = lista.OrderByDescending(c => c.Nombre);
                    }
                }
                else if (categoria == "Calificacion" && calificacion.HasValue)
                {
                    int valor = calificacion.Value;

                    int min = valor;
                    int max = valor + 1;

                    query = lista.Where(c =>
                        c.Calificacion >= min &&
                        c.Calificacion <= max
                    );
                }
                else if (categoria == "Docente" && !string.IsNullOrEmpty(docente))
                {
                    var filtro = docente.ToLower();
                    query = lista.Where(c =>
                        c.Docente != null &&
                        c.Docente.NombreCompleto.ToLower().Contains(filtro)
                    );
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new CursoException("Error al obtener los cursos filtrados", ex);
            }
        }

        public Curso FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Curso obj)
        {
            throw new NotImplementedException();
        }

        public Curso FindByNombre(string nombre)
        {
            return _db.Cursos.Where(c => c.Nombre == nombre).FirstOrDefault();
        }
    }
}
