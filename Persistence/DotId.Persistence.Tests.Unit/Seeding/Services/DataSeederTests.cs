using System;
using System.Threading.Tasks;
using DotId.Persistence.Seeding.Services;
using DotId.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.ImportData.SeedingData;
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

            var locationSeeder = new LocationImportStrategy(context);
            var scoreSeeder = new ScoreImportStrategy(context);

            _dataSeeder = new DataSeeder(locationSeeder, scoreSeeder);
        }

        [Fact]
        public async Task SeedData_DoesDataSeed_SeedSuccess()
        {
            _dataSeeder.SeedData();
        }
    }
}
