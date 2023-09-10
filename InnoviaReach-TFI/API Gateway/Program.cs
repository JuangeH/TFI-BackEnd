var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd", builder =>
    {
        builder.WithOrigins("https://tufrontend.com")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

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

// Habilitar CORS
app.UseCors("AllowFrontEnd");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Configurar rutas
app.MapGet("/api/{**catchall}", async (HttpContext context) =>
{
    var backendBaseUrl = "https://localhost:44308"; // Cambia esto a la URL de tu API BackEnd
    var httpClient = new HttpClient();

    // Configura la solicitud al backend, por ejemplo, establece encabezados de autenticación
    // httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer tu-token");

    var response = await httpClient.GetAsync(backendBaseUrl + context.Request.Path);

    // Maneja la respuesta del backend y envíala de vuelta al cliente
    context.Response.StatusCode = (int)response.StatusCode;

    foreach (var header in response.Headers)
    {
        context.Response.Headers[header.Key] = header.Value.ToArray();
    }

    var content = await response.Content.ReadAsStringAsync();
    await context.Response.WriteAsync(content);
});

app.Run();
