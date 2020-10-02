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

        public async Task<IEnumerable<ReportResult>> GetReportDataAsync()
        {
            IEnumerable<ReportResult> reportResultList = new List<ReportResult>();

            using (var connection = new SqlConnection(SqlConnectionString))
            {
                reportResultList = await connection.QueryAsync<ReportResult>("SELECT StateName, PlaceName from Locations Inner Join States ON States.StateId = Locations.StateId");
            }

            return reportResultList;
        }
    }
}
