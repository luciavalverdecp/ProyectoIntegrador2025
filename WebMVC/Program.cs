using Microsoft.EntityFrameworkCore;
using NextLevel.AccesoDatos.EF;
using NextLevel.LogicaNegocio.InterfacesRepositorios;

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
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionNextLevel"))); builder.Services.AddControllersWithViews();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseSession();
            app.Run();
        }
    }
}
