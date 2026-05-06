using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DigitalWorkshop.Infrastructure.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Читаем строку подключения из appsettings.json в проекте WebUI
            var basePath = Directory.GetCurrentDirectory();
            // Путь может отличаться в зависимости от того, откуда запускается команда, 
            // но обычно для DesignTime_factory лучше явно указать путь к WebUI или использовать относительный
            var configPath = Path.Combine(basePath, "src", "DigitalWorkshop.WebUI", "appsettings.json");

            // Если файл не найден по этому пути (например, мы внутри папки WebUI), пробуем текущую директорию
            if (!File.Exists(configPath))
            {
                configPath = Path.Combine(basePath, "appsettings.json");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}