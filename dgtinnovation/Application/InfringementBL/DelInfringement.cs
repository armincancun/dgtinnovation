using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Abstract;

namespace Application.InfringementBL
{
    public class DelInfringement
    {
        public class Data : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Data>
        {
            private readonly IInfringementRepository _infringementRepository;

            public Handler(IInfringementRepository infringementRepository)
            {
                _infringementRepository = infringementRepository;
            }

            public async Task<Unit> Handle(Data request, CancellationToken cancellationToken)
            {
                var infringement = _infringementRepository.GetSingle(request.Id);
                if (infringement == null)
                {
                    throw new Exception("no record found");
                }
                _infringementRepository.Delete(infringement);

                var result = await _infringementRepository.Commit();
                if (result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Records were not saved");
            }
        }
    }
}
