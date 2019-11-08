using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Service.Features.Test.Index;

namespace Service.Features.Test
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IndexResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(IndexRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

       
        [HttpGet]
        [ProducesResponseType(typeof(IndexResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index_V2(IndexRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}