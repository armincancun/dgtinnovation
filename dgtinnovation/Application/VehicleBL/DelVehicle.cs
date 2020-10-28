using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using System.Threading.Tasks;
using System.Threading;

namespace Application.VehicleBL
{
    public class DelVehicle
    {
        public class Data : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Data>
        {
            private readonly IVehiclesRepository _vehiclesRepository;

            public Handler(IVehiclesRepository driverRepository)
            {
                _vehiclesRepository = driverRepository;
            }

            public async Task<Unit> Handle(Data request, CancellationToken cancellationToken)
            {
                var vehicle = _vehiclesRepository.GetSingle(request.Id);
                if (vehicle == null)
                {
                    throw new Exception("no record found");
                }
                _vehiclesRepository.Delete(vehicle);

                var result = await _vehiclesRepository.Commit();
                if (result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Records were not saved");
            }
        }
    }
}
