using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Abstract;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverBL
{
    public class GetDriver
    {
        public class DriverList : IRequest<List<Driver>> { }

        public class Handler : IRequestHandler<DriverList, List<Driver>>
        {
            private readonly IDriverRepository _driverRepository;
            public Handler(IDriverRepository driverRepository)
            {
                _driverRepository = driverRepository;
            }

            public async Task<List<Driver>> Handle(DriverList request, CancellationToken cancellationToken)
            {
                var drivers = await _driverRepository.GetAll();

                return drivers;
            }

        }
    }
}
