using Microsoft.EntityFrameworkCore;
using Mottu.Rentals.Api.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar controllers
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro de serviços e repositórios
// builder.Services.AddScoped<IMotoRepository, MotoRepository>();
// builder.Services.AddScoped<IEntregadorRepository, EntregadorRepository>();

var app = builder.Build();

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
