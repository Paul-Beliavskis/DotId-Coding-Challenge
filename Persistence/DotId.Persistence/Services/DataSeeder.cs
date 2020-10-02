using System.Threading.Tasks;
using DotId.Persistence.Seeding.Services;
using Repository.ImportData.SeedingData;

namespace DotId.Persistence.Services
{
    public class DataSeeder : IDataSeeder
    {
        private readonly DotIdContext _dotIdContext;

        public DataSeeder(DotIdContext context)
        {
            _dotIdContext = context;
        }

        public async Task SeedDataAsync(DotIdContext context)
        {
            context.Database.EnsureCreated();

            var locationImportStrategy = new LocationImportStrategy();
            var scoreImportStrategy = new ScoreImportStrategy();

            await locationImportStrategy.SeedToContextAsync(context);

            context.SaveChanges();

            await scoreImportStrategy.SeedToContextAsync(context);

            context.SaveChanges();
        }
    }
}
