using DotId.Persistence.Seeding.Interfaces;

namespace DotId.Persistence.Services
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ILocationImportStrategy _locationImportStrategy;

        private readonly IScoreImportStrategy _scoreImportStrategy;

        public DataSeeder(ILocationImportStrategy locationImportStrategy, IScoreImportStrategy scoreImportStrategy)
        {

            _scoreImportStrategy = scoreImportStrategy;

            _locationImportStrategy = locationImportStrategy;

        }

        public void SeedData()
        {
            _locationImportStrategy.SeedToContext();

            _scoreImportStrategy.SeedToContext();
        }
    }
}
