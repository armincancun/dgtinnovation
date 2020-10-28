using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using Domain;
using System.Threading.Tasks;
using System.Threading;
using Application.ErrorHandler;
using System.Net;
using System.Linq;

namespace Application.DriverInfringementBL
{
    public class AddDriverInfringement
    {
        public class Data : IRequest
        {
            public Guid Id { get; set; }
            public DateTime DateCreated { get; set; }
            public Guid InfringementId { get; set; }
            public Guid DriverId { get; set; }
            public int PointDiscount { get; set; }
            public bool Status { get; set; }
        }

        public class Handler : IRequestHandler<Data>
        {
            private readonly IDriverInfringementRepository _driverInfringementRepository;
            private readonly IDriverRepository _driverRepository;
            private readonly IInfringementRepository _infringementRepository;

            public Handler(IDriverInfringementRepository driverInfringementRepository, IDriverRepository driverRepository, IInfringementRepository infringementRepository)
            {
                _driverInfringementRepository = driverInfringementRepository;
                _driverRepository = driverRepository;
                _infringementRepository = infringementRepository;
            }

            public async Task<Unit> Handle(Data request, CancellationToken cancellationToken)
            {
                Guid _Id = Guid.NewGuid();
                var infringement = new DriverInfringement()
                {
                    Id = _Id,
                    DateCreated = request.DateCreated,
                    InfringementId = request.InfringementId,
                    DriverId = request.DriverId,
                    PointDiscount = request.PointDiscount,
                    Status = true
                };

                var findDriver = _driverRepository.GetSingle(f => f.Id == infringement.DriverId);
                if (findDriver == null)
                {
                    throw new ExceptionHandler(HttpStatusCode.Conflict, new { mensaje = "Driver not exist in the database" });
                }

                var findInf = await _infringementRepository.FindBy(f => f.Id == infringement.InfringementId);
                if (!findInf.Any())
                {
                    throw new ExceptionHandler(HttpStatusCode.Conflict, new { mensaje = "Infringement not exist in the database" });
                }

                _driverInfringementRepository.Add(infringement);

                findDriver.LostPoints = findDriver.LostPoints + infringement.PointDiscount;
                _driverRepository.Update(findDriver);

                var value = await _driverInfringementRepository.Commit();
                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("The record was not inserted");
            }
        }
    }
}
