using Microsoft.EntityFrameworkCore;
using NextLevel.AccesoDatos.EF;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Usuarios;
using NextLevel.LogicaAplicacion.InterfacesCU.Usuarios;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Estudiantes;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiante;
using NextLevel.LogicaNegocio.InterfacesRepositorios;
using NextLevel.LogicaAplicacion.InterfacesCU.Cursos;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Cursos;
using NextLevel.LogicaAplicacion.InterfacesCU.Estudiantes;
using Azure.Storage.Blobs;
using NextLevel.LogicaAplicacion.InterfacesCU.CambiosDeRol;
using NextLevel.LogicaAplicacion.ImplementacionesCU.CambiosDeRol;
using NextLevel.LogicaAplicacion.InterfacesCU.Docentes;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Docentes;
using NextLevel.LogicaAplicacion.InterfacesCU.Materiales;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Materiales;
using NextLevel.LogicaAplicacion.InterfacesCU.Mensajes;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Mensajes;
using NextLevel.LogicaAplicacion.InterfacesCU.Pagos;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Pagos;
using NextLevel.LogicaAplicacion.InterfacesCU.ParticipantesConversacion;
using NextLevel.LogicaAplicacion.ImplementacionesCU.ParticipantesConversacion;
using NextLevel.LogicaAplicacion.InterfacesCU.Conversaciones;
using NextLevel.LogicaAplicacion.ImplementacionesCU.Conversaciones;

namespace WebMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession(
                options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(30);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                }
            );

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<NextLevelContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionNextLevel")));

            builder.Services.AddSingleton(x =>
            {
                // Leer desde appsettings.json
                string connectionString = builder.Configuration["Azure:BlobStorage"];

                // Si está vacía, intentar desde variable de entorno
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = Environment.GetEnvironmentVariable("AZURE_BLOB_STORAGE");
                }

                // Si sigue siendo nula, lanzar un error claro
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException(
                        "La cadena de conexión de Azure Blob Storage no está configurada. " +
                        "Define AZURE_BLOB_STORAGE en tu entorno o en appsettings.json.");
                }

                return new BlobServiceClient(connectionString);
            });


            //REPOSITORIOS
            builder.Services.AddScoped<IRepositorioAdministrador, RepositorioAdministrador>();
            builder.Services.AddScoped<IRepositorioCambioRol, RepositorioCambioRol>();
            builder.Services.AddScoped<IRepositorioCurso, RepositorioCurso>();
            builder.Services.AddScoped<IRepositorioDocente, RepositorioDocente>();
            builder.Services.AddScoped<IRepositorioEstudiante, RepositorioEstudiante>();
            builder.Services.AddScoped<IRepositorioForo, RepositorioForo>();
            builder.Services.AddScoped<IRepositorioMaterial, RepositorioMaterial>();
            builder.Services.AddScoped<IRepositorioMensaje, RepositorioMensaje>();
            builder.Services.AddScoped<IRepositorioSemana, RepositorioSemana>();
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            builder.Services.AddScoped<IRepositorioAltaCurso, RepositorioAltaCurso>();
            builder.Services.AddScoped<IRepositorioConversacion, RepositorioConversacion>(); 
            builder.Services.AddScoped<IRepositorioParticiapanteConversacion, RepositorioParticipanteConversacion>();
            builder.Services.AddScoped<IRepositorioPostulacion, RepositorioPostulacion>();
            builder.Services.AddScoped<IRepositorioPago, RepositorioPago>();

            //CASOS DE USO
            builder.Services.AddScoped<IRegistroEstudiante, RegistroEstudiante>();
            builder.Services.AddScoped<ILoginUsuario, LoginUsuario>();
            builder.Services.AddScoped<IRecuperarCuenta, RecuperarCuenta>();
            builder.Services.AddScoped<IObtenerCursosFiltrados, ObtenerCursosFiltrados>();
            builder.Services.AddScoped<IObtenerMisCursos, ObtenerMisCursos>();
            builder.Services.AddScoped<IObtenerEstudiante, ObtenerEstudiante>();
            builder.Services.AddScoped<ICursosTerminados, CursosTerminados>();
            builder.Services.AddScoped<IObtenerCursosDocente, ObtenerCursosDocente>();
            builder.Services.AddScoped<IObtenerCurso, ObtenerCurso>();
            builder.Services.AddScoped<IAltaCurso, AltaCursoCU>();
            builder.Services.AddScoped<ICambioDeRol, CambioDeRolCU>();
            builder.Services.AddScoped<IObtenerDocente, ObtenerDocente>();
            builder.Services.AddScoped<ICRUDMaterial, CRUDMaterial>();
            builder.Services.AddScoped<IAgregarClase, AgregarClase>();
            builder.Services.AddScoped<IModificarEstudiante, ModificarEstudiante>();
            builder.Services.AddScoped<IModificarDocente, ModificarDocente>();
            builder.Services.AddScoped<IObtenerMensajes, ObtenerMensajes>();
            builder.Services.AddScoped<IEnviarMensaje, EnviarMensaje>();
            builder.Services.AddScoped<IRealizarPago, RealizarPago>();
            builder.Services.AddScoped<IAgregarCurso, AgregarCurso>();
            builder.Services.AddScoped<IObtenerPartiConversaciones, ObtenerPartiConversaciones>();
            builder.Services.AddScoped<IObtenerConversacion, ObtenerConversacion>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Cursos}/{action=ListadoCursos}/{id?}");

            app.UseSession();
            app.Run();
        }
    }
}