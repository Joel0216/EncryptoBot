using WebAPI.Services; // Asegúrate de que el namespace sea correcto

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 🔐 Registrar el servicio César
builder.Services.AddScoped<ICesarService, CesarService>();

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
