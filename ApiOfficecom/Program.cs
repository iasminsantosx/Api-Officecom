using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder; // Adicione esta diretiva
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate(); // Aplica as migrações
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>(); // Certifique-se de que a classe Startup está presente
                webBuilder.UseUrls("http://+:5000"); // Escuta na porta 5000
            })
            .ConfigureServices((_, services) =>
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer("Server=db;Database=pgc;User Id=sa;Password=Master@123;Encrypt=False;TrustServerCertificate=True;MultiSubnetFailover=True"))); // Substitua pela sua string de conexão
}
