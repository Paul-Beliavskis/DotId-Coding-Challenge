using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvHelper;
using DotId.Domain.Entities;
using DotId.Persistence;
using DotId.Persistence.Exceptions;
using DotId.Persistence.Extensions;
using DotId.Persistence.Repositories;
using DotId.Persistence.Seeding.Interfaces;

namespace Repository.ImportData.SeedingData
{
    public class ScoreImportStrategy : IScoreImportStrategy
    {

        //this is declared in this file because we don't want any other file passed into this service
        private const string ImportFileName = "SEIFA_2011.csv";

        private readonly DotIdContext _dotIdContext;

        private readonly IQueryRepository _queryRepository;

        public ScoreImportStrategy(DotIdContext dotIdContext, IQueryRepository queryRepository)
        {
            _dotIdContext = dotIdContext;

            _queryRepository = queryRepository;
        }

        public void SeedToContext()
        {
            try
            {
                var importFileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"/Seeding/SeedData/{ImportFileName}";

                using (var stream = new FileStream(importFileLocation, FileMode.Open))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {

                    var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

                    csvReader.SkipRecords(1);

                    var stateName = string.Empty;

                    while (csvReader.Read())
                    {
                        csvReader.TryGetField<string>(0, out var stateValue);

                        stateName = string.IsNullOrWhiteSpace(stateValue) ? stateName : stateValue;

                        csvReader.TryGetField<string>(1, out var locationName);

                        if (!string.IsNullOrWhiteSpace(locationName))
                        {

                            var state = _dotIdContext.States.FirstOrDefault(x => x.StateName.Equals(stateName));

                            if (state == null && !string.IsNullOrWhiteSpace(stateName))
                            {

                                state = new State() { StateName = stateName };
                                _dotIdContext.States.Add(state);
                                _dotIdContext.SaveChanges();

                                state = _dotIdContext.States.FirstOrDefault(x => x.StateName == stateName);
                            }

                            var location = _dotIdContext.Locations.FirstOrDefault(x => x.PlaceName == locationName.Trim());

                            if (location != null && state != null)
                            {
                                location.State = state;
                            }

                            csvReader.TryGetField<int>(2, out var disadvantage);

                            csvReader.TryGetField<int>(3, out var advantage);

                            Score score = new Score()
                            {
                                DisadvantageScore = disadvantage,
                                AdvantageDisadvantageScore = advantage,
                                Year = 2011,
                                Location = location
                            };

                            _dotIdContext.Scores.Update(score);
                        }
                    }
                    _dotIdContext.SaveChanges();
                }

                PopulateMedian();
            }
            catch (Exception e)
            {
                throw new FailedToSeedException($"Seeding data from {ImportFileName} has failed", e);
            }
        }

        public void PopulateMedian()
        {
            var states = _dotIdContext.States.ToList();

            foreach (var state in states)
            {
                var scores = _queryRepository.GetScoresForState(state.StateName).ToList();

                state.Median = scores.GetMedian();
            }

            _dotIdContext.SaveChanges();
        }
    }
}
