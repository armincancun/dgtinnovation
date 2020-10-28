using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.ErrorHandler;
using DataAccess.Abstract;
using DataAccess.Repositories;
using Domain;
using MediatR;

namespace Application.VehicleBL
{
    public class AddVehicle
    {
        public class Data: IRequest
        {
            public Guid Id { get; set; }
            public string Enrollment { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public Guid RegularDriver { get; set; }
            public bool Status { get; set; }
            public List<Guid> DriverList { get; set; }
        }

        public class Handler : IRequestHandler<Data>
        {
            private readonly IDriverRepository _driverRepository;
            private readonly IDriverVehicleRepository _driverVehicleRepository;
            private readonly IVehiclesRepository _vehiclesRepository;

            public Handler(IVehiclesRepository vehiclesRepository, IDriverRepository driverRepository, IDriverVehicleRepository driverVehicleRepository)
            {
                _driverRepository = driverRepository;
                _driverVehicleRepository = driverVehicleRepository;
                _vehiclesRepository = vehiclesRepository;
            }

            public async Task<Unit> Handle(Data request, CancellationToken cancellationToken)
            {
                Guid _Id = Guid.NewGuid();
                var vehicle = new Vehicle
                {
                    Id = _Id,
                    Enrollment = request.Enrollment,
                    Brand = request.Brand,
                    Model = request.Model,
                    RegularDriver = request.RegularDriver,
                    Status = true
                };
               
                var findEnrollment = await _vehiclesRepository.FindBy(f => f.Enrollment == vehicle.Enrollment);
                if (findEnrollment != null & findEnrollment.Count > 0)
                {
                    throw new ExceptionHandler(HttpStatusCode.Conflict, new { mensaje = "Enrollment existing in the database" });
                }

                _vehiclesRepository.Add(vehicle);

                if (request.DriverList != null)
                {
                    foreach (var id in request.DriverList)
                    {
                        var findDriver = await _driverRepository.FindBy(f => f.Id == id);
                        if (!findDriver.Any())
                        {
                            throw new ExceptionHandler(HttpStatusCode.Conflict, new { mensaje = $"Driver does not exist: {id}." });
                        }

                        var driverVehicle = new DriverVehicle
                        {
                            Id = Guid.NewGuid(),
                            DriverId = id,
                            VehicleId = vehicle.Id
                        };
                        _driverVehicleRepository.Add(driverVehicle);
                    }
                }

                var value = await _vehiclesRepository.Commit();
                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("The record was not inserted");
            }
        }
    }

    
}
