using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using Domain;

namespace DataAccess.Repositories
{
    public class DriverVehicleRepository: EntityBaseRepository<DriverVehicle>, IDriverVehicleRepository
    {
        public DriverVehicleRepository(dbInnovationContext context): base(context){}
    }
}
