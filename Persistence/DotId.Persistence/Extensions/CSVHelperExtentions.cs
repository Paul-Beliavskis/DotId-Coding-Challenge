using System.Threading.Tasks;
using CsvHelper;

namespace DotId.Persistence.Extensions
{
    public static class CSVHelperExtentions
    {
        public static async Task SkipRecordsAsync(this CsvReader csvReader, int recordsToSkip)
        {
            for (var i = 0; i < recordsToSkip; ++i)
            {
                await csvReader.ReadAsync();
            }
        }
    }
}
