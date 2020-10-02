using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using DotId.Domain.Entities;
using DotId.Persistence.Extensions;
using DotId.Persistence.Seeding.Interfaces;

namespace DotId.Persistence.Seeding.Services
{
    public class LocationImportStrategy : IImportStrategy
    {
        //this is declared in this file because we don't want any other file passed into this service
        private const string ImportFileName = "SEIFA_2016.csv";

        public async Task SeedToContextAsync(DotIdContext seedingContext)
        {
            seedingContext.Database.EnsureCreated();

            var importFileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/Seeding/SeedData/{ImportFileName}";
            try
            {

                using (var stream = new FileStream(importFileLocation, FileMode.Open))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {

                    var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                    await csvReader.SkipRecordsAsync(6);

                    while (csvReader.Read())
                    {

                        csvReader.TryGetField<int>(0, out int locationCode);

                        csvReader.TryGetField<string>(1, out var locationName);

                        if (!string.IsNullOrWhiteSpace(locationName))
                        {
                            var location = new Location() { PlaceName = locationName, Code = locationCode, State = null };

                            seedingContext.Locations.Add(location);
                            csvReader.TryGetField<int>(2, out var disadvantage);
                            csvReader.TryGetField<int>(4, out var advantage);

                            var score = new Score()
                            {
                                DisadvantageScore = disadvantage,
                                AdvantageDisadvantageScore = advantage,
                                //Todo: this should not be hard coded
                                Year = 2016,
                                Location = location
                            };

                            seedingContext.Scores.Add(score);

                            csvReader.TryGetField<int>(3, out var disadvantageDecile);
                            csvReader.TryGetField<int>(5, out var advantageDecile);

                            csvReader.TryGetField<int>(6, out var indexOfEconomicResourcesScore);
                            csvReader.TryGetField<int>(7, out var indexOfEconomicResourcesDecile);
                            csvReader.TryGetField<int>(8, out var indexOfEducationAndOccupationScore);
                            csvReader.TryGetField<int>(9, out var indexOfEducationAndOccupationDecile);
                            csvReader.TryGetField<decimal>(10, out var usualResedantPopulation);

                            ScoreDetail scoreDetail = new ScoreDetail()
                            {
                                Score = score,
                                DisadvantageDecile = disadvantageDecile,
                                AdvantageDisadvantageDecile = advantageDecile,
                                IndexOfEconomicResourcesScore = indexOfEconomicResourcesScore,
                                IndexOfEconomicResourcesDecile = indexOfEconomicResourcesDecile,
                                IndexOfEducationAndOccupationScore = indexOfEducationAndOccupationScore,
                                IndexOfEducationAndOccupationDecile = indexOfEducationAndOccupationDecile,
                                UsualResedantPopulation = usualResedantPopulation
                            };

                            scoreDetail.Score = score;
                            seedingContext.ScoreDetails.Add(scoreDetail);
                        }
                    }

                }
            }
            // Todo: Create exception class to be caught and logged the right logging
            catch (Exception e)
            {
            }
        }
    }
}
