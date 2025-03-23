using Gis.Api.Data;
using Gis.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// MongoDB Konekcija
builder.Services.AddSingleton<MongoDbContext>(); // Konekcija na MongoDB
builder.Services.AddScoped<UcenikRepository>(); // Repository za rad s podacima

// Swagger konfiguracija
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapiranje kontrolera
app.MapControllers();

app.Run();
