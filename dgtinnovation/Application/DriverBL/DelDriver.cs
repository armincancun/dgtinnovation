using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Abstract;
using MediatR;

namespace Application.DriverBL
{
    public class DelDriver
    {
        public class Data : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Data>
        {
            private readonly IDriverRepository _driverRepository;

            public Handler(IDriverRepository driverRepository)
            {
                _driverRepository = driverRepository;
            }

            public async Task<Unit> Handle(Data request, CancellationToken cancellationToken)
            {
                var driver = _driverRepository.GetSingle(request.Id);
                if (driver == null)
                {
                    throw new Exception("no record found");
                }
                _driverRepository.Delete(driver);

                var result = await _driverRepository.Commit();
                if (result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Records were not saved");
            }
        }
    }
}
