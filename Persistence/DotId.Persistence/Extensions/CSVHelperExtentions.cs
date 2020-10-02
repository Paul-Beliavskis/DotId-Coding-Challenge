using CsvHelper;

namespace DotId.Persistence.Extensions
{
    public static class CSVHelperExtentions
    {
        public static void SkipRecords(this CsvReader csvReader, int recordsToSkip)
        {
            for (var i = 0; i < recordsToSkip; ++i)
            {
                csvReader.Read();
            }
        }
    }
}
