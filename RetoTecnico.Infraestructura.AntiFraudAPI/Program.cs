using Microsoft.EntityFrameworkCore;

using RetoTecnico.Aplicacion.AntiFraud.CasoUso;
using RetoTecnico.Aplicacion.AntiFraud.Mapper;

using RetoTecnico.Dominio.Interfaces.Repositorios;

using RetoTecnico.Infraestructura.Kafka.Adapter;
using RetoTecnico.Infraestructura.PostgreSql.Contextos;
using RetoTecnico.Infraestructura.PostgreSql.Repositorios;

var builder = WebApplication.CreateBuilder(args);
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AntiFraudProfile));
builder.Services.AddDbContext<TransactionContext>(options => options.UseNpgsql(connStr));

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