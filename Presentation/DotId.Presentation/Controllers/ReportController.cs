using System.Threading.Tasks;
using DotId.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotId.Presentation.Controllers
{
    public class ReportController : Controller
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: ReportController
        public async Task<ActionResult> Index()
        {
            var request = new GetReportRequest();

            var result = await _mediator.Send(request);

            return View();
        }
    }
}
