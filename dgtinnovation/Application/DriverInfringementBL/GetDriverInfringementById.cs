using DataAccess.Abstract;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DriverInfringementBL
{
    public class GetDriverInfringementById
    {
        public class Data : IRequest<List<DriverInfringement>>
        {
            public Guid DriverId { get; set; }
        }

        public class Handler : IRequestHandler<Data, List<DriverInfringement>>
        {
            private readonly IDriverInfringementRepository _driverInfringementRepository;
            public Handler(IDriverInfringementRepository driverInfringementRepository)
            {
                _driverInfringementRepository = driverInfringementRepository;
            }

            public async Task<List<DriverInfringement>> Handle(Data request, CancellationToken cancellationToken)
            {
                var infringements = await _driverInfringementRepository.FindBy(f => f.DriverId == request.DriverId);

                return infringements;
            }

        }
    }
}
