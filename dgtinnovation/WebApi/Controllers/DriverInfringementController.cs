using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DriverBL;
using Application.DriverInfringementBL;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infrastructure.Core;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class DriverInfringementController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<DriverInfringement>>> GetDriverInfringement()
        {
            return await Mediator.Send(new GetDriverInfringement.InfringementList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<DriverInfringement>>> GetDriverInfringement(Guid id)
        {
            return await Mediator.Send(new GetDriverInfringementById.Data{ DriverId = id });
        }

        [HttpGet("top5")]
        public async Task<ActionResult<List<InfringementTop5DTO>>> GetDriverInfringementTop5()
        {
            return await Mediator.Send(new GetDriverInfringementTop5.Data());
        }

        [HttpGet("topdriver/{num}")]
        public async Task<ActionResult<List<DriverTop5DTO>>> GetDriverInfringementTopDriver(int num)
        {
            return await Mediator.Send(new GetDriverInfringementTopDriver.Data{Number = num});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> AddDriverInfringement(AddDriverInfringement.Data data)
        {
            return await Mediator.Send(data);
        }
    }
}
