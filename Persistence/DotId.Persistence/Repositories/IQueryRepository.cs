using System.Collections.Generic;
using System.Threading.Tasks;
using DotId.Persistence.DTO;

namespace DotId.Persistence.Repositories
{
    public interface IQueryRepository
    {
        Task<IEnumerable<ReportResult>> GetReportDataAsync();
    }
}
