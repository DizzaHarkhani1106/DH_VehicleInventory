using DH_VehicleInventory.Application.Interfaces;
using DH_VehicleInventory.Application.Services;
using DH_VehicleInventory.Application.Validators;
using DH_VehicleInventory.Infrastructure.Data;
using DH_VehicleInventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with SQL Server
builder.Services.AddDbContext<DH_InventoryDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Application layer services
builder.Services.AddScoped<DH_IVehicleRepository, DH_VehicleRepository>();
builder.Services.AddScoped<DH_VehicleService>();
builder.Services.AddScoped<DH_CreateVehicleValidator>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "DH Vehicle Inventory API",
        Version = "v1",
        Description = "A RESTful API for managing vehicle inventory using Clean Architecture and DDD principles."
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DH Vehicle Inventory API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
