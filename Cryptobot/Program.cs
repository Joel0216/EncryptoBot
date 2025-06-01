// CryptoBot/Program.cs
using CryptoBot.Services.Features.Crypto;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios
builder.Services.AddScoped<CaesarCipherService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "CryptoBot API",
        Description = "Una API inteligente para encriptar y desencriptar mensajes con cifrado César",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Tu Nombre",
            Url = new Uri("https://github.com/tuusuario")
        }
    });
});

var app = builder.Build();

// Configurar pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoBot API V1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
