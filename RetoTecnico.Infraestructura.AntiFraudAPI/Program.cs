using Microsoft.EntityFrameworkCore;

using RetoTecnico.Aplicacion.CasoUso;
using RetoTecnico.Aplicacion.Interfaces.Repository;
using RetoTecnico.Aplicacion.Interfaces.Service;
using RetoTecnico.Aplicacion.Mapper;
using RetoTecnico.Infraestructura.Kafka.Adapter;
using RetoTecnico.Infraestructura.PostgreSql.Contextos;
using RetoTecnico.Infraestructura.PostgreSql.Repositorios;

var builder = WebApplication.CreateBuilder(args);
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(TransactionProfile));
builder.Services.AddDbContext<NpgsqlContext>(options => options.UseNpgsql(connStr));

// Add services to the container.
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddHostedService<ConsumerKafkaAdapter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
