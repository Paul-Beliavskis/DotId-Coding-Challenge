using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using DotId.Domain.Entities;
using DotId.Persistence;
using DotId.Persistence.Exceptions;
using DotId.Persistence.Extensions;
using DotId.Persistence.Seeding.Interfaces;

namespace Repository.ImportData.SeedingData
{
    public class ScoreImportStrategy : IImportStrategy
    {

        //this is declared in this file because we don't want any other file passed into this service
        private const string ImportFileName = "SEIFA_2011.csv";

        public async Task SeedToContextAsync(DotIdContext context)
        {
            try
            {
                var importFileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/Seeding/SeedData/{ImportFileName}";

                using (var stream = new FileStream(importFileLocation, FileMode.Open))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {

                    var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

                    await csvReader.SkipRecordsAsync(1);

                    var state = new State();
                    while (csvReader.Read())
                    {
                        csvReader.TryGetField<string>(0, out var stateName);

                        csvReader.TryGetField<string>(1, out var locationName);

                        if (!string.IsNullOrWhiteSpace(locationName))
                        {
                            if (!string.IsNullOrWhiteSpace(stateName))
                            {
                                state = new State() { StateName = stateName };
                                context.States.Add(state);
                            }

                            Location location = new Location() { PlaceName = locationName, State = state };
                            context.Locations.Update(location);

                            csvReader.TryGetField<int>(2, out var disadvantage);

                            csvReader.TryGetField<int>(3, out var advantage);

                            Score score = new Score()
                            {
                                DisadvantageScore = disadvantage,
                                AdvantageDisadvantageScore = advantage,
                                Year = 2011,
                                Location = location
                            };

                            context.Scores.Update(score);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                throw new FailedToSeedException($"Seeding data from {ImportFileName} has failed", e);
            }
        }
    }
}
