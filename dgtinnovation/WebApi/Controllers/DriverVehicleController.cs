using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DriverVehicleBL;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infrastructure.Core;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class DriverVehicleController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> AddDriver(AddDriverVehicle.Data data)
        {
            return await Mediator.Send(data);
        }
    }
}
