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

        public async Task<IEnumerable<ReportResult>> GetReport()
        {
            using (var connection = new SqlConnection(SqlConnectionString))
            {
                var reportResultList = await connection.QueryAsync<ReportResult>("SELECT * FROM \"Users\"");
            }
            return null;
        }
    }
}
