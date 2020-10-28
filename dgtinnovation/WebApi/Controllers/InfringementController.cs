using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.InfringementBL;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infrastructure.Core;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class InfringementController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Infringement>>> GetInfringement()
        {
            return await Mediator.Send(new GetInfringement.InfringementList());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> AddInfringement(AddInfringement.Data data)
        {
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DelInfringement(Guid id)
        {
            return await Mediator.Send(new DelInfringement.Data { Id = id });
        }
    }
}
