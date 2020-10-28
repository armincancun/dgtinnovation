using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DataAccess.Repositories;
using System.Threading.Tasks;
using System.Threading;
using Domain;
using DataAccess.Abstract;

namespace Application.InfringementBL
{
    public class AddInfringement
    {
        public class Data : IRequest
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
            public short Points_To_Discount { get; set; }
            public bool Status { get; set; }
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
                Guid _Id = Guid.NewGuid();
                var infringement = new Infringement
                {
                    Id = _Id,
                    Description = request.Description,
                    Points_To_Discount = request.Points_To_Discount,
                    Status = true
                };

                _infringementRepository.Add(infringement);
                var value = await _infringementRepository.Commit();
                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("The record was not inserted");
            }
        }
    }
}
