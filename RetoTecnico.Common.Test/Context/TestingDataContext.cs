using Microsoft.EntityFrameworkCore;

using RetoTecnico.Infraestructura.PostgreSql.Contextos;

namespace RetoTecnico.Common.Test.Context;

public class TestingDataContext(DbContextOptions<NpgsqlContext> options) : NpgsqlContext(options);
