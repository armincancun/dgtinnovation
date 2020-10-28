using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InfringementBL
{
    public class GetInfringement
    {
        public class InfringementList : IRequest<List<Infringement>> { }

        public class Handler : IRequestHandler<InfringementList, List<Infringement>>
        {
            private readonly IInfringementRepository _infringementRepository;
            public Handler(IInfringementRepository infringementRepository)
            {
                _infringementRepository = infringementRepository;
            }

            public async Task<List<Infringement>> Handle(InfringementList request, CancellationToken cancellationToken)
            {
                var infringements = await _infringementRepository.GetAll();

                return infringements;
            }

        }
    }
}
