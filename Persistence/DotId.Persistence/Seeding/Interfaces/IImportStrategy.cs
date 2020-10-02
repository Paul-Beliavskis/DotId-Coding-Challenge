using System.Threading.Tasks;

namespace DotId.Persistence.Seeding.Interfaces
{
    public interface IImportStrategy
    {
        Task SeedToContextAsync(DotIdContext seedingContext);
    }
}
