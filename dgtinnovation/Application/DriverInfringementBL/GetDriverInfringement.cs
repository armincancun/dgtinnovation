using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using Domain;
using System.Threading.Tasks;
using System.Threading;

namespace Application.DriverInfringementBL
{
    public class GetDriverInfringement
    {
        public class InfringementList : IRequest<List<DriverInfringement>> { }

        public class Handler : IRequestHandler<InfringementList, List<DriverInfringement>>
        {
            private readonly IDriverInfringementRepository _driverInfringementRepository;
            public Handler(IDriverInfringementRepository driverInfringementRepository)
            {
                _driverInfringementRepository = driverInfringementRepository;
            }

            public async Task<List<DriverInfringement>> Handle(InfringementList request, CancellationToken cancellationToken)
            {
                var infringements = await _driverInfringementRepository.GetAll();

                return infringements;
            }

        }
    }
}
