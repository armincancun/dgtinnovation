using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.VehicleBL;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infrastructure.Core;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class VehicleController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Vehicle>>> GetVehicle()
        {
            return await Mediator.Send(new GetVehicle.VehiclesList());
        }
        
        [HttpPost]
        public async Task<ActionResult<Unit>> AddVehicle(AddVehicle.Data data)
        {
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DelVehicle(Guid id)
        {
            return await Mediator.Send(new DelVehicle.Data { Id = id });
        }
    }
}
