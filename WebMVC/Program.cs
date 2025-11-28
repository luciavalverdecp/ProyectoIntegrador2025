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

            //REPOSITORIOS
            builder.Services.AddScoped<IRepositorioAdministrador, RepositorioAdministrador>();
            builder.Services.AddScoped<IRepositorioCambioRol, RepositorioCambioRol>();
            builder.Services.AddScoped<IRepositorioCurso, RepositorioCurso>();
            builder.Services.AddScoped<IRepositorioDocente, RepositorioDocente>();
            builder.Services.AddScoped<IRepositorioEstudiante, RepositorioEstudiante>();
            builder.Services.AddScoped<IRepositorioForo, RepositorioForo>();
            builder.Services.AddScoped<IRepositorioMaterial, RepositorioMaterial>();
            builder.Services.AddScoped<IRepositorioMensaje, RepositorioMensaje>();
            builder.Services.AddScoped<IRepositorioMensajeria, RepositorioMensajeria>();
            builder.Services.AddScoped<IRepositorioSemana, RepositorioSemana>();
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

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
