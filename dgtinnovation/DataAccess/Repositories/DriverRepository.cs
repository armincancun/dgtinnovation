using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using Domain;

namespace DataAccess.Repositories
{
    public class DriverRepository: EntityBaseRepository<Driver>, IDriverRepository
    {
        public DriverRepository(dbInnovationContext context)
            : base(context)
        { }
    }
}
