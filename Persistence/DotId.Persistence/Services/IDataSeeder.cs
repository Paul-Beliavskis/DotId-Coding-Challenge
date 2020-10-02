using System.Threading.Tasks;

namespace DotId.Persistence.Services
{
    public interface IDataSeeder
    {
        public Task SeedDataAsync();
    }
}
