using Microsoft.EntityFrameworkCore;
using PersonasAPI.Data;
//Test Respositorio

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Personas API",
        Version = "v1",
        Description = "API para gestionar personas"
    });
});

// Add logging configuration
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Information);
});

var app = builder.Build();

// Log application startup
app.Logger.LogInformation("Starting up the application");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Personas API V1");
    c.RoutePrefix = string.Empty; // Para que Swagger UI sea la página de inicio
});

app.UseHttpsRedirection();

// Configure CORS
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

// Add a health check endpoint
app.MapGet("/health", () => "API is running!");

// Log that we're about to start the application
app.Logger.LogInformation("Application configured and ready to start");

app.Run();
