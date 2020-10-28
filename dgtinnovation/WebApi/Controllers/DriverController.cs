using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DriverBL;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infrastructure.Core;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class DriverController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Driver>>> GetDriver()
        {
            return await Mediator.Send(new GetDriver.DriverList());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> AddDriver(AddDriver.Data data)
        {
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DelDriver(Guid id)
        {
            return await Mediator.Send(new DelDriver.Data{Id = id});
        }

    }
}
