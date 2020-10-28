using System;
using System.ComponentModel.DataAnnotations;
using Domain.Abstract;

namespace Domain
{
    public class DriverVehicle: IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }

        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}