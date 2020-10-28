using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class dbInnovationContext: DbContext 
    {
        public dbInnovationContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<DriverInfringement> DriverInfringement { get; set; }
        public virtual DbSet<DriverVehicle> DriverVehicle { get; set; }
        public virtual DbSet<Infringement> Infringement { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }

    }
}