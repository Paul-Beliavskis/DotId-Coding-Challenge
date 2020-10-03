using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DotId.Persistence.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DotId.Persistence.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        private readonly string SqlConnectionString;

        public QueryRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            SqlConnectionString = connectionStrings?.Value?.SqlServer;
        }

        public async Task<IEnumerable<ReportResult>> GetReportDataAsync(int stateId, decimal minScore)
        {
            IEnumerable<ReportResult> reportResultList = new List<ReportResult>();

            using (var connection = new SqlConnection(SqlConnectionString))
            {
                reportResultList = await connection.QueryAsync<ReportResult>($"SELECT PlaceName, StateName, DisadvantageScore, AdvantageDisadvantageScore, Year, Locations.StateId from Locations Inner Join States ON States.StateId = Locations.StateId Inner Join Scores ON Scores.LocationId = Locations.LocationId where Locations.StateId = {stateId} AND DisadvantageScore > {minScore}");
            }

            return reportResultList;
        }

        public IEnumerable<int> GetScoresForState(string stateName)
        {
            IEnumerable<int> reportResultList = new List<int>();

            using (var connection = new SqlConnection(SqlConnectionString))
            {
                reportResultList = connection.Query<int>($"SELECT DisadvantageScore FROM [DotId].[dbo].[Scores]  Inner Join Locations on Locations.LocationId = Scores.LocationId Full Outer Join States on States.StateId = Locations.StateId where StateName = '{stateName}'");
            }

            return reportResultList;
        }
    }
}
