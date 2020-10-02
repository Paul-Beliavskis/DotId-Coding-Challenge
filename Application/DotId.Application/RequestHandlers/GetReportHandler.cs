using System.Threading;
using System.Threading.Tasks;
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

            var reportList = await _queryRepository.GetReportDataAsync();


            throw new System.NotImplementedException();
        }
    }
}
