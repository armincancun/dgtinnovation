using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using Domain;
using System.Threading.Tasks;
using System.Threading;
using Application.ErrorHandler;
using System.Linq;
using System.Net;

namespace Application.DriverVehicleBL
{
    public class AddDriverVehicle
    {
        public class Data : IRequest
        {
            public Guid Id { get; set; }
            public Guid DriverId { get; set; }
            public Guid VehicleId { get; set; }
        }

        public class Handler : IRequestHandler<Data>
        {
            private readonly IDriverVehicleRepository _driverVehicleRepository;
            private readonly IDriverRepository _driverRepository;
            private readonly IVehiclesRepository _vehiclesRepository;

            public Handler(IDriverVehicleRepository driverVehicleRepository, IDriverRepository driverRepository, IVehiclesRepository vehiclesRepository)
            {
                _driverVehicleRepository = driverVehicleRepository;
                _driverRepository = driverRepository;
                _vehiclesRepository = vehiclesRepository;
            }

            public async Task<Unit> Handle(Data request, CancellationToken cancellationToken)
            {
                Guid _Id = Guid.NewGuid();
                var record = new DriverVehicle
                {
                    Id = _Id,
                    DriverId = request.DriverId,
                    VehicleId = request.VehicleId
                };

                var findDriver = await _driverRepository.FindBy(f => f.Id == record.DriverId);
                if (!findDriver.Any())
                {
                    throw new ExceptionHandler(HttpStatusCode.Conflict, new { mensaje = $"Driver does not exist: {record.DriverId}." });
                }

                var findVehicle = await _vehiclesRepository.FindBy(f => f.Id == record.VehicleId);
                if (!findVehicle.Any())
                {
                    throw new ExceptionHandler(HttpStatusCode.Conflict, new { mensaje = $"Vehicle does not exist: {record.VehicleId}." });
                }

                var findDuplicated = await _driverVehicleRepository.FindBy(f =>
                    f.DriverId == record.DriverId && f.VehicleId == record.VehicleId);
                if (findDuplicated != null & findDuplicated.Count() > 0)
                {
                    throw new ExceptionHandler(HttpStatusCode.Conflict, new { mensaje = $"Record exist: {record.DriverId} - {record.VehicleId}." });
                }

                var findCount = await _driverVehicleRepository.FindBy(f => f.DriverId == record.DriverId);
                if (findCount.Count > 10)
                {
                    throw new ExceptionHandler(HttpStatusCode.Conflict, new { mensaje = $"Driver cannot be habitual of more than 10 vehicles. {record.DriverId}" });
                }


                _driverVehicleRepository.Add(record);
                var value = await _driverVehicleRepository.Commit();
                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("The record was not inserted");
            }
        }
    }
}
