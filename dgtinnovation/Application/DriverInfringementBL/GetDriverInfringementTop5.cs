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
    public class GetDriverInfringementTop5
    {
        public class Data : IRequest<List<InfringementTop5DTO>>
        {
            public Guid DriverId { get; set; }
        }

        public class Handler : IRequestHandler<Data, List<InfringementTop5DTO>>
        {
            private readonly IDriverInfringementRepository _driverInfringementRepository;
            private readonly IMapper _mapper;
            public Handler(IDriverInfringementRepository driverInfringementRepository, IMapper mapper)
            {
                _driverInfringementRepository = driverInfringementRepository;
                _mapper = mapper;
            }

            public async Task<List<InfringementTop5DTO>> Handle(Data request, CancellationToken cancellationToken)
            {

                var listAll = await _driverInfringementRepository.GetAll();
                var infringements = listAll.GroupBy(i => i.InfringementId)
                    .Select(group => new
                    {
                        Infringement = group.Key,
                        Count = group.Count()
                    })
                    .OrderBy(x => x.Count)
                    .Take(5);

                var resultList = new List<InfringementTop5DTO>();
                foreach (var item in infringements)
                {
                    var list = new InfringementTop5DTO
                    {
                        Infringement = item.Infringement,
                        Count = item.Count
                    };
                    resultList.Add(list);
                };

                return resultList;
            }

        }
    }
}
