using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using NextLevel.Compartidos.DTOs.CambioRoles;
using NextLevel.Compartidos.DTOs.Mappers;
using NextLevel.LogicaAplicacion.InterfacesCU.CambiosDeRol;
using NextLevel.LogicaNegocio.Entidades;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.AltaCurso;
using NextLevel.LogicaNegocio.ExcepcionesEntidades.CambioRol;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NextLevel.LogicaAplicacion.ImplementacionesCU.CambiosDeRol
{
    public class CambioDeRolCU : ICambioDeRol
	{
		private readonly IRepositorioCambioRol _repositorioCambioRol;
		private readonly IRepositorioEstudiante _repositorioEstudiante;
		private readonly IRepositorioDocente _repositorioDocente;
		private readonly BlobServiceClient _blobServiceClient;

		public CambioDeRolCU (IRepositorioCambioRol repositorioCambioRol, 
			BlobServiceClient blobServiceClient,
			IRepositorioEstudiante repositorioEstudiante,
			IRepositorioDocente repositorioDocente)
		{
			_repositorioCambioRol = repositorioCambioRol;
			_blobServiceClient = blobServiceClient;
			_repositorioEstudiante = repositorioEstudiante;
			_repositorioDocente = repositorioDocente;
		}

		public async Task Ejecutar(CambioRolDTO cambioRolDTO, List<IFormFile> archivos)
		{
			var cambioPendiente = _repositorioCambioRol.FindByEmail(cambioRolDTO.Estudiante.Email);
			if (cambioPendiente != null)
				throw new CambioRolExistenteException("El usuario tiene un cambio de rol pendiente de aprobación.");
			var docenteExistente = _repositorioDocente.FindByEmail(cambioRolDTO.Estudiante.Email);
			if (docenteExistente != null)
				throw new CambioRolDocenteExistenteException("Este correo ya tiene una cuenta de tipo Docente.");
			if (archivos.Count() <= 0)
				throw new AltaCursoArchivosException("Debe ingresar al menos un archivo para validar su formación.");

			CambioRol nuevoCambioRol = CambioRolesMapper.FromCambioRolDTO(cambioRolDTO);

			Estudiante estu = _repositorioEstudiante.FindByEmail(nuevoCambioRol.Estudiante.Email);
			nuevoCambioRol.Estudiante = estu;

			var contenedor = _blobServiceClient.GetBlobContainerClient("cambiorol");
			await contenedor.CreateIfNotExistsAsync();
			await contenedor.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.None);

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

			nuevoCambioRol.Archivos = archivosEntidad;
			_repositorioCambioRol.Add(nuevoCambioRol);
        }
	}
}
