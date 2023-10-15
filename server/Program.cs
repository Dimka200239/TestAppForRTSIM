using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using server.DataBaseMigrator;
using server.Model.Data;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Загрузка конфигурации из файла appsettings.json
        builder.Configuration.AddJsonFile("appsettings.json");
        // Определение текущей среды выполнения
        var environment = builder.Configuration.GetValue<string>("Environment");
        // Загрузка конфигурации из файла appsettings.Development.json или appsettings.Production.json в зависимости от среды
        builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true);

        // Добавление сервисов к контейнеру
        builder.Services.AddDbContext<ApplicationContext>(options =>
        {
            // Получение строки подключения из конфигурации
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            DatabaseMigrator.Migrate(app.Services);
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}