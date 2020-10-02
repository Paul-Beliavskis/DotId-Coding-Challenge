using System;
using System.Threading.Tasks;
using DotId.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DotId.Persistence.Tests.Unit.Seeding.Services
{
    public class DataSeederTests
    {
        private readonly DbContextOptions<DotIdContext> _options;

        private readonly IDataSeeder _dataSeeder;

        public DataSeederTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            _options = new DbContextOptionsBuilder<DotIdContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString())
                     .UseInternalServiceProvider(serviceProvider)
                     .Options;
            var context = new DotIdContext(_options);

            _dataSeeder = new DataSeeder(context);
        }

        [Fact]
        public async Task SeedData_DoesDataSeed_SeedSuccess()
        {
            var context = new DotIdContext(_options);

            await _dataSeeder.SeedDataAsync(context);
        }
    }
}
