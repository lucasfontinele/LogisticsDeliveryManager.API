using LogisticsDeliveryManager.Api.Filters;
using LogisticsDeliveryManager.Domain.Services.Customers;
using LogisticsDeliveryManager.Domain.Services.Vehicles;
using LogisticsDeliveryManager.Application.UseCases.Customers.CreateCustomer;
using LogisticsDeliveryManager.Application.UseCases.Vehicles.CreateVehicle;
using LogisticsDeliveryManager.Infrastructure;
using Microsoft.OpenApi.Models;
using LogisticsDeliveryManager.API.Runners;

using LogisticsDeliveryManager.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddScoped<ICustomerDomainService, CustomerDomainService>();
builder.Services.AddScoped<IVehicleDomainService, VehicleDomainService>();
builder.Services.AddScoped<LogisticsDeliveryManager.Domain.Services.Employees.IEmployeeDomainService, LogisticsDeliveryManager.Domain.Services.Employees.EmployeeDomainService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Logistics Delivery Manager API",
        Version = "v1",
        Description = "Documentacao da API de gerenciamento logistico."
    });
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddHostedService<ApplicationStartupLogger>();

var app = builder.Build();

app.Services.InitializeDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Logistics Delivery Manager API v1");
        options.RoutePrefix = "swagger";
        options.DocumentTitle = "Logistics Delivery Manager API Docs";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
