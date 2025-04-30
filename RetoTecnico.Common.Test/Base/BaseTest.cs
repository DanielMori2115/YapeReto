using Microsoft.EntityFrameworkCore;

using RetoTecnico.Infraestructura.PostgreSql.Contextos;
using RetoTecnico.Common.Test.Context;
using AutoMapper;
using RetoTecnico.Aplicacion.Mapper;

namespace RetoTecnico.Common.Test.Base;

public class BaseTest
{
    protected IMapper Mapper;

    protected void InitMapper()
    {
        var myProfile = new TransactionProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

        Mapper = new Mapper(configuration);
    }

    protected static async Task<TestingDataContext> GetMockContext(string database)
    {
        var options = new DbContextOptionsBuilder<NpgsqlContext>()
            .UseInMemoryDatabase(databaseName: database).Options;

        var context = new TestingDataContext(options);

        await context.Database.EnsureCreatedAsync();

        return context;
    }
}
