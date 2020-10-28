using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Abstract;

namespace Domain
{
    public class Driver: IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string DNI { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalPoints { get; set; }
        public int LostPoints { get; set; }
        public bool Status { get; set; }

        public ICollection<DriverVehicle> DriverVehicles { get; set; }
        public ICollection<DriverInfringement> DriverInfringements { get; set; }
    }
}
