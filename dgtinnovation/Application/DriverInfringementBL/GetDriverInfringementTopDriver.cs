using AutoMapper;
using DataAccess.Abstract;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DriverInfringementBL
{
    public class GetDriverInfringementTopDriver
    {
        public class Data : IRequest<List<DriverTop5DTO>>
        {
            public int Number { get; set; }
        }

        public class Handler : IRequestHandler<Data, List<DriverTop5DTO>>
        {
            private readonly IDriverInfringementRepository _driverInfringementRepository;
            private readonly IMapper _mapper;
            public Handler(IDriverInfringementRepository driverInfringementRepository, IMapper mapper)
            {
                _driverInfringementRepository = driverInfringementRepository;
                _mapper = mapper;
            }

            public async Task<List<DriverTop5DTO>> Handle(Data request, CancellationToken cancellationToken)
            {

                var listAll = await _driverInfringementRepository.GetAll();
                var infringements = listAll.GroupBy(i => i.DriverId)
                    .Select(group => new
                    {
                        Infringement = group.Key,
                        Count = group.Count()
                    })
                    .OrderBy(x => x.Count)
                    .Take(request.Number);

                var resultList = new List<DriverTop5DTO>();
                foreach (var item in infringements)
                {
                    var list = new DriverTop5DTO
                    {
                        Driver = item.Infringement,
                        Count = item.Count
                    };
                    resultList.Add(list);
                };

                return resultList;
            }

        }
    }
}
