using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using Domain;

namespace DataAccess.Repositories
{
    public class InfringementRepository: EntityBaseRepository<Infringement>, IInfringementRepository
    {
        public InfringementRepository(dbInnovationContext context): base(context) {}
    }
}
