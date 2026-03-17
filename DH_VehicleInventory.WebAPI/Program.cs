using DH_VehicleInventory.Infrastructure.Data;
using DH_VehicleInventory.Domain.VehicleAggregate;
using DH_VehicleInventory.Infrastructure.Repositories;
using DH_VehicleInventory.Application.Interfaces;
using DH_VehicleInventory.Application.Services;
using DH_VehicleInventory.Application.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DH_InventoryDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

builder.Services.AddScoped<DH_VehicleService>();
builder.Services.AddScoped<DH_CreateVehicleValidator>();

builder.Services.AddControllers();
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