using Microsoft.AspNetCore.Http;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.Compartidos.DTOs.Temarios;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.AltaCurso;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Cursos
{
    public class AltaCursoCU : IAltaCurso
    {
        private readonly IRepositorioCurso _repositorioCurso;
        private readonly IRepositorioDocente _repositorioDocente;
        private readonly IRepositorioAltaCurso _repositorioAltaCurso;
        public AltaCursoCU(IRepositorioCurso repositorioCurso, IRepositorioDocente repositorioDocente, IRepositorioAltaCurso repositorioAltaCurso)
        {
            _repositorioCurso = repositorioCurso;
            _repositorioDocente = repositorioDocente;
            _repositorioAltaCurso = repositorioAltaCurso;
        }

        public void Ejecutar(CursoAltaDTO cursoAltaDTO, List<IFormFile> archivos, string email)
        {
            if (cursoAltaDTO.Nombre == null || cursoAltaDTO.Nombre == "") 
            {
                throw new CursoNombreException("El nombre del curso no puede ser vacío.");
            }
            if (cursoAltaDTO.Descripcion == null || cursoAltaDTO.Descripcion == "")
            {
                throw new CursoDescripcionException("La descripción del curso no puede ser vacía.");
            }
            if (cursoAltaDTO.FechaInicio == new DateTime() || cursoAltaDTO.FechaFin == new DateTime())
            {
                throw new CursoFechaException("Debe seleccionar una fecha de inicio y de fin para el curso.");

            }
            if (cursoAltaDTO.Precio <= 0)
            {
                throw new CursoPrecioException("El precio del curso debe ser mayor a 0.");
            }
            if (!Enum.IsDefined(typeof(Dificultad), cursoAltaDTO.Dificultad))
            {
                throw new CursoDificultadException("Debe seleccionar una dificultad válida");
            }
            if (cursoAltaDTO.Temarios.Count() < 3)
            {
                throw new CursoTemarioException("El curso debe contener al menos 3 temarios.");
            }
            if(archivos.Count() <= 0)
            {
                throw new AltaCursoArchivosException("Debe ingresar al menos un archivo para validar su formación.");
            }

            Docente docente = _repositorioDocente.FindByEmail(email);
            CursoAltaDTO altaDTO = new CursoAltaDTO(cursoAltaDTO.Id, cursoAltaDTO.Nombre, DocenteMapper.ToDocenteNombreDTO(docente), cursoAltaDTO.Imagen, cursoAltaDTO.FechaInicio, cursoAltaDTO.FechaFin, cursoAltaDTO.Descripcion, cursoAltaDTO.Temarios, cursoAltaDTO.Precio, cursoAltaDTO.Dificultad);
            var nuevosTemarios = altaDTO.Temarios
            .Select(t => new TemarioVistaPreviaDTO(
                t.Tema,
                new CursoNombreDTO(altaDTO.Nombre)
            ))
            .ToList();

            altaDTO = altaDTO with { Temarios = nuevosTemarios };
            Curso nuevoCurso = CursoMapper.FromCursoAltaDTO(altaDTO);
            nuevoCurso.Docente = docente;
            nuevoCurso.Imagen = "";
            nuevoCurso.DocenteId = docente.Id;
            var temariosGuardados = nuevoCurso.Temarios;
            nuevoCurso.Temarios = new List<Temario>();
            _repositorioCurso.Add(nuevoCurso);
            foreach(var t in temariosGuardados)
            {
                t.Curso = nuevoCurso;
                nuevoCurso.Temarios.ToList().Add(t);
            }
            _repositorioCurso.Update(nuevoCurso);

            List<Archivo> archivosEntidad = new List<Archivo>();

            foreach (var file in archivos)
            {
                if (file != null && file.Length > 0)
                {
                    string nombreArchivo = Path.GetFileName(file.FileName);
                    string carpetaDestino = Path.Combine("wwwroot", "uploads", "cursos");
                    Directory.CreateDirectory(carpetaDestino);

                    string rutaCompleta = Path.Combine(carpetaDestino, nombreArchivo);

                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    Archivo archivoEntidad = new Archivo(
                        nombreArchivo: nombreArchivo,
                        ruta: rutaCompleta,
                        peso: file.Length,
                        contentType: file.ContentType
                    );

                    archivosEntidad.Add(archivoEntidad);
                }
            }

            AltaCurso altaCurso = new AltaCurso(nuevoCurso, archivosEntidad);
            _repositorioAltaCurso.Add(altaCurso);
        }
    }
}
