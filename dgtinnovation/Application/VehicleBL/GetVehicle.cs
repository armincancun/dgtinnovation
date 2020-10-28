using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using Domain;
using System.Threading.Tasks;
using System.Threading;

namespace Application.VehicleBL
{
    public class GetVehicle
    {
        public class VehiclesList : IRequest<List<Vehicle>> { }

        public class Handler : IRequestHandler<VehiclesList, List<Vehicle>>
        {
            private readonly IVehiclesRepository _vehiclesRepository;
            public Handler(IVehiclesRepository driverRepository)
            {
                _vehiclesRepository = driverRepository;
            }

            public async Task<List<Vehicle>> Handle(VehiclesList request, CancellationToken cancellationToken)
            {
                var vehicles = await _vehiclesRepository.GetAll();

                return vehicles;
            }

        }
    }
}
