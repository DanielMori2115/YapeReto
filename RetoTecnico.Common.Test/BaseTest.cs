using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

using NSubstitute;
using RetoTecnico.Infraestructura.PostgreSql.Contextos;

namespace RetoTecnico.Common.Test
{
    public class BaseTest
    {
        protected static async Task<TestingDataContext> GetMockContext(string database)
        {
            var options = new DbContextOptionsBuilder<NpgsqlContext>()
                .UseInMemoryDatabase(databaseName: database).Options;

            var interceptorMock = Substitute.For<ISaveChangesInterceptor>();
            var context = new TestingDataContext(options);

            await context.Database.EnsureCreatedAsync();

            return context;
        }
    }
}
