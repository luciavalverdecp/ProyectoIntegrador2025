using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NextLevel.AccesoDatos.EF
{
    public class NextLevelContextFactory
        : IDesignTimeDbContextFactory<NextLevelContext>
    {
        public NextLevelContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NextLevelContext>();

            optionsBuilder.UseSqlServer(
                "Server=tcp:nextlevelserver2025.database.windows.net,1433;" +
                "Initial Catalog=databaseNextLevel;" +
                "Persist Security Info=False;" +
                "User ID=LuciaMauricio;" +
                "Password=ProyectoLM2025;" +
                "Encrypt=True;" +
                "TrustServerCertificate=False;"
            );

            return new NextLevelContext(optionsBuilder.Options);
        }
    }
}
