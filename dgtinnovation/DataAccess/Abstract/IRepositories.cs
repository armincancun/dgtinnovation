using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace DataAccess.Abstract
{
    public interface IDriverRepository: IEntityBaseRepository<Driver> {}
    public interface IDriverInfringementRepository : IEntityBaseRepository<DriverInfringement> {}
    public interface IDriverVehicleRepository : IEntityBaseRepository<DriverVehicle>{}
    public interface IInfringementRepository : IEntityBaseRepository<Infringement>{}
    public interface IVehiclesRepository : IEntityBaseRepository<Vehicle>{}
}
