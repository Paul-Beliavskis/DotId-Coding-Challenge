using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotId.Application.Requests;
using DotId.Application.Responses;
using DotId.Persistence;
using DotId.Persistence.Repositories;
using MediatR;

namespace DotId.Application.RequestHandlers
{
    public class GetReportHandler : IRequestHandler<GetReportRequest, GetReportResponse>
    {
        private readonly DotIdContext _dotIdContext;

        private readonly IQueryRepository _queryRepository;

        public GetReportHandler(DotIdContext dotIdContext, IQueryRepository queryRepository)
        {
            _dotIdContext = dotIdContext;
        }

        public Task<GetReportResponse> Handle(GetReportRequest request, CancellationToken cancellationToken)
        {

            var reportList = _dotIdContext.Locations.Where(x => x.State.StateId == request.StateId).ToList();


            throw new System.NotImplementedException();
        }
    }
}
