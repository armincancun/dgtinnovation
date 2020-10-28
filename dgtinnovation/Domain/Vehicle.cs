using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Abstract;

namespace Domain
{
    public class Vehicle: IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string Enrollment { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Guid RegularDriver { get; set; }
        public bool Status { get; set; }

        public ICollection<DriverVehicle> DriverVehicles { get; set; }

    }
}