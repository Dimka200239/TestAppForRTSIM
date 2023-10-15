using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using server.DataBaseMigrator;
using server.Model.Data;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json");
        var environment = builder.Configuration.GetValue<string>("Environment");
        builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true);

        builder.Services.AddDbContext<ApplicationContext>(options =>
        {
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
            app.UseSwagger();
            app.UseSwaggerUI();
            DatabaseMigrator.Migrate(app.Services);
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}