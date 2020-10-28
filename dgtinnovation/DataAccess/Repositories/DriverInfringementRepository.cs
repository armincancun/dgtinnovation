using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using Domain;

namespace DataAccess.Repositories
{
    public class DriverInfringementRepository: EntityBaseRepository<DriverInfringement>, IDriverInfringementRepository
    {
        public DriverInfringementRepository(dbInnovationContext context): base(context)
        {}
    }
}
