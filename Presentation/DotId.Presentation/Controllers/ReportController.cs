using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotId.Application.Requests;
using DotId.Persistence;
using DotId.Presentation.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotId.Presentation.Controllers
{
    public class ReportController : Controller
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        private readonly DotIdContext _dotIdContext;

        public ReportController(IMediator mediator, IMapper mapper, DotIdContext dotIdContext)
        {
            _mediator = mediator;

            _mapper = mapper;

            _dotIdContext = dotIdContext;
        }

        // GET: ReportController
        public async Task<ActionResult> Index()
        {
            var viewModel = await GetReportModelsAsync();

            ViewData["States"] = _dotIdContext.States.Distinct().ToList().Select(p => new SelectListItem()
            {
                Value = p.StateId.ToString(),
                Text = p.StateName,
                Selected = (p.StateName.Equals("Victoria", StringComparison.CurrentCultureIgnoreCase))
            })
                .ToList();

            return View(viewModel);
        }

        public async Task<ActionResult> ReportGrid(int stateId = 2)
        {
            var viewModel = await GetReportModelsAsync(stateId);

            return View(viewModel);
        }

        private async Task<List<ReportModel>> GetReportModelsAsync(int stateId = 2)
        {
            var request = new GetReportRequest()
            {
                StateId = stateId
            };

            var result = await _mediator.Send(request);
            var viewModel = _mapper.Map<List<ReportModel>>(result.ReportModels);

            return viewModel;
        }
    }
}
