using DotId.Application.Responses;
using MediatR;

namespace DotId.Application.Requests
{
    public class GetReportRequest : IRequest<GetReportResponse>
    {
        public int StateId { get; set; }
    }
}
