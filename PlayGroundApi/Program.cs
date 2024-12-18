using Microsoft.Extensions.DependencyInjection;
using PlayGroundLibForberedelse;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Tilf�jer support til at udforske API-endepunkter
builder.Services.AddEndpointsApiExplorer();
// Tilf�jer og konfigurerer Swagger til at generere API-dokumentation
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                              });
});


// Tilf�jer en singleton service for PlayGroundsRepository
builder.Services.AddSingleton<PlayGroundsRepository>(new PlayGroundsRepository());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger(); // Aktiverer middleware til at generere Swagger-dokumentation
app.UseSwaggerUI(); // Aktiverer Swagger UI til at udforske og teste API-endepunkter

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Fort�ller appen at den skal bruge Cors med navnet "AllowAll"
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
