using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using NextLevel.Compartidos.DTOs.Cursos;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.Compartidos.DTOs.Temarios;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.AltaCurso;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.Curso;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Cursos
{
    public class AltaCursoCU : IAltaCurso
    {
        private readonly IRepositorioCurso _repositorioCurso;
        private readonly IRepositorioDocente _repositorioDocente;
        private readonly IRepositorioAltaCurso _repositorioAltaCurso;
        private readonly IRepositorioConversacion _repositorioConversacion;
        private readonly BlobServiceClient _blobServiceClient;

        public AltaCursoCU(
            IRepositorioCurso repositorioCurso,
            IRepositorioDocente repositorioDocente,
            IRepositorioAltaCurso repositorioAltaCurso,
            BlobServiceClient blobServiceClient,
            IRepositorioConversacion repositorioConversacion)
        {
            _repositorioCurso = repositorioCurso;
            _repositorioDocente = repositorioDocente;
            _repositorioAltaCurso = repositorioAltaCurso;
            _blobServiceClient = blobServiceClient;
            _repositorioConversacion = repositorioConversacion;
        }

        public async Task Ejecutar(CursoAltaDTO cursoAltaDTO, List<IFormFile> archivos, string email, IFormFile imagen)
        {
            if (cursoAltaDTO.Nombre == null || cursoAltaDTO.Nombre == "")
                throw new CursoNombreException("El nombre del curso no puede ser vacío.");

            if (cursoAltaDTO.Descripcion == null || cursoAltaDTO.Descripcion == "")
                throw new CursoDescripcionException("La descripción del curso no puede ser vacía.");

            if (cursoAltaDTO.FechaInicio == new DateTime() || cursoAltaDTO.FechaFin == new DateTime())
                throw new CursoFechaException("Debe seleccionar una fecha de inicio y de fin para el curso.");

            if (cursoAltaDTO.Precio <= 0)
                throw new CursoPrecioException("El precio del curso debe ser mayor a 0.");

            if (!Enum.IsDefined(typeof(Dificultad), cursoAltaDTO.Dificultad))
                throw new CursoDificultadException("Debe seleccionar una dificultad válida");

            if (cursoAltaDTO.Temarios.Count() < 3)
                throw new CursoTemarioException("El curso debe contener al menos 3 temarios.");

            if (archivos.Count() <= 0)
                throw new AltaCursoArchivosException("Debe ingresar al menos un archivo para validar su formación.");

            if (imagen == null)
                throw new CursoException("Debe ingresar una imagen para el curso.");

            Docente docente = (Docente)_repositorioDocente.FindByEmail(email);

            CursoAltaDTO altaDTO = new CursoAltaDTO(
                cursoAltaDTO.Id,
                cursoAltaDTO.Nombre,
                DocenteMapper.ToDocenteNombreDTO(docente),
                cursoAltaDTO.Imagen,
                cursoAltaDTO.FechaInicio,
                cursoAltaDTO.FechaFin,
                cursoAltaDTO.Descripcion,
                cursoAltaDTO.Temarios,
                cursoAltaDTO.Precio,
                cursoAltaDTO.Dificultad
            );

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
            nuevoCurso.Foro = null;

            var temariosGuardados = nuevoCurso.Temarios;
            nuevoCurso.Temarios = new List<Temario>();

            _repositorioCurso.Add(nuevoCurso);


            foreach (var t in temariosGuardados)
            {
                t.Curso = nuevoCurso;
                nuevoCurso.Temarios.ToList().Add(t);
            }

            var contenedor = _blobServiceClient.GetBlobContainerClient("altacursos");
            await contenedor.CreateIfNotExistsAsync();
            await contenedor.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.None);

            string imagenUrl = null;

            string nombreUnicoImg = "img_" + Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
            var blobImg = contenedor.GetBlobClient(nombreUnicoImg);

            using (var stream = imagen.OpenReadStream())
            {
                await blobImg.UploadAsync(stream);
            }

            imagenUrl = blobImg.Uri.ToString();
            nuevoCurso.Imagen = imagenUrl;

            List<Archivo> archivosEntidad = new();

            foreach (var file in archivos)
            {
                if (file != null && file.Length > 0)
                {
                    string nombreUnico = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    var blob = contenedor.GetBlobClient(nombreUnico);

                    using (var stream = file.OpenReadStream())
                    {
                        await blob.UploadAsync(stream);
                    }

                    Archivo archivoEntidad = new Archivo(
                        nombreArchivo: file.FileName,
                        ruta: blob.Uri.ToString(),
                        peso: file.Length,
                        contentType: file.ContentType
                    );

                    archivosEntidad.Add(archivoEntidad);
                }
            }

            var conversacion1 = new Conversacion(
                                    TipoConversacion.Foro,
                                    nuevoCurso.Id
                                );
            conversacion1.Curso = nuevoCurso;
            var foro = new Foro(conversacion1);
            nuevoCurso.Foro = foro;

            _repositorioCurso.Update(nuevoCurso);
            AltaCurso altaCurso = new AltaCurso(nuevoCurso, archivosEntidad);
            _repositorioAltaCurso.Add(altaCurso);
        }
    }
}
