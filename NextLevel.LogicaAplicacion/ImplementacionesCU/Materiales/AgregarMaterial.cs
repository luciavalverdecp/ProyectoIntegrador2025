using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using NextLevel.Compartidos.DTOs.Materiales;
using NextLevel.Compartidos.DTOs.Semanas;
using NextLevel.LogicaAplicacion.InterfacesCU.Materiales;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.Materiales
{
    public class AgregarMaterial : IAgregarMaterial
    {
        private readonly IRepositorioMaterial repositorioMaterial;
        private readonly IRepositorioCurso repositorioCurso;
        private readonly IRepositorioSemana repositorioSemana;
        private readonly BlobServiceClient _blobServiceClient;

        public AgregarMaterial(IRepositorioMaterial repositorioMaterial, 
            BlobServiceClient blobServiceClient,
            IRepositorioCurso repositorioCurso,
            IRepositorioSemana repositorioSemana)
        {
            this.repositorioMaterial = repositorioMaterial;
            this.repositorioCurso = repositorioCurso;
            _blobServiceClient = blobServiceClient;
            this.repositorioSemana = repositorioSemana;
        }

        public async Task Ejecutar(MaterialDTO material, int numeroSemana, string nombreCurso)
        {
            var file = material.Archivo;
            string ruta = null;
            if (file != null)
            {
                var contenedor = _blobServiceClient.GetBlobContainerClient("materiales");
                await contenedor.CreateIfNotExistsAsync();
                await contenedor.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.None);


                string nombreUnico = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var blob = contenedor.GetBlobClient(nombreUnico);

                using (var stream = file.OpenReadStream())
                {
                    await blob.UploadAsync(stream);
                }

                ruta = blob.Uri.ToString();
            }
            Curso curso = repositorioCurso.FindByNombre(nombreCurso);
            Semana semana = curso.Semanas[numeroSemana - 1];
            TipoMaterial tipo = obtenerTipoMaterial(material.Archivo);
            Material materialNuevo = new Material(material.Nombre, tipo, ruta, material.Texto);
            semana.Materiales.Add(materialNuevo);
            repositorioSemana.Update(semana);
        }

        private TipoMaterial obtenerTipoMaterial(IFormFile material)
        {
            if (material == null || material.FileName == null)
                throw new ArgumentNullException(nameof(material));

            string extension = Path.GetExtension(material.FileName).ToLower();

            // Grabaciones (videos)
            string[] extensionesGrabaciones = { ".mp4", ".mov", ".avi", ".mkv", ".wmv" };

            // PDFs
            if (extension == ".pdf")
                return TipoMaterial.PDF;

            // Imágenes
            string[] extensionesImagen = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            if (extensionesImagen.Contains(extension))
                return TipoMaterial.Imagen;

            // Grabaciones (videos)
            if (extensionesGrabaciones.Contains(extension))
                return TipoMaterial.Grabaciones;

            // Pruebas (puede ser .docx .xlsx .pptx .zip, etc. adaptalo si querés)
            string[] extensionesPruebas = { ".docx", ".doc", ".xlsx", ".xls", ".pptx", ".ppt" };
            if (extensionesPruebas.Contains(extension))
                return TipoMaterial.Pruebas;

            // Si no es ninguna de las anteriores, lo tratamos como Archivo genérico
            return TipoMaterial.Archivo;
        }

    }
}
