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

namespace Application.DriverBL
{
    public class AddDriver
    {
        public class Data : IRequest
        {
            public int Id { get; set; }
            public string DNI { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int TotalPoints { get; set; }
            public int LostPoints { get; set; }
            public bool Status { get; set; }
            public List<Guid> VehiclesList { get; set; }

        }

        public class Handler : IRequestHandler<Data>
        {
            private readonly IDriverRepository _driverRepository;
            private readonly IDriverVehicleRepository _driverVehicleRepository;
            private readonly IVehiclesRepository _vehiclesRepository;
            public Handler(IDriverRepository driverRepository, IDriverVehicleRepository driverVehicleRepository, IVehiclesRepository vehiclesRepository)
            {
                _driverRepository = driverRepository;
                _driverVehicleRepository = driverVehicleRepository;
                _vehiclesRepository = vehiclesRepository;
            }
            public async Task<Unit> Handle(Data request, CancellationToken cancellationToken)
            {
                Guid _Id = Guid.NewGuid();
                var driver = new Driver
                {
                    Id = _Id,
                    DNI = request.DNI,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    TotalPoints = request.TotalPoints,
                    LostPoints = 0,
                    Status = true
                };

                var findDNI = await _driverRepository.FindBy(f => f.DNI == driver.DNI);
                if (findDNI != null & findDNI.Count > 0)
                {
                    throw new ExceptionHandler(HttpStatusCode.Conflict, new { mensaje = "DNI existing in the database" });
                }

                _driverRepository.Add(driver);

                if (request.VehiclesList != null)
                {
                    if (request.VehiclesList.Count > 10)
                    {
                        throw new ExceptionHandler(HttpStatusCode.Conflict, new { mensaje = "Driver cannot be habitual of more than 10 vehicles." });
                    }

                    foreach (var id in request.VehiclesList)
                    {
                        var findVehicle = await _vehiclesRepository.FindBy(f => f.Id == id);
                        if (!findVehicle.Any())
                        {
                            throw new ExceptionHandler(HttpStatusCode.Conflict, new { mensaje = $"Vehicle does not exist: {id}." });
                        }

                        var driverVehicle = new DriverVehicle
                        {
                            Id = Guid.NewGuid(),
                            DriverId = driver.Id,
                            VehicleId = id
                        };
                        _driverVehicleRepository.Add(driverVehicle);
                    }
                }

                var value = await _driverRepository.Commit();
                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("The record was not inserted");

            }
        }

    }
}
