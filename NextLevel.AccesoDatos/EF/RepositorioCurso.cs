using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using EfCore = Microsoft.EntityFrameworkCore.EF;

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
            try
            {
                _db.Cursos.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new CursoException(
        "Error al dar de alta el curso: " + ex.InnerException?.Message ?? ex.Message
    );
            }
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
            var curso = _db.Cursos.Find(obj.Id);
            try
            {
                curso.Temarios = obj.Temarios;
                curso.FechasClases = obj.FechasClases;
                curso.Foro = obj.Foro;
                curso.TotalCalificaciones = obj.TotalCalificaciones;
                _db.Cursos.Update(curso);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new CursoException("Error al actualizar el curso", ex);
            }
        }

        public Curso FindByNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return null;

            nombre = nombre.Replace("_", " ");

            var curso = _db.Cursos.Where(c => c.Nombre == nombre)
                .Include(c => c.Pruebas)
                .Include(c => c.Docente)
                .Include(c => c.Temarios)
                .Include(c => c.Estudiantes)
                .Include(c => c.Foro)
                    .ThenInclude(f=> f.Conversacion)
                .Include(c => c.Semanas)
                    .ThenInclude(s => s.Materiales).FirstOrDefault();

            if (curso == null)
                return null;

            curso.ActualizarSemanas();
            this.Update(curso);

            return curso;
        }

        public IEnumerable<Curso> GetByDocente(Usuario usuario)
        {
            return _db.Cursos.Where(c => c.Docente.Email == usuario.Email).ToList();
        }

        public IEnumerable<Curso> Buscar(string? categoria, string? dificultad, int? duracionMax, string? NombreDocente, DateTime? FechaInicio, double? Calificacion, double? Precio)
        {
            IQueryable<Curso> query = _db.Cursos;


            if (!string.IsNullOrWhiteSpace(categoria))
            {
                query = query.Where(c =>
                c.Nombre.ToLower().Contains(categoria.ToLower()) || c.Descripcion.ToLower().Contains(categoria.ToLower()));
            }


            if (!string.IsNullOrWhiteSpace(dificultad) &&
                Enum.TryParse<Dificultad>(dificultad, true, out var dif))
            {
                query = query.Where(c => c.Dificultad == dif);
            }



            if (duracionMax.HasValue)
            {
                query = query.Where(c =>
                c.Duracion <= duracionMax.Value);
            }

            if (!string.IsNullOrWhiteSpace(NombreDocente))
            {
                query = query.Where(c =>
                EfCore.Functions.Collate(
                    c.Docente.NombreCompleto.ToLower(),
                    "Latin1_General_CI_AI"
                ).Contains(NombreDocente.ToLower()));
            }


            if (FechaInicio.HasValue)
            {
                var desde = FechaInicio.Value.Date;
                var hasta = desde.AddDays(14);

                query = query.Where(c =>
                    c.FechaInicio >= desde &&
                    c.FechaInicio <= hasta);
            }


            if (Calificacion.HasValue)
            {
                query = query.OrderByDescending(c => c.Calificacion);
            }

            if (Precio.HasValue)
            {
                var tolerancia = Precio.Value * 0.15;
                var min = Precio.Value - tolerancia;
                var max = Precio.Value + tolerancia;

                query = query.Where(c =>
                    c.Precio >= min && c.Precio <= max);
            }

            return query.Include(c => c.Docente).ToList();
        }
    }
}
