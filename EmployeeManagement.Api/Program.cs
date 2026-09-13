using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//Discovers the Controllers so Swagger can map them
builder.Services.AddEndpointsApiExplorer();

// Configures NSwag document generation
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "v1";
    config.Title = "Employee Management API";
    config.Version = "v1";
});

// Database registration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("EmployeeManagement.Infrastructure") // <-- FIXED: Migrations are in Infrastructure!
    ));

// Clean Architecture Dependency Injection mappings
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    // Generates the raw openapi/swagger JSON specification file (Required by NSwag)
    app.UseOpenApi();

    // Generates the visual Web UI page for testing endpoints
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Maps controller routes so your API endpoints are accessible
app.MapControllers();

app.Run();
