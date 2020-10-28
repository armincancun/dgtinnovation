using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class DriverInfringement : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid InfringementId { get; set; }
        public Guid DriverId { get; set; }
        public int PointDiscount { get; set; }
        public bool Status { get; set; }

        public Infringement Infringement { get; set; }
        public Driver Driver { get; set; }

    }
}
