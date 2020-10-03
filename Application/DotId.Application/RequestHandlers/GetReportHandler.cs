using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotId.Application.Models;
using DotId.Application.Requests;
using DotId.Application.Responses;
using DotId.Persistence;
using DotId.Persistence.DTO;
using DotId.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.Options;

namespace DotId.Application.RequestHandlers
{
    public class GetReportHandler : IRequestHandler<GetReportRequest, GetReportResponse>
    {
        private readonly DotIdContext _dotIdContext;

        private readonly IQueryRepository _queryRepository;

        public GetReportHandler(DotIdContext dotIdContext, IQueryRepository queryRepository, IOptions<ConnectionStrings> options)
        {
            _dotIdContext = dotIdContext;

            _queryRepository = queryRepository;
        }

        public async Task<GetReportResponse> Handle(GetReportRequest request, CancellationToken cancellationToken)
        {
            var state = _dotIdContext.States.FirstOrDefault(x => x.StateId == request.StateId);

            var data = await _queryRepository.GetReportDataAsync(request.StateId, state.Median);

            var year2016 = data.Where(x => x.Year == 2016).ToList();
            var year2011 = data.Where(x => x.Year == 2011);

            var reportList = new List<ReportModel>();

            foreach (var item in year2011)
            {
                var year2016Data = year2016.FirstOrDefault(x => x.PlaceName.Equals(item.PlaceName));

                if (year2016Data != null)
                {
                    year2016.Remove(year2016Data);
                }

                var reportItem = new ReportModel()
                {
                    Disadvantage2011 = item.DisadvantageScore,
                    Disadvantage2016 = year2016Data?.DisadvantageScore,
                    PlaceName = item.PlaceName,
                    StateName = item.StateName

                };

                reportList.Add(reportItem);
            }

            foreach (var item in year2016)
            {
                var reportItem = new ReportModel()
                {
                    Disadvantage2016 = item.DisadvantageScore,
                    PlaceName = item.PlaceName,
                    StateName = item.StateName

                };

                reportList.Add(reportItem);
            }

            return new GetReportResponse() { ReportModels = reportList };
        }
    }
}
