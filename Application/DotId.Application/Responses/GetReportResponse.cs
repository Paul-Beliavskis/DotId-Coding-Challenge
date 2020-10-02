using System.Collections.Generic;
using DotId.Application.Models;

namespace DotId.Application.Responses
{
    public class GetReportResponse
    {
        public IEnumerable<ReportModel> ReportModels { get; set; }
    }
}
