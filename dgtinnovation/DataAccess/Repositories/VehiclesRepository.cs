using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DataAccess.Abstract;
using Domain;

namespace DataAccess.Repositories
{
    public class VehiclesRepository: EntityBaseRepository<Vehicle>, IVehiclesRepository
    {
        public VehiclesRepository(dbInnovationContext context)
            : base(context)
        { }
    }
}
