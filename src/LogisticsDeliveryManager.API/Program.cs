using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

// OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer(); // necessário para gerar o documento
//builder.Services.AddOpenApi();              // gera a especificação OpenAPI

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Expondo o documento OpenAPI (JSON/YAML)
    //app.MapOpenApi();
    app.UseSwagger();

    // Swagger UI (requer o pacote Microsoft.AspNetCore.SwaggerUI)
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Logistics Delivery Manager API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
